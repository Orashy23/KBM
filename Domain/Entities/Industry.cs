using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Industry
    {

        public int IndustryID { get; set; }
        public string IndustryName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
