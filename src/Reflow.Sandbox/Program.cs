using System;
using System.Collections.Generic;
using System.IO;
using Reflow.Core.API;
using Reflow.Models.Entity;

namespace Reflow.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            string fileDir = @"C:\Users\Hentov\Desktop\testcount";

            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            Console.WriteLine(path);

            ReflowController app = new ReflowController();
            int testNumber = 1;

            Console.WriteLine($"[{testNumber++}] Get Dir()");
            Console.WriteLine(app.GetDir(null).Result);
            
            Console.WriteLine($"{Environment.NewLine}[{testNumber++}] Get Files In Directory()");
            Console.WriteLine(app.GetFilesInDirectory(fileDir).Result);
            
            Console.WriteLine($"{Environment.NewLine}[{testNumber++}] Get Files Count()");
            Console.WriteLine(app.GetFilesCount(null).Result);
            
            Console.WriteLine($"{Environment.NewLine}[{testNumber++}] Get Options()");
            Console.WriteLine(app.GetSettings(null).Result);

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($"{Environment.NewLine}[{testNumber++}] Get Tags()");
            Console.WriteLine(app.GetTags(null).Result);

            Console.WriteLine($"{Environment.NewLine}[{testNumber++}] Get Progress()");
            Console.WriteLine(app.GetProgress(null).Result);




            var res = app.UpdateTagsStructure(
                "[{\"Id\":1,\"TagType\":\"AutoIncrementTag\",\"Options\":[{\"Id\":1,\"TagType\":\"Has Leading Zero\",\"Type\":\"CheckBox\",\"Default\":true,\"props\":{\"name\":\"Has Leading Zero\",\"default\":true}},{\"Id\":2,\"TagType\":\"Start From\",\"Type\":\"NumericBox\",\"Default\":0,\"props\":{\"name\":\"Start From\",\"default\":0}},{\"Id\":3,\"TagType\":\"Skip\",\"Type\":\"NumericBox\",\"Default\":0,\"props\":{\"name\":\"Skip\",\"default\":0}}]}]");

            var res2 = app.GetFiles(null).Result.ToString();
            Console.WriteLine(res2);
            Console.WriteLine(res.Result);
           // var opts = new Dictionary<int, Option>();
           // opts.Add(7, new Option(7, "Regex", OptionType.TextArea));
           // opts.Add(8, new Option(8, "Escape Regex", OptionType.Toggle));
           // opts.Add(9, new Option(9, "Regex Params", OptionType.CheckList));
           // Console.WriteLine($"{Environment.NewLine}[{testNumber++}] UpdateTagsStructure()");
           // //Console.WriteLine(app.UpdateTagsStructure2(new Tag(4, "Regex", opts, new[] { 7, 8, 9 })).Result);
           // //Console.WriteLine(app.UpdateTagsStructure2("[{\"Id\":4,\"TagType\":\"Regex\",\"Options\":[{\"Id\":7,\"TagType\":\"Regex\",\"Type\":\"TextArea\",\"Values\":null,\"Default\":null,\"props\":{\"name\":\"Regex\"}},{\"Id\":8,\"TagType\":\"Escape Regex\",\"Type\":\"Toggle\",\"Values\":null,\"Default\":\"false\",\"props\":{\"name\":\"Escape Regex\",\"default\":false}},{\"Id\":9,\"TagType\":\"Regex Params\",\"Type\":\"CheckList\",\"Values\":[\"/g\",\"/m\",\"/i\",\"/x\",\"/X\",\"/s\",\"/u\",\"/U\",\"/A\",\"/D\",\"/J\"],\"Default\":\"Select regex params\",\"props\":{\"list\":[{\"name\":\"/g\",\"default\":false},{\"name\":\"/m\",\"default\":false},{\"name\":\"/i\",\"default\":false},{\"name\":\"/x\",\"default\":false},{\"name\":\"/X\",\"default\":false},{\"name\":\"/s\",\"default\":false},{\"name\":\"/u\",\"default\":false},{\"name\":\"/U\",\"default\":false},{\"name\":\"/A\",\"default\":false},{\"name\":\"/D\",\"default\":false},{\"name\":\"/J\",\"default\":false}],\"listKey\":\"Regex Params\"}}]}]"));
           // Console.WriteLine(app.UpdateTagsStructure2("[{\"Id\":1,\"TagType\":\"AutoIncrementTag\",\"Options\":[{\"Id\":1,\"TagType\":\"HasLeadingZero\",\"Type\":\"CheckBox\",\"Values\":null,\"Default\":\"true\",\"props\":{\"name\":\"HasLeadingZero\",\"default\":true}},{\"Id\":2,\"TagType\":\"StartFrom\",\"Type\":\"NumericBox\",\"Values\":null,\"Default\":\"0\",\"props\":{\"name\":\"StartFrom\",\"default\":0}},{\"Id\":3,\"TagType\":\"Skip\",\"Type\":\"NumericBox\",\"Values\":null,\"Default\":\"0\",\"props\":{\"name\":\"Skip\",\"default\":0}}]}]"));
           // //Console.WriteLine($"{Environment.NewLine}[{testNumber++}] Get Filters()");
           // //Console.WriteLine(app.GetFilters(null).Result);
           //

            //var a = Console.ReadLine();
        }
    }
}
