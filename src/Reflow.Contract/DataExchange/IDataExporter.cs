namespace Reflow.Contract.DataExchange
{
    public interface IDataExporter<out T>
    {
        T Export(object data);

        T ExportSuccess(object data);

        T ExportError(object data);
    }
}
