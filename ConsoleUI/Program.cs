using Business.Concrete;
using ConsoleUI.Interfaces;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // carManager is from business layer
            // UI sends data access layer reference to carManager
            // carManager uses DAL methods fo manipulate data

            //CarManager carManager = new CarManager(new InMemoryCarDal());
            MainUI mainUI = new MainUI();
            BrandUI brandUI = new BrandUI();
            CarUI carUI = new CarUI();
            UserUI userUI = new UserUI();

            List<IUserInterface> userInterfaces = new List<IUserInterface> { brandUI, carUI, userUI };

            mainUI.Show(userInterfaces);
        }                
    }
}


//Müşteriler tablosu oluşturunuz. Customers-->UserId, CompanyName