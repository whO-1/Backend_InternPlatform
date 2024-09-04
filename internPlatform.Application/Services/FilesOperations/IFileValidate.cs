using System.Collections.Generic;
using System.Web;

namespace internPlatform.Application.Services.FilesOperations
{
    public interface IFileValidate
    {
        List<string> Validate(IEnumerable<HttpPostedFileBase> images);
        List<string> Validate(HttpPostedFileBase file);

    }
}
