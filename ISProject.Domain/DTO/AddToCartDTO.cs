using ISProject.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Domain.DTO
{
    public class AddToCartDTO
    {
        public string? OwnerId { get; set; }
        public ICollection<MusicRecordDTO> MusicRecordsInShoppingCart { get; set; }
    }
}
