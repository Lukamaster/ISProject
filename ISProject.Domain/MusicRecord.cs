using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Domain
{
    public class MusicRecord : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Artist { get; set; }
        public double Price { get; set; }
        public int Volume { get; set; }
        public bool InStock { get; set; }
        public string ImageURL { get; set; }
        public ICollection<MusicRecordInOrder>? MusicRecordInOrders { get; set; }
        public virtual ICollection<MusicRecordInShoppingCart>? MusicRecordsInShoppingCart { get; set; }
    }
}
