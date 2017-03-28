using Microsoft.VisualStudio.TestTools.UnitTesting;
using AyD_P2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
            var resultado = o.verificarInserciónTransferencia("11111", "x", "transferencia","1");

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

        /*[TestMethod()]
        public void verificarInsercionRegistroTest()
        {
            AccountController o = new AccountController();

            bool esperado = true;
            var resultado = o.verificarInserciónRegistro("d", "123123", 5);

            Assert.AreEqual(esperado, resultado);
        }*/

        [TestMethod()]
        public void devuelveSaldoTest()
        {
            OperacionController o = new OperacionController();
            var cliente = 1;
            var saldo = (_db.CUENTA.Where(x => x.cod_cliente == cliente).FirstOrDefault()).saldo.ToString();
            var resultado = o.devuelveSaldo(cliente, "0");

            Assert.AreEqual(saldo, resultado);
        }

        [TestMethod()]
        public void verificarSaldoTransferenciaTest()
        {
            OperacionController o = new OperacionController();

            bool esperado = true;
            var resultado = o.verificarSaldoTransferencia(1, "200");

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod()]
        public void devuelveCodigoUsuarioTest()
        {
            AccountController o = new AccountController();
            var usuario = "nat";
            var pass = "123123";
            var codigo = (_db.USUARIO.Where(x => x.usuario1 == usuario && x.contrasenia == pass).FirstOrDefault()).cod_cliente;
            var resultado = o.retornarCodigoUsuario(usuario, pass);

            Assert.AreEqual(codigo, resultado);
        }

        [TestMethod()]
        public void existeUsuarioTest()
        {
            AccountController o = new AccountController();

            bool esperado = true;
            var resultado = o.existeUsuario(1,"nat", "123123");

            Assert.AreEqual(esperado, resultado);
        }

    }
}