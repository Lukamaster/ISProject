using ISProject.Domain;
using ISProject.Repository.Interface;
using ISProject.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Service.Implementation
{
    public class MusicRecordService : IMusicRecordService
    {
        private readonly IRepository<MusicRecord> recordRepository;
        public MusicRecordService(IRepository<MusicRecord> recordRepository)
        {
            this.recordRepository = recordRepository;
        }

        public async Task<MusicRecord> CreateRecord(MusicRecord record)
        {
            return await recordRepository.Insert(record);
        }

        public async Task DeleteRecordAsync(Guid id)
        {
            await recordRepository.Delete(await recordRepository.Get(id));
        }

        public async Task<List<MusicRecord>> GetAll()
        {
            return await recordRepository.GetAll();
        }

        public async Task<MusicRecord> GetRecordById(Guid id)
        {
            return await recordRepository.Get(id);
        }

        public async Task<MusicRecord> UpdateRecord(MusicRecord record)
        {
            return await recordRepository.Update(record);
        }
    }
}
