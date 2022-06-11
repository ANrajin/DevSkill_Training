using Autofac;
using FirstDemo.Training.Services;
using FirstDemo.Web.Models;

namespace FirstDemo.Web.Areas.Admin.Models
{
    public class StudentListModel
    {
        private IStudentService _studentService;
        public StudentSearchModel SearchItem { get; set; }

        public StudentListModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public StudentListModel() { }

        public async Task<object> GetPagedStudents(DataTablesAjaxRequestModel model)
        {
            var data = await _studentService.GetStudentsAsync(
                model.PageIndex,
                model.PageSize,
                SearchItem.Name!,
                SearchItem.Address!,
                model.GetSortText(new string[] { "Name", "Fee" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string?[]
                        {
                                record.Name,
                                record.CGPA.ToString(),
                                record.Address,
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _studentService = scope.Resolve<IStudentService>();
        }

        internal void DeleteStudent(int id)
        {
            _studentService.DeleteStudent(id);
        }
    }
}
