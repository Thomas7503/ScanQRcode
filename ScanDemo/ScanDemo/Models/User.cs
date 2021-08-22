using System;
using System.Collections.Generic;
using System.Text;

namespace ScanDemo.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Code { get; set; }
        public List<Item> Items { get; set; }


        public User(string mail, string pass)
        {
            this.Email = mail;
            this.Password = pass;
        }

        public User()
        {
        }
    }
}
