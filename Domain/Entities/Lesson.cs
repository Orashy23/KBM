using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Lesson
    {
        public int LessonID { get; set; }
        public string ProjectName { get; set; }
        public string TitleName { get; set; }
        public string Description { get; set; }
        public string ValueProposition { get; set; }
        public string TargetAudience { get; set; }

        public string PersonToContact { get; set; }
        public string ImageURL { get; set; }

        public int FunctionID { get; set; }
        public int DepartmentID { get; set; }
        public int IndustryID { get; set; }

        // Inside Lesson.cs (add below the FK IDs):
        public Function? Function { get; set; }
        public Department? Department { get; set; }
        public Industry? Industry { get; set; }

       
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

    }
}
