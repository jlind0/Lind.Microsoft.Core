using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lind.Core.Tests
{
    public class Audit
    {
        public Audit(Person auditor)
        {
            this.Auditor += auditor;
        }
        public NonNullReference<Person> Auditor { get; set; }
    } 
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Audit audit = new Audit(new Person() { FirstName = "James", LastName = "Bond"});
            audit.Auditor += new Person() { FirstName = "Jason", LastName = "Lind" };
        }
    }
}
