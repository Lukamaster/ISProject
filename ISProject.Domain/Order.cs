using ISProject.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Domain
{
    public class Order : BaseEntity
    {
        public string? OwnerId { get; set; }
        public MusicStoreUser? Owner { get; set; }
        public ICollection<MusicRecordInOrder>? MusicRecordsInOrder { get; set; }
    }
}
