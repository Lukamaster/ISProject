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

        public MusicRecord CreateRecord(MusicRecord record)
        {
            record.Id = Guid.NewGuid();
            return recordRepository.Insert(record);
        }

        public MusicRecord DeleteRecord(Guid? id)
        {
            return recordRepository.Delete(recordRepository.Get(id));
        }

        public List<MusicRecord> GetAll()
        {
            return recordRepository.GetAll().ToList();
        }

        public MusicRecord GetRecordById(Guid? id)
        {
            return recordRepository.Get(id);
        }

        public MusicRecord UpdateRecord(MusicRecord record)
        {
            return recordRepository.Update(record);
        }
    }
}
