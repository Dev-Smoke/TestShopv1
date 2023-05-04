using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TestShopv1.Models;
using TestShopv1.Models.ViewModel;

namespace TestShopv1.Services
{
    public class UserService
    {
        internal static Customer Get(Customer customer)
        {
            using (var db = new MyContext())
            {
                return db.Customers.SingleOrDefault(x => x.EmailAddress == customer.EmailAddress);
            }
        }
        public static void Register(Customer customer)
        {
                using (var db = new MyContext())
                {
                    if (!db.Customers.Any(x => x.EmailAddress == customer.EmailAddress))
                    {
                        customer.PasswordSalt = Guid.NewGuid().ToString();
                        customer.PasswordHash = Hash(customer.PasswordHash + customer.PasswordSalt.ToString());
                        db.Customers.Add(customer);
                        db.SaveChanges();
                    }
                }
            
        }
        public static Customer Mapping(CustomerVM userVM)
        {
            Customer customer = new Customer();
            customer.EmailAddress = userVM.EmailAddress;
            customer.PasswordHash = userVM.Password;
            customer.Salutation = userVM.Salutation;
            customer.Firstname = userVM.Firstname;
            customer.Lastname = userVM.Lastname;
            customer.Street = userVM.Street;
            customer.ZipCode = userVM.ZipCode;
            customer.City = userVM.City;
            return customer;
        }
        public static CustomerVM Mapping(Customer customer)
        {
            CustomerVM uservm = new CustomerVM();
            uservm.Id = customer.Id;
            uservm.EmailAddress = customer.EmailAddress;
            uservm.Password = customer.PasswordHash;
            uservm.Salutation = customer.Salutation;
            uservm.Firstname = customer.Firstname;
            uservm.Lastname = customer.Lastname;
            uservm.Street = customer.Street;
            uservm.ZipCode = customer.ZipCode;
            uservm.City = customer.City;
            return uservm;
        }
        internal static bool IsUserValid(Customer customer)
        {
            using (var db = new MyContext())
            {
                Customer customerDB = db.Customers.SingleOrDefault(x => x.EmailAddress == customer.EmailAddress);

                if (customerDB == null)
                    return false;

                if (customerDB.PasswordHash != Hash(customer.PasswordHash + customerDB.PasswordSalt.ToString()))
                    return false;

                return true;
            }
        }
        public static string Hash(string input)
        {
            byte[] bytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }


    }
}
