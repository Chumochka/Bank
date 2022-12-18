using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class Helper
    {
        private static BankEntities s_bankEntities;
        public static BankEntities GetContext()
        {
            if (s_bankEntities == null)
            {
                s_bankEntities = new BankEntities();
            }
            return s_bankEntities;
        }
        public int SearchUsers(string login, string password)
        {
            var user = s_bankEntities.User.Where(x=> x.Login == login && x.Password == password).FirstOrDefault();
            if (user == null)
            {
                return -1;
            }
            else
            {
                return user.IDUser;
            }
        }
        public long LastBankAccount(int IDUser, DateTime date, int balance, int type)
        {
            var bankAccount = s_bankEntities.BankAccount.OrderByDescending(x=> x.UserID == IDUser && x.DateOpen == date && x.Balance == balance && x.TypeID == type).FirstOrDefault();
            return bankAccount.NumberAccount;
        }
        public int LastContract(long numberAccount, int userID, int amount, int period, DateTime date, double percet)
        {
            var contract = s_bankEntities.Contract.OrderByDescending(x => x.NumberAccount == numberAccount && x.UserID == userID && x.Amount == amount && x.Period == period && x.ExpirationDate == date && x.Percet == percet).FirstOrDefault();
            return contract.IDContract;
        }

        public int CreateContract(int IDUser, int amount,int period, double percet)
        {
            BankAccount bankAccount = new BankAccount();
            bankAccount.UserID = IDUser;
            DateTime date = DateTime.Today;
            bankAccount.DateOpen = date;
            bankAccount.Balance = amount;
            bankAccount.TypeID = 3;
            s_bankEntities.BankAccount.Add(bankAccount);
            s_bankEntities.SaveChanges();
            long number_account = LastBankAccount(IDUser,date,amount,3);
            Contract contract = new Contract();
            contract.NumberAccount = number_account;
            contract.UserID = IDUser;
            contract.Amount = amount;
            contract.Period = period;
            contract.Percet = percet;
            DateTime date1 = DateTime.Today.AddMonths(period);
            contract.ExpirationDate = date1;
            s_bankEntities.Contract.Add(contract);
            s_bankEntities.SaveChanges();
            int IDContract = LastContract(number_account,IDUser,amount,period,date1,percet);
            return IDContract;
        }
        public User FindUser(int IdUser)
        {
            var user = s_bankEntities.User.Where(x => x.IDUser == IdUser).FirstOrDefault();
            return user;
        }
        public BankAccount FindBankAccount(long number_account)
        {
            var bankAccount = s_bankEntities.BankAccount.Where(x => x.NumberAccount == number_account).FirstOrDefault();
            return bankAccount;
        }
        public Contract FindContract(int IDContract)
        {
            var contract = s_bankEntities.Contract.Where(x => x.IDContract == IDContract).FirstOrDefault();
            return contract;
        }
    }
}
