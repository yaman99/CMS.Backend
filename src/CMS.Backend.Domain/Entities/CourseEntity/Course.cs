using CMS.Backend.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Domain.Entities.CourseEntity
{
    public class Course : BaseEntity<Guid>
    {
        public Guid Instructor { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Status{ get; set; }
        public IList<Lesson> Lessons { get; set; }
        public Course(Guid id , Guid instructor,  string title , string subject , string description) : base(id)
        {
            Title = title;
            Subject = subject;
            Description = description;
            Instructor = instructor;
            Status = "available";
            Lessons = new List<Lesson>();
        }

        public Course(Guid id ):base(id)
        {
        }
    }
}
