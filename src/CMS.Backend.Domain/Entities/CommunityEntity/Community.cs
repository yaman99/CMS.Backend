using CMS.Backend.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Domain.Entities.CommunityEntity
{
    public class Community : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public Guid Owner { get; set; }
        public IList<CommunityPost> Posts { get; set; }
        public IList<CommunityMember> Members { get; set; }
        public Community(Guid id) : base(id)
        {
        }

        public Community(Guid id , string name, string subject, Guid owner): base(id)
        {
            Name = name;
            Subject = subject;
            Owner = owner;
            Posts = new List<CommunityPost>();
            Members = new List<CommunityMember>();
        }
    }
}
