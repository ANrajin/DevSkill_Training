using MyApp.Training.Services;
using MyApp.Web.Models;

namespace MyApp.Web.Areas.Admin.Models
{
    public class StudentListModel
    {
        private readonly IStudentService _studentService;

        public StudentListModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public object GetPaginatedStudents(DatatableAjaxRequestModel model)
        {
            var data = _studentService.GetStudents(
                model.PageIndex,
                model.PageSize,
                model.SearchText,
                model.GetSortText(new string[] { "Name", "CGPA", "Address" })
                );

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.CGPA.ToString(),
                                record.Address.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void Delete(int id)
        {
            _studentService.DeleteStudent(id);
        }
    }
}
