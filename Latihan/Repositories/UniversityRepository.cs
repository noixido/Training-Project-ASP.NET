using Latihan.Context;
using Latihan.Models;
using Latihan.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Latihan.Repositories
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly MyContext _context;

        public UniversityRepository(MyContext context)
        {
            _context = context;
        }

        public int addUniversity(University university)
        {
            var lastRecord = _context.Universities.Max(u => u.Univ_Id);
            if(lastRecord == null)
            {
                university.Univ_Id = "U001";
            }
            else
            {
                var lastRecordId = int.Parse(lastRecord.Substring(1));
                lastRecordId++;
                var number = lastRecordId.ToString("D3");
                var customId = "U" + number;

                university.Univ_Id = customId;
            }

            _context.Universities.Add(university);
            return _context.SaveChanges();
        }

        public University GetLastInsertedData()
        {
            var lastInserted = _context.Universities
                .OrderByDescending(u => u.Univ_Id)
                .FirstOrDefault();
            if(lastInserted == null)
            {
                return null;
            }

            return new University
            {
                Univ_Id = lastInserted.Univ_Id,
                Univ_Name = lastInserted.Univ_Name,
            };
            
        }

        public int deleteUniversity(string univId)
        {
            var deleteUniv = _context.Universities.Find(univId);
            if (deleteUniv == null)
            {
                return 0;
            }

            _context.Universities.Remove(deleteUniv);
            return _context.SaveChanges();
        }

        public IEnumerable<University> GetAllUniversities()
        {
            return _context.Universities.ToList();
        }

        public University GetUniversityById(string univId)
        {
            return _context.Universities.Find(univId);
        }

        public int updateUniversity(University university)
        {
            _context.Entry(university).State = EntityState.Modified;
            return _context.SaveChanges();
        }
    }
}
