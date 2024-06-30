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
        public List<MusicRecord> GetAll();
        public MusicRecord GetRecordById(Guid? id);
        public MusicRecord CreateRecord(MusicRecord record);
        public MusicRecord UpdateRecord(MusicRecord record);
        public MusicRecord DeleteRecord(Guid? id);
    }
}
