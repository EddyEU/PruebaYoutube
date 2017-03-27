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

        [TestMethod()]
        public void verificarInsercionTransferenciaTest()
        {
            OperacionController o = new OperacionController();

            bool esperado = true;
            var resultado = o.verificarInserciónTransferencia("11111", "2.00", "transferencia","1");

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod()]
        public void verificarInsercionServicioTest()
        {
            OperacionController o = new OperacionController();

            bool esperado = true;
            var resultado = o.verificarInserciónServicio("Luz","11111", "2.00", "Servicio", 1);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod()]
        public void verificarInsercionRegistroTest()
        {
            AccountController o = new AccountController();

            bool esperado = true;
            var resultado = o.verificarInserciónRegistro("d", "123123", 5);

            Assert.AreEqual(esperado, resultado);
        }


    }
}