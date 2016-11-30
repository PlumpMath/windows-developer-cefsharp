using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CustomerManagement
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class CustomerService
    {
        private readonly ICollection<Customer> _customers = new List<Customer>();
        private int _id;

        public event EventHandler ItemAdded;

        public void Add(string firstName, string lastName)
        {
            _customers.Add(new Customer()
            {
                Id = ++_id,
                FirstName = firstName,
                LastName = lastName
            });
            OnItemAdded();
        }

        public Customer Get(int id)
        {
            return _customers.FirstOrDefault(c => c.Id == id);
        }

        public ICollection<Customer> List()
        {
            return _customers;
        }

        protected virtual void OnItemAdded()
        {
            ItemAdded?.Invoke(this, EventArgs.Empty);
        }
    }
}