
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Windows.ApplicationModel.Activation;
using SIKONClassLibrary.EventHandlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIKONClassLibrary;

namespace SIKONUnitTest
{
    [TestClass]
    public class UnitTest1
    {
       
        [TestMethod]
        public void TestRead()
        {
            //Arrange
            List<Event> list = new List<Event>();
            //Act
            list = new EventsHandler().Read();
            //Assert
            Assert.AreNotEqual(0, list.Count);
        }

        [TestMethod]
        public void TestReadFrom()
        {
            //Arrange
            Event ev;
            //Act
            ev = new EventsHandler().ReadFrom(1);
            //Assert
            Assert.IsTrue(ev != null);
        }

        [TestMethod]
        public void TestCreateAndDelete()
        {
            //Arrange
            Event ev = new Event();
            ev.Subject = "UnitTest Event";
            bool isMade = false;
            Event ev3 = null;
            int countBefore;
            int countAfter;

            //Act
            new EventsHandler().Create(ev);

            List<Event> list = new EventsHandler().Read();
            countBefore = list.Count;
            foreach (var ev2 in list)
            {
                if (ev2.Equals(ev))
                {
                    isMade = true;
                    ev3 = ev2;
                }
            }
            if (ev3 != null) new EventsHandler().Delete(ev3.ID);
            countAfter = new EventsHandler().Read().Count;

            //Assert
            Assert.AreEqual(countBefore-1, countAfter);
        }

        [TestMethod]
        public void TestUpdate()
        {
            //Arrange
            Event ev = new EventsHandler().ReadFrom(4);
            int subject = int.Parse(ev.Subject);
            subject++;
            ev.Subject = $"{subject}";

            //Act
            new EventsHandler().Update(ev, 4);

            //Assert
            Assert.AreEqual(ev, new EventsHandler().ReadFrom(4));
        }
    }
}
