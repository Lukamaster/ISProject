using ISProject.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Domain
{
    public class ShoppingCart : BaseEntity
    {
        public string? OwnerId { get; set; }
        public MusicStoreUser? Owner { get; set; }
        public virtual ICollection<MusicRecordInShoppingCart>? MusicRecordsInShoppingCart { get; set; }
    }
}
