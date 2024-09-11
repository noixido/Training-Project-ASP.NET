using Latihan.Context;
using Latihan.Models;
using Latihan.Repositories.Interface;
using Latihan.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Latihan.Repositories
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly MyContext _context;

        public RegisterRepository(MyContext context)
        {
            _context = context;
        }

        public IEnumerable<ShowDataVM> GetAllEmpData()
        {
            return _context.Employees
                .Include(e => e.Account.Profiling.Education.University)
                .Select(a => new ShowDataVM
                {
                    NIK = a.NIK,
                    FullName = a.FirstName + " " + a.LastName,
                    Phone = a.Phone,
                    Email = a.Email,
                    BirthDate = a.BirthDate.Value.ToString("dd-MM-yyyy") ?? string.Empty,
                    Degree = a.Account.Profiling.Education.Degree.ToString(),
                    GPA = a.Account.Profiling.Education.GPA,
                    Univ_Name = a.Account.Profiling.Education.University.Univ_Name,
                })
                .ToList();
        }

        public RegisterVM lastInsertedEmpData()
        {
            var lastEmpInserted = _context.Employees
                .Include(a => a.Account)
                .OrderByDescending(e => e.NIK)
                .FirstOrDefault();
            var lastEduInserted = _context.Educations
                .Include(u => u.University)
                .OrderByDescending(e => e.Education_Id)
                .FirstOrDefault();
            if (lastEmpInserted == null && lastEduInserted == null)
            {
                return null;
            }
            return new RegisterVM
            {
                NIK = lastEmpInserted.NIK,
                FirstName = lastEmpInserted.FirstName,
                LastName = lastEmpInserted.LastName,
                BirthDate = lastEmpInserted.BirthDate,
                Phone = lastEmpInserted.Phone,
                Email = lastEmpInserted.Email,
                Degree = lastEduInserted.Degree.ToString(),
                GPA = lastEduInserted.GPA,
                Univ_Id = lastEduInserted.University_Id

            };
        }

        public bool Login(LoginVM loginVM)
        {
            var acc = _context.Accounts
                .Include(e => e.Employee)
                .FirstOrDefault(x => x.Employee.Email == loginVM.Username);
            if (acc == null)
            {
                throw new Exception("Email is Invalid");
            }

            var checkPass = BCrypt.Net.BCrypt.Verify(loginVM.Password, acc.Password);
            if (!checkPass)
            {
                throw new Exception("Try Again!");
            }
            return true;
        }

        public int Register(RegisterVM registerVM)
        {
            Employee emp = new Employee();

            // cek duplikat email
            if (_context.Employees.Any(e => e.Email == registerVM.Email))
            {
                throw new Exception("Email is Already Registered!");
            }

            // generate unik id buat employee
            DateTime now = DateTime.Now;
            var lastRecord = _context.Employees.Max(x => x.NIK);
            if (lastRecord == null)
            {
                //employee.Employee_Id = "2024090001";
                emp.NIK = $"{now.Year}{now.Month.ToString("D2")}0001";

            }
            else
            {
                var lastRecordId = int.Parse(lastRecord.Substring(7));
                lastRecordId++; //2
                var number = lastRecordId.ToString("D4"); //0002

                emp.NIK = $"{now.Year}{now.Month.ToString("D2")}{number}";
            }

            // mapping data untuk tabel Employees
            emp.FirstName = registerVM.FirstName;
            emp.LastName = registerVM.LastName;
            emp.Phone = registerVM.Phone;
            emp.BirthDate = registerVM.BirthDate;
            emp.Email = registerVM.Email;

            _context.Employees.Add(emp);

            Account account = new Account();

            account.NIK = emp.NIK;
            var pass = "12345";
            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            account.Password = BCrypt.Net.BCrypt.HashPassword(pass, salt);

            _context.Accounts.Add(account);

            Education education = new Education();

            var lastEduRecord = _context.Educations.Max(u => u.Education_Id);
            if (lastEduRecord == null)
            {
                education.Education_Id = "E001";
            }
            else
            {
                var lastEduRecordId = int.Parse(lastEduRecord.Substring(1));
                lastEduRecordId++;
                var eduNum = lastEduRecordId.ToString("D3");
                var customEduId = "E" + eduNum;

                education.Education_Id = customEduId;
            }

            switch (registerVM.Degree)
            {
                case "D3":
                    education.Degree = Degree.D3;
                    break;
                case "D4":
                    education.Degree = Degree.D4;
                    break;
                case "S1":
                    education.Degree = Degree.S1;
                    break;
                case "S2":
                    education.Degree = Degree.S2;
                    break;
                case "S3":
                    education.Degree = Degree.S3;
                    break;
                default:
                    throw new Exception("Degree is Invalid");
            }

            //education.Degree = registerVM.Degree;
            education.GPA = registerVM.GPA;
            education.University_Id = registerVM.Univ_Id;

            _context.Educations.Add(education);

            Profiling profiling = new Profiling();

            profiling.NIK = emp.NIK;
            profiling.Education_Id = education.Education_Id;

            _context.Profilings.Add(profiling);

            return _context.SaveChanges();
        }
    }
}
