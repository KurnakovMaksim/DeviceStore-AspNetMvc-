﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DeviceStore.Domain.AbstractModel;
using DeviceStore.Domain.Entities;
using DeviceStore.WebUI.Controllers;
using DeviceStore.WebUI.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DeviceStore.WebUI.Tests
{
    [TestClass]
    public class CategoryTest
    {
        [TestMethod]
        public void CanFilterDevicesByCategory()
        {
            Mock<IDeviceRepository> mock = new Mock<IDeviceRepository>();

            mock.Setup(m => m.Devices).Returns(new List<Device>
            {
                new Device {DeviceId = 1, DeviceName = "Device", DeviceCategory = "Category"},
                new Device {DeviceId = 2, DeviceName = "1212Device", DeviceCategory = "Category"},
                new Device {DeviceId = 3, DeviceName = "322", DeviceCategory = "Cat"},
            });
            HomeController controller = new HomeController(mock.Object);

            List<Device> action = ((FilteredDevicesList)controller.Index("Category", 1).Model).Devices.ToList();

            Assert.AreEqual(action.Count(), 2);

            Assert.IsTrue(action[0].DeviceName == "Device" && action[0].DeviceCategory == "Category");
            Assert.IsFalse(action[1].DeviceName == "1212Device" && action[1].DeviceCategory == "Cat2");
        }

        [TestMethod]
        public void CanCreateCategories()
        {
            Mock<IDeviceRepository> mock = new Mock<IDeviceRepository>();

            mock.Setup(m => m.Devices).Returns(new List<Device>
            {
                new Device {DeviceId = 1, DeviceName = "Device1", DeviceCategory = "ПК"},
                new Device {DeviceId = 2, DeviceName = "Device2", DeviceCategory = "ПК"},
                new Device {DeviceId = 3, DeviceName = "Device3", DeviceCategory = "Смарт часы"},
                new Device {DeviceId = 4, DeviceName = "Device4", DeviceCategory = "Нетбуки"},
            });
            NavigationController controller = new NavigationController(mock.Object);

            List<string> results = ((IEnumerable<string>)controller.CategoryMenu().Model).ToList();

            Assert.AreEqual(results.Count(), 3);
            Assert.AreEqual(results[0], "Нетбуки");
            Assert.AreEqual(results[1], "ПК");
            Assert.AreEqual(results[2], "Смарт часы");
        }
    }
}