using System;
using Sage.Connector.Customers.Contracts.Data;
using Sage.Connector.DomainContracts.Data;

namespace BlobUploadTester
{
    public static class CustomerUtil
    {

        public static Customer CreateCustomer(string prefix,int index)
        {
            var customer = new Customer
            {
                CreditLimit = 1000 + index,
                Name = "CustomerName-" + prefix + GetIndexString(index),
                CreditAvailable = 100 + index,
                IsCreditLimitUsed = false,
                IsOnCreditHold = false,
                PaymentTerms = "SOMEDAY",
                EntityStatus = EntityStatus.Active,
                ExternalId = Guid.NewGuid().ToString(),
                ExternalReference = Guid.NewGuid().ToString(),

            };

            var contact = CreateContact(prefix,index);
            customer.Contacts.Add(contact);
            customer.Addresses.Add(CreateAddress(prefix,index));
            customer.TaxSchedule = "SomeSchedule";
            customer.TaxClasses.Add(CreateTaxClass(index));

            return customer;
        }



        private static CustomerAddress CreateAddress(string prefix, int index)
        {
            return new CustomerAddress
            {
                Name = "SomeName-" + prefix + GetIndexString(index),
                City = "SomeCity-" + prefix + GetIndexString(index),
                Email = "address@email.com",
                //    Customer = customer,
                Country = "The good 'ol USA",
                //   Url = "address.url.com",
                Phone = "555-555-5555",
                PostalCode = "90210",
                StateProvince = "CA",
                Street1 = index + " Huckleberry street",
                Street2 = "in the lane",
                Street3 = "around the corner",
                Street4 = "does anyone have an address this long?",
                Type = AddressType.Billing,
                ExternalId = Guid.NewGuid().ToString(),
                ExternalReference = Guid.NewGuid().ToString(),

            };
        }

        private static CustomerContact CreateContact(string prefix, int index)
        {
            return new CustomerContact
            {
                EmailWork = "me@work.com",
                EmailPersonal = "me@personal.com",
                FirstName = "MyFirstName-" + prefix+GetIndexString(index),
                LastName = "MyLastName-"  +prefix+GetIndexString(index),
                Title = "My Title",
                PhoneHome = "604-555-5555",
                PhoneMobile = "778-555-5555",
                PhoneWork = "250-555-5555",
                Url = "my.url.com",
                ExternalId = Guid.NewGuid().ToString(),
                ExternalReference = Guid.NewGuid().ToString(),
            };
        }

        private static CustomerTaxClass CreateTaxClass(int index)
        {
            return new CustomerTaxClass
            {
                TaxClass = "TaxClass" + index,
                TaxCode = "TaxCode" + index,
            };
        }

        private static string GetIndexString(int index)
        {
            return "-"+index.ToString("D10");
        }
    }
}