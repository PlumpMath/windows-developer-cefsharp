using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerManagement
{
    public partial class CustomerForm : Form
    {
        private readonly CustomerService _customerService;

        public CustomerForm()
        {
            InitializeComponent();

            _customerService = new CustomerService();
            _customerService.ItemAdded += ServiceOnItemAdded;
        }

        private void ServiceOnItemAdded(object sender, EventArgs e)
        {
            customers.Items.Clear();

            foreach (var customer in _customerService.List())
            {
                customers.Items.Add($"{customer.Id} {customer.FirstName} {customer.LastName}");
            }
        }

        private void addCustomer_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(firstName.Text) || String.IsNullOrWhiteSpace(lastName.Text))
            {
                return;
            }

            _customerService.Add(firstName.Text, lastName.Text);

            firstName.Text = "";
            lastName.Text = "";
        }

        private void customers_Click(object sender, EventArgs e)
        {
            var selection = (string) customers.SelectedItem;

            if (String.IsNullOrWhiteSpace(selection))
            {
                return;
            }

            var id = Int32.Parse(selection.Split(' ')[0]);
            var customer = _customerService.Get(id);

            firstName.Text = customer.FirstName;
            lastName.Text = customer.LastName;
        }
    }
}
