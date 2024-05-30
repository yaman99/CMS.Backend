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
        public string Title { get; set; }
        public long Size { get; set; }
        public string Video { get; set; }
        public string Description { get; set; }
        public Lesson(Guid id) : base(id)
        {
        }
    }
}
