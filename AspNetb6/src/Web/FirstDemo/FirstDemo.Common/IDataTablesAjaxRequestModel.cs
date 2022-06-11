namespace FirstDemo.Common
{
    public interface IDataTablesAjaxRequestModel
    {
        int Length { get; }
        int PageIndex { get; }
        int PageSize { get; }
        string SearchText { get; }
        int Start { get; }

        string GetSortText(string[] columnNames);
    }
}