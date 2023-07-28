using Model;
using DAL;

namespace BL
{
    public class StaffBL
    {
        StaffDAL staffDAL = new StaffDAL();
        public Staff? Login(string UserName)
        {
            Console.Write("Password: ");
            string PassWord = "";
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && PassWord.Length > 0)
                {
                    Console.Write("\b \b");
                    PassWord = PassWord[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    PassWord += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            
            Staff staff = new Staff();
            staff = staffDAL.GetStaffAccount(UserName);
            if (staff.Password == PassWord)
            {
                return staff;
            }
            else
            {
                return null;
            }
        }
    }
}