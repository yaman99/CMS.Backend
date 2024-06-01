using CMS.Backend.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Domain.Entities.CommunityEntity
{
    public class CommunityMember : BaseEntity<Guid>
    {
        public string FullName { get; set; }
        public CommunityMember(Guid id) : base(id)
        {
        }
    }
}
