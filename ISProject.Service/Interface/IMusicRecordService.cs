using ISProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Service.Interface
{
    public interface IMusicRecordService
    {
        public Task<List<MusicRecord>> GetAll();
        public Task<MusicRecord> GetRecordById(Guid id);
        public Task<MusicRecord> CreateRecord(MusicRecord record);
        public Task<MusicRecord> UpdateRecord(MusicRecord record);
        public Task<MusicRecord> DeleteRecordAsync(Guid id);
    }
}
