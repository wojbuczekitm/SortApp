
namespace SortApp.Application.Service
{
    public interface IFileService
    {
        IEnumerable<long> GetLastSavedFileContent();
        void SaveNumberArrayToFile(IEnumerable<long> numbers);
    }
}