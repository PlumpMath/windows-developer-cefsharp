using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;

namespace CustomerManagement
{
    public class CustomerServiceBridge
    {
        private readonly CustomerService _customerService;
        private readonly ChromiumWebBrowser _browser;

        public event Action<int> ItemSelected; 

        public CustomerServiceBridge(CustomerService customerService, ChromiumWebBrowser browser)
        {
            _customerService = customerService;
            _browser = browser;

            _customerService.ItemAdded += CustomerServiceOnItemAdded;

            browser.RegisterJsObject("customerService", this);
        }

        private void CustomerServiceOnItemAdded(object sender, EventArgs eventArgs)
        {
            _browser.ExecuteScriptAsync("window.customerService.itemAdded();");
        }

        public string List()
        {
            return JsonConvert.SerializeObject(_customerService.List());
        }

        public void SelectItem(int id)
        {
            OnItemSelected(id);
        }

        protected virtual void OnItemSelected(int obj)
        {
            ItemSelected?.Invoke(obj);
        }
    }
}