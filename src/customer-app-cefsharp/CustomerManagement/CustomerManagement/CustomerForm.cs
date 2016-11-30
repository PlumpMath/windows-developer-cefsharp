using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;

namespace CustomerManagement
{
    public partial class CustomerForm : Form
    {
        private readonly CustomerService _customerService;
        private ChromiumWebBrowser _browser;

        public CustomerForm()
        {
            InitializeComponent();

            _customerService = new CustomerService();
        }

        private void InitializeBrowser()
        {
            var settings = new CefSettings();
            settings.SetOffScreenRenderingBestPerformanceArgs();

            Cef.Initialize(settings);

            var startPage = String.Format(@"{0}\web\index.html", Application.StartupPath);
            _browser = new ChromiumWebBrowser("about:blank");
            _browser.Dock = DockStyle.Fill;
            _browser.BrowserSettings = new BrowserSettings()
            {
                OffScreenTransparentBackground = false
            };

            cefHost.Controls.Add(_browser);

            var bridge = new CustomerServiceBridge(_customerService, _browser);
            bridge.ItemSelected += BridgeOnItemSelected;

            _browser.Load(startPage);
        }

        private void BridgeOnItemSelected(int i)
        {
            var customer = _customerService.Get(i);

            Invoke((MethodInvoker) delegate
            {
                firstName.Text = customer.FirstName;
                lastName.Text = customer.LastName;
            });
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

        private void CustomerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
