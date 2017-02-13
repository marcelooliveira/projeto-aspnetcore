using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Carrinho.Core.Test
{
    public class DescontoManagerTest
    {
        public DescontoManager descontoManager;

        public DescontoManagerTest()
        {
            descontoManager = new DescontoManager();
        }

        [Fact]
        public void GetDesconto_0_Deve_Retornar_Taxa_0_And_Valor_0()
        {
            var rule = descontoManager.GetDesconto(0);
            Assert.Equal(0, rule.Taxa);
            Assert.Equal(0, rule.DescontoCalculado);
        }

        [Fact]
        public void GetDesconto_499_99_Deve_Retornar_Taxa_0_And_Valor_0()
        {
            var rule = descontoManager.GetDesconto(499.99M);
            Assert.Equal(0, rule.Taxa);
            Assert.Equal(0, rule.DescontoCalculado);
        }

        [Fact]
        public void GetDesconto_500M_Deve_Retornar_Taxa_5_And_Valor_25()
        {
            var rule = descontoManager.GetDesconto(500M);
            Assert.Equal(.05M, rule.Taxa);
            Assert.Equal(25, rule.DescontoCalculado);
        }

        [Fact]
        public void GetDesconto_599_99M_Deve_Retornar_Taxa_5_And_Valor_25()
        {
            var rule = descontoManager.GetDesconto(599.99M);
            Assert.Equal(.05M, rule.Taxa);
            Assert.Equal(30M, rule.DescontoCalculado);
        }

        [Fact]
        public void GetDesconto_600M_Deve_Retornar_Taxa_10_And_Valor_60()
        {
            var rule = descontoManager.GetDesconto(600M);
            Assert.Equal(.10M, rule.Taxa);
            Assert.Equal(60M, rule.DescontoCalculado);
        }

        [Fact]
        public void GetDesconto_699_99M_Deve_Retornar_Taxa_10_And_Valor_70M()
        {
            var rule = descontoManager.GetDesconto(699.99M);
            Assert.Equal(.10M, rule.Taxa);
            Assert.Equal(70M, rule.DescontoCalculado);
        }

        [Fact]
        public void GetDesconto_700M_Deve_Retornar_Taxa_15_And_Valor_105()
        {
            var rule = descontoManager.GetDesconto(700M);
            Assert.Equal(.15M, rule.Taxa);
            Assert.Equal(105M, rule.DescontoCalculado);
        }

        [Fact]
        public void GetDesconto_10000M_Deve_Retornar_Taxa_15_And_Valor_1500()
        {
            var rule = descontoManager.GetDesconto(10000M);
            Assert.Equal(.15M, rule.Taxa);
            Assert.Equal(1500M, rule.DescontoCalculado);
        }
    }
}
