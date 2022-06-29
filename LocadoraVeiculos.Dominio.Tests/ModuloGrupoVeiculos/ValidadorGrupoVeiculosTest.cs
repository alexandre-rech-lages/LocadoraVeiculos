﻿using LocadoraVeiculos.Dominio.ModuloGrupoVeiculos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocadoraVeiculos.Dominio.Tests.ModuloGrupoVeiculos
{
    [TestClass]
    public class ValidadorGrupoVeiculosTest
    {
        [TestMethod]
        public void Nome_deve_ser_obrigatorio()
        {
            //arrange
            var grupoVeiculos = new GrupoVeiculos();
            grupoVeiculos.Nome = null;

            ValidadorGrupoVeiculos validador = new();

            //action
            var resultado = validador.Validate(grupoVeiculos);

            //assert
            Assert.AreEqual("O campo 'Nome' é obrigatório!", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_nao_deve_ter_caracteres_especiais()
        {
            //arrange
            var grupoVeiculos = new GrupoVeiculos();
            grupoVeiculos.Nome = "Uber#@";

            ValidadorGrupoVeiculos validador = new();

            //action
            var resultado = validador.Validate(grupoVeiculos);

            //assert
            Assert.AreEqual("Caracteres especiais não são permitidos!", resultado.Errors[0].ErrorMessage);
        }
    }
}