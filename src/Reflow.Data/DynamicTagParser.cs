using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Reflow.Models.Options;
using Reflow.Models.Options.Base;
using Reflow.Contract.DTO;
using Reflow.Contract.Entity;
using Reflow.Contract.Attributes;

namespace Reflow.Data
{
    public static class DynamicTagParser
    {
        private static readonly IDictionary<int, ITagOption> _options = new ConcurrentDictionary<int, ITagOption>();
        private static readonly IDictionary<int, Type> _tags = new Dictionary<int, Type>();
        private static readonly IDictionary<Type, ITagOption> _registeredOptionTypes = new Dictionary<Type, ITagOption>();
        private static readonly IDictionary<Type, OptionDefaultSet> _typeMappings = new Dictionary<Type, OptionDefaultSet>();

        private const int TagKey = 100_000;
        private const int OptionKey = 100;

        static DynamicTagParser()
        {
            RegisterTypeMappings();
            Load();
        }

        private static void RegisterTypeMappings()
        {
            _typeMappings.Add(typeof(Byte), new OptionDefaultSet { DefaultType = typeof(NumericBoxOption), DefaultValue = 0 });
            _typeMappings.Add(typeof(Int16), new OptionDefaultSet { DefaultType = typeof(NumericBoxOption), DefaultValue = 0 });
            _typeMappings.Add(typeof(Int32), new OptionDefaultSet { DefaultType = typeof(NumericBoxOption), DefaultValue = 0 });
            _typeMappings.Add(typeof(Int64), new OptionDefaultSet { DefaultType = typeof(NumericBoxOption), DefaultValue = 0 });

            _typeMappings.Add(typeof(String), new OptionDefaultSet { DefaultType = typeof(TextBoxOption), DefaultValue = string.Empty });
            _typeMappings.Add(typeof(Char), new OptionDefaultSet { DefaultType = typeof(TextBoxOption), DefaultValue = string.Empty });

            _typeMappings.Add(typeof(Boolean), new OptionDefaultSet { DefaultType = typeof(ToggleOption), DefaultValue = false });

            _typeMappings.Add(typeof(Enum), new OptionDefaultSet { DefaultType = typeof(ListOption), DefaultValue = null });
            _typeMappings.Add(typeof(List<>), new OptionDefaultSet { DefaultType = typeof(CheckListOption), DefaultValue = null });
        }

        private static void Load()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().First(a => a.FullName.Contains("Reflow.Models"));
            var types = assembly.GetTypes();
            var optionTypes = types.Where(t => typeof(ITagOption).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            var tags = types.Where(t => typeof(ITag).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            var tagId = TagKey;
            foreach (var tag in tags)
            {
                var props = tag.GetProperties();
                var reflowOptions = props.Where(p => p.IsDefined(typeof(ReflowOptionAttribute), false));

                var optionId = OptionKey;
                foreach (var option in reflowOptions)
                {
                    ReflowOptionAttribute attributeValue = TryGetAttributeValue(option);

                    var optionType = TryGetType(option, attributeValue);
                    var optionDefault = TryGetDefault(option, attributeValue);
                    var optionName = attributeValue.Name ?? option.Name;

                    var key = tagId + optionId;
                    optionId += OptionKey;
                    if (attributeValue is ReflowCollectionOptionAttribute)
                    {
                        var enabledOptions = (attributeValue as ReflowCollectionOptionAttribute).EnabledValues.Select((o, idx) => (CollectionItem)Activator.CreateInstance(typeof(CollectionItem), idx, o, true));
                        var disabledOptions = (attributeValue as ReflowCollectionOptionAttribute).DisabledValues.Select((o, idx) => (CollectionItem)Activator.CreateInstance(typeof(CollectionItem), idx, o, false));
                        IEnumerable<CollectionItem> enumGeneratedOptions = TryGetEnumOptions(option);
                        _options.Add(key, (ITagOption)Activator.CreateInstance(optionType, key, optionName, optionDefault, enabledOptions.Union(disabledOptions).Union(enumGeneratedOptions).ToList()));
                    }
                    else
                    {
                        _options.Add(key, (ITagOption)Activator.CreateInstance(optionType, key, optionName, optionDefault));
                    }

                }
                var tagName = (ReflowTagAttribute)tag.GetCustomAttribute(typeof(ReflowTagAttribute));
                var attributeName = tagName?.Name ?? tag.Name;

                _tags.Add(tagId, tag);

                tagId += TagKey;
            }
        }

        public static IEnumerable<ITagModel> GetTags()
        {
            return _tags.Select((t, idx) => new TagViewModel()
            {
                Id = t.Key,
                OrderId = idx,
                TagType = t.Value.Name,
                Name = ((ReflowTagAttribute)t.Value.GetCustomAttribute(typeof(ReflowTagAttribute)))?.Name ?? t.Value.Name,
                Options = _options.Where(o => (o.Key - t.Key) < TagKey && (o.Key - t.Key) > 0).Select(o => o.Value).ToList()
            });
        }

        public static IEnumerable<ITagOption> GetOptions()
        {
            return _options.Values;
        }

        private static IEnumerable<CollectionItem> TryGetEnumOptions(PropertyInfo option)
        {
            try
            {
                var isEnum = option.PropertyType.BaseType == typeof(System.Enum);
                if (!isEnum)
                    return Enumerable.Empty<CollectionItem>();

                var enumValues = Enum.GetValues(option.PropertyType);
                var collectionItems = new List<CollectionItem>();
                for (int i = 0; i < enumValues.Length; i++)
                    collectionItems.Add(new CollectionItem(i, Convert.ToString(enumValues.GetValue(i)), false));

                return collectionItems;
            }
            catch (Exception)
            {
                return Enumerable.Empty<CollectionItem>();
            }

        }

        private static ReflowOptionAttribute TryGetAttributeValue(PropertyInfo option)
        {
            return (ReflowOptionAttribute)option.GetCustomAttribute(typeof(ReflowOptionAttribute))
                ?? (ReflowCollectionOptionAttribute)option.GetCustomAttribute(typeof(ReflowCollectionOptionAttribute));
        }

        private static object TryGetDefault(PropertyInfo option, ReflowOptionAttribute attributeValue)
        {
            try
            {
                if (attributeValue.Default == null)
                {
                    if (_typeMappings.ContainsKey(option.PropertyType))
                        return _typeMappings[option.PropertyType].DefaultValue;

                    return _typeMappings[option.PropertyType.BaseType].DefaultValue;
                }
                return attributeValue.Default;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static Type TryGetType(PropertyInfo option, ReflowOptionAttribute attributeValue)
        {
            try
            {
                if (attributeValue.OptionType == null)
                {
                    if (_typeMappings.ContainsKey(option.PropertyType))
                        return _typeMappings[option.PropertyType].DefaultType;

                    return _typeMappings[option.PropertyType.BaseType].DefaultType;
                }
                return attributeValue.OptionType;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
