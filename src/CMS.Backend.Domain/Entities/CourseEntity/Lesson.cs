using CMS.Backend.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Domain.Entities.CourseEntity
{
    public class Lesson : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public string Video { get; set; }
        public Lesson(Guid id) : base(id)
        {
        }
    }
}
