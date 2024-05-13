using CMS.Backend.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Domain.Entities.CourseEntity
{
    public class StudentCourses : BaseEntity<Guid>
    {
        public StudentCourses(Guid id) : base(id)
        {
        }

        public Guid Student { get; set; }
        public Guid Course { get; set; }
    }
}
