using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.Model;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class AdminViewModel : ViewModelBase
    {

        Admin admin;
        Entity context = new Entity();

        public AdminViewModel(Admin adminOpen)
        {
            admin = adminOpen;
            //geting data from database to list
            OrderList = GetOrders();
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
        private tblOrder order;
        public tblOrder Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
                OnPropertyChanged("Order");
            }
        }
        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                if (delete == null)
                {
                    delete = new RelayCommand(param => DeleteExecute(), param => CanDeleteExecute());
                }
                return delete;
            }
        }
        private void DeleteExecute()
        {
            try
            {
                //deleting order
                tblOrder orderToDelete = (from r in context.tblOrders where r.OrderID == Order.OrderID select r).FirstOrDefault();
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure? Order will be deleted", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    context.tblOrders.Remove(orderToDelete);
                    context.SaveChanges();
                    //refreshing list afterwards
                    OrderList = GetOrders();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDeleteExecute()
        {
            //if order is selected
            if (Order != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private ICommand reject;
        public ICommand Reject
        {
            get
            {
                if (reject == null)
                {
                    reject = new RelayCommand(param => RejectExecute(), param => CanRejectExecute());
                }
                return reject;
            }
        }
        /// <summary>
        /// Changes status to rejected
        /// </summary>
        private void RejectExecute()
        {
            try
            {
                //findifn order to modify
                tblOrder orderToReject = (from r in context.tblOrders where r.OrderID == Order.OrderID select r).FirstOrDefault();
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure? Order will be rejected", "Reject Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    orderToReject.OrderStatus = "Rejected";
                    context.SaveChanges();
                    //refreshing list afterwards
                    OrderList = GetOrders();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanRejectExecute()
        {
            //if order is selected
            if (Order != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private ICommand approve;
        public ICommand Approve
        {
            get
            {
                if (approve == null)
                {
                    approve = new RelayCommand(param => ApproveExecute(), param => CanApproveExecute());
                }
                return approve;
            }
        }
        /// <summary>
        /// changes status to approved
        /// </summary>
        private void ApproveExecute()
        {
            try
            {
                //finding order to aprove
                tblOrder orderToApprove = (from r in context.tblOrders where r.OrderID == Order.OrderID select r).FirstOrDefault();
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure? Order will be approved", "Approve Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    orderToApprove.OrderStatus = "Approved";
                    context.SaveChanges();
                    //saving change and refresing list afterwards
                    OrderList = GetOrders();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanApproveExecute()
        {
            //is order is selected
            if (Order != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private ICommand close;
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
            admin.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }
        /// <summary>
        /// method for taking orders from database to list and than showing that list in datagrid
        /// </summary>
        /// <returns></returns>
        private List<tblOrder> GetOrders()
        {
            List<tblOrder> list = new List<tblOrder>();

            list = context.tblOrders.ToList();
            return list;
        }
    }
}
