using CMS.Backend.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Domain.Entities.CourseEntity
{
    public class EnrolledStudent : BaseEntity<Guid>
    {
        public EnrolledStudent(Guid id) : base(id)
        {
        }

    }
}
