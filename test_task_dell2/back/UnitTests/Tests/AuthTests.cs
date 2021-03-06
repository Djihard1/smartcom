using AutoMapper.Configuration;
using BL.Repository_;
using BL.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Tests
{
    [TestClass]
    class AuthTests
  
    {
        // Arrange

             
       
     
        Customer Customer = new Customer()
        {
            Login = "testLogin",
            Role = "Manager",
            ADDRESS = "Test Adress",
            CODE = "TestCode",
            NAME = "testName",
            DISCOUNT = 20,
        };
        string login = "testUser";
        string password = "password";

        // Act
        







    }
}
