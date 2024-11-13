using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Domain
{
    public class MusicRecordInShoppingCart : BaseEntity
    {
        public Guid MusicRecordId { get; set; }
        public MusicRecord MusicRecord { get; set; }
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}
