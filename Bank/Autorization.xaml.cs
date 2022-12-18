using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace Bank
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        int amount = 1000;
        int period = 1;
        double percet = 8;
        public Autorization(int amount, int period, double percet)
        {
            InitializeComponent();
            this.amount = amount;
            this.period = period;
            this.percet = percet;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = tb_login.Text;
            string password = tb_password.Password;
            Helper helper = new Helper();
            BankEntities db = Helper.GetContext();
            HashPasswords.Hash hash = new HashPasswords.Hash();
            if(login == "")
            {
                if (password == "") MessageBox.Show("Введите логин и пароль");
                else MessageBox.Show("Введите логин");
            }
            else
            {
                if(password == "")
                {
                    MessageBox.Show("Введите пароль");
                }
                else
                {
                    password = hash.hashing(password);
                    int IDUser = helper.SearchUsers(login, password);
                    if(IDUser >= 0)
                    {
                        int IDContract = helper.CreateContract(IDUser, amount, period, percet);
                        Contract contract = helper.FindContract(IDContract);
                        CreateWord(IDUser, contract.NumberAccount, IDContract);
                    }
                    else
                    {
                        MessageBox.Show("Такого пользователя не существует.");
                    }
                }
            }

        }
        private void CreateWord(int IdUser, long IDBankAccount, int IdContract)
        {
            Helper helper = new Helper();
            BankEntities db = Helper.GetContext();
            User user = helper.FindUser(IdUser);
            BankAccount bankAccount = helper.FindBankAccount(IDBankAccount);
            Contract contract = helper.FindContract(IdContract);
            string TemplateFileName = @"C:\Users\Miller\source\repos\Bank\Bank\Images\ШаблонДоговора.docx";
            var wordApp = new Word.Application();
            wordApp.Visible = false;

            var wordDocument = wordApp.Documents.Open(TemplateFileName);
            ReplaceWordStub("Номер договора", contract.IDContract.ToString(),wordDocument);
            ReplaceWordStub("день", DateTime.Today.ToString("dd"),wordDocument);
            ReplaceWordStub("месяц", DateTime.Today.ToString("MM"), wordDocument);
            ReplaceWordStub("Год", DateTime.Today.ToString("yy"), wordDocument);
            ReplaceWordStub("ФИО вкладчика", user.Surname + " " + user.Name + " " + user.Patronymic,wordDocument);
            ReplaceWordStub("ФИО вкладчика", user.Surname + " " + user.Name + " " + user.Patronymic, wordDocument);
            ReplaceWordStub("Сумма_вклада", contract.Amount.ToString(),wordDocument);
            ReplaceWordStub("Срок_вклада", contract.Period.ToString(), wordDocument);
            ReplaceWordStub("Дата_окончания_срока_вклада",contract.ExpirationDate.ToString("dd/MM/yyyy"), wordDocument);
            ReplaceWordStub("Процентная_ставка_по_вкладу",contract.Percet.ToString(),wordDocument);
            ReplaceWordStub("Номер счета вклада", bankAccount.NumberAccount.ToString(), wordDocument);
            ReplaceWordStub("Адрес_регистрации", user.Adress.ToString(), wordDocument);
            ReplaceWordStub("Адрес_электронной_почты", user.E_Mail.ToString(), wordDocument);
            ReplaceWordStub("Серия_паспорта", user.Series.ToString(), wordDocument);
            ReplaceWordStub("Номер_паспорта", user.Number.ToString(), wordDocument);
            ReplaceWordStub("Кем_и_когда_выдан", user.Issued.ToString()+" " + user.DateOfIssue.ToString(), wordDocument);
            ReplaceWordStub("Дата_рождения", user.DateOfBirth.ToString("dd/MM/yyyy"), wordDocument);
            ReplaceWordStub("Место_рождения", user.PlaceOfBirth.ToString(), wordDocument);

            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                wordDocument.SaveAs(@"C:\Users\Miller\source\repos\Bank\Bank\Images\Договор.docx");
            }
            wordDocument.Close();
        }
        private void ReplaceWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace,ReplaceWith: text);
        }
    }
}
