using CMS.Backend.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Backend.Domain.Entities.CommunityEntity
{
    public class CommunityPost : BaseEntity<Guid>
    {
        public string Content { get; set; }
        public Guid Owner { get; set; }
        public int Likes { get; set; }
        public CommunityPost(Guid id) : base(id)
        {
        }

        public CommunityPost(Guid id , string content, Guid owner) : base(id)
        {
            Content = content;
            Owner = owner;
            Likes = 0;
        }
    }
}
