using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Domain
{
    public class MusicRecordInOrder : BaseEntity
    {
        public Guid MusicRecordId { get; set; }
        public MusicRecord MusicRecord { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
