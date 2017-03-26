using Microsoft.VisualStudio.TestTools.UnitTesting;
using AyD_P2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyD_P2.Controllers.Tests
{
    [TestClass()]
    public class OperacionControllerTests
    {
        ModeloDBEntities _db = new ModeloDBEntities();

        [TestMethod()]
        public void existeSaldoTest()
        {

            OperacionController o = new OperacionController();

            bool esperado = true;
            var resultado = o.existeSaldo(1, "100");

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod()]
        public void verificarCuentaDebitoTest()
        {
            OperacionController o = new OperacionController();

            bool esperado = true;
            var resultado = o.verificarCuentaDebito("02220");

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod()]
        public void verificarSaldoCuentaDebitoTest()
        {
            OperacionController o = new OperacionController();

            bool esperado = true;
            var resultado = o.verificarSaldoCuentaDebito(1, "200");

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod()]
        public void verificarSaldoCuentaCreditoTest()
        {
            OperacionController o = new OperacionController();

            bool esperado = true;
            var resultado = o.verificarSaldoCuentaCredito(1, "100");

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod()]
        public void verificarCuentaCreditoTest()
        {
            OperacionController o = new OperacionController();

            bool esperado = true;
            var resultado = o.verificarCuentaCredito("71114");

            Assert.AreEqual(esperado, resultado);
        }

        /*[TestMethod()]
        public void devuelveSaldoTest()
        {
            OperacionController o = new OperacionController();

            bool esperado = true;
            var resultado = o.devuelveSaldo(5, "");

            Assert.AreEqual(esperado, resultado);
        }*/



















        /*
        [TestMethod()]
        public void IndexTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ServicioTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ServicioTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void TransferenciaTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void TransferenciaTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ConsultaTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreditoTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreditoTest1()
        {
            Assert.Fail();
        }
        [TestMethod()]
        public void DebitoTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DebitoTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void OperacionesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LogoutTest()
        {
            Assert.Fail();
        }
        

        [TestMethod()]
        public void agregaTransferenciaTest()
        {
            Assert.Fail();
        }*/
    }
}