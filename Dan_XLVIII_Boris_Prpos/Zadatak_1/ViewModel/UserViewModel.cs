﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.Model;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class UserViewModel : ViewModelBase
    {
        //Creating object that will conect Model and ViewModel (using Entity Framework)
        static Entity context = new Entity();
        User uvm;
        MainWindowViewModel mvvm = new MainWindowViewModel();

        public UserViewModel(User uvmOpen, string username)
        {
            uvm = uvmOpen;
            tblorder = new tblOrder();
            //taking JMBG that was forwarded from login form
            tblorder.CustomerJMBG = username;
            //filling list with data from database
            OrderList = GetOrders();
            //calling function that will show order status for 2 seconds and than that messagebox will be closed
            Show();
        }
        public UserViewModel(User user)
        {
            this.user = user;
        }
        /// <summary>
        /// Funtion that shows order status for every user and than closes mesagebox after 2s
        /// </summary>
        public void Show()
        {
            List<string> dateAndStatus = new List<string>();
            List<tblOrder> tblOrders = context.tblOrders.ToList();

            //getting date and status for every order 
            foreach (var item in tblOrders)
            {
                if (item.CustomerJMBG == tblOrder.CustomerJMBG)
                {
                    dateAndStatus.Add(item.OrderDate + " " + item.OrderStatus + "\n");
                }
            }

            string total = " ";
            //putting everything from the list into one string => easier to display in mesagebox
            foreach (string item in dateAndStatus)
            {
                total += item + " ";
            }
            //aditional message after orders
            total += "\nThis message box will be closed automaticaly after 2s";

            //show mesage box only if there are orders for that user
            if (dateAndStatus.Count != 0)
            {
                //using autoclosingmesagebox class =>allows messagebox to close after specified time
                AutoClosingMessageBox.Show(total, "Orders status", 2000);
            }
        }

        private tblOrder tblorder;
        public tblOrder tblOrder
        {
            get
            {
                return tblorder;
            }
            set
            {
                tblorder = value;
                OnPropertyChanged("tblOrder");
            }
        }
        private List<tblOrder> orderList;
        public List<tblOrder> OrderList
        {
            get
            {
                return orderList;
            }
            set
            {
                orderList = value;
                OnPropertyChanged("OrderList");
            }
        }
        private int bigPizza;
        public int BigPizza
        {
            get
            {
                return bigPizza;
            }
            set
            {
                bigPizza = value;
                OnPropertyChanged("BigPizza");
            }
        }
        private int mediumPizza;
        public int MediumPizza
        {
            get
            {
                return mediumPizza;
            }
            set
            {
                mediumPizza = value;
                OnPropertyChanged("MediumPizza");
            }
        }
        private int smallPizza;
        public int SmallPizza
        {
            get
            {
                return smallPizza;
            }
            set
            {
                smallPizza = value;
                OnPropertyChanged("SmallPizza");
            }
        }
        private int familyPizza;
        public int FamilyPizza
        {
            get
            {
                return familyPizza;
            }
            set
            {
                familyPizza = value;
                OnPropertyChanged("FamilyPizza");
            }
        }
        private int specialPizza;
        public int SpecialPizza
        {
            get
            {
                return specialPizza;
            }
            set
            {
                specialPizza = value;
                OnPropertyChanged("SpecialPizza");
            }
        }
        //Taking values that represent price for every meal in database
        static tblPrice bigPizzaMeal = (from r in context.tblPrices where r.Meal == "BigPizza" select r).First();
        int bigpizzaCost = bigPizzaMeal.Price.GetValueOrDefault();

        static tblPrice mediumPizzaMeal = (from r in context.tblPrices where r.Meal == "MediumPizza" select r).First();
        int mediumpizzaCost = mediumPizzaMeal.Price.GetValueOrDefault();

        static tblPrice smallPizzaMeal = (from r in context.tblPrices where r.Meal == "SmallPizza" select r).First();
        int smallpizzaCost = smallPizzaMeal.Price.GetValueOrDefault();

        static tblPrice familyPizzaMeal = (from r in context.tblPrices where r.Meal == "FamilyPizza" select r).First();
        int familypizzaCost = familyPizzaMeal.Price.GetValueOrDefault();

        static tblPrice specialPizzaMeal = (from r in context.tblPrices where r.Meal == "SpecialPizza" select r).First();
        int speicalpizzaCost = specialPizzaMeal.Price.GetValueOrDefault();

        //property that represents total cost =>based on input from every text box
        private int totalAmount;
        public int TotalAmount
        {
            get
            {
                return BigPizza * bigpizzaCost + MediumPizza * mediumpizzaCost + SmallPizza * smallpizzaCost + FamilyPizza * familypizzaCost + SpecialPizza * speicalpizzaCost;
            }
            set
            {
                totalAmount = value;
                OnPropertyChanged("TotalAmount");
            }
        }

        private ICommand order;
        public ICommand Order
        {
            get
            {
                if (order == null)
                {
                    order = new RelayCommand(param => OrderExecute(), param => CanOrderExecute());
                }
                return order;
            }
        }
        //method that saves order into database
        private void OrderExecute()
        {
            try
            {
                tblOrder newOrder = new tblOrder();
                //values from text boxes
                newOrder.BigPizza = BigPizza;
                newOrder.SmallPizza = SmallPizza;
                newOrder.MediumPizza = MediumPizza;
                newOrder.FamilyPizza = FamilyPizza;
                newOrder.SpecialPizza = SpecialPizza;
                //date and time
                string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                newOrder.OrderDate = text;
                //jmbg that was forwarded into constructor (from login window)
                newOrder.CustomerJMBG = tblorder.CustomerJMBG;
                //status automaticaly set to waiting
                newOrder.OrderStatus = "Waiting";
                newOrder.TotalAmount = TotalAmount;

                context.tblOrders.Add(newOrder);
                context.SaveChanges();
                MessageBox.Show("Order is waiting for approval.");
                OrderList = GetOrders();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanOrderExecute()
        {
            if (EverythingEmpty() == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //method checks if every textbox is empty=>if true=>cant save
        private bool EverythingEmpty()
        {
            if (BigPizza == 0 && MediumPizza == 0 && SmallPizza == 0 && SpecialPizza == 0 && FamilyPizza == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private ICommand close;
        private User user;

        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }
        private void CloseExecute()
        {
            uvm.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }
        /// <summary>
        /// Method takes rows from sql and puts them into list
        /// </summary>
        /// <returns></returns>
        private List<tblOrder> GetOrders()
        {
            List<tblOrder> list = new List<tblOrder>();

            list = context.tblOrders.ToList();

            List<tblOrder> ByUser = new List<tblOrder>();

            foreach (tblOrder item in list)
            {
                if (item.CustomerJMBG == tblorder.CustomerJMBG)
                {
                    ByUser.Add(item);
                }
            }
            return ByUser;
        }
    }
    /// <summary>
    /// Class that allows MessageBox to timeout
    /// </summary>
    public class AutoClosingMessageBox
    {
        System.Threading.Timer _timeoutTimer;
        string _caption;
        AutoClosingMessageBox(string text, string caption, int timeout)
        {
            _caption = caption;
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                null, timeout, System.Threading.Timeout.Infinite);
            MessageBox.Show(text, caption);
        }
        public static void Show(string text, string caption, int timeout)
        {
            new AutoClosingMessageBox(text, caption, timeout);
        }
        void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow(null, _caption);
            if (mbWnd != IntPtr.Zero)
                SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            _timeoutTimer.Dispose();
        }
        const int WM_CLOSE = 0x0010;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
    }
}
