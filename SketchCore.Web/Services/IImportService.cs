using System.Threading.Tasks;

namespace SketchCore.Web.Services
{
    public interface IImportService
    {
        Task DeepImport(string pageUrl);
    }
}