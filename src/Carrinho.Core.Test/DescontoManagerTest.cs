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
        public void GetDiscount_0_Should_Return_Rate_0_And_Value_0()
        {
            var rule = descontoManager.GetDesconto(0);
            Assert.Equal(0, rule.Rate);
            Assert.Equal(0, rule.DescontoCalculado);
        }

        [Fact]
        public void GetDiscount_499_99_Should_Return_Rate_0_And_Value_0()
        {
            var rule = descontoManager.GetDesconto(499.99M);
            Assert.Equal(0, rule.Rate);
            Assert.Equal(0, rule.DescontoCalculado);
        }

        [Fact]
        public void GetDiscount_500M_Should_Return_Rate_5_And_Value_25()
        {
            var rule = descontoManager.GetDesconto(500M);
            Assert.Equal(.05M, rule.Rate);
            Assert.Equal(25, rule.DescontoCalculado);
        }

        [Fact]
        public void GetDiscount_599_99M_Should_Return_Rate_5_And_Value_25()
        {
            var rule = descontoManager.GetDesconto(599.99M);
            Assert.Equal(.05M, rule.Rate);
            Assert.Equal(30M, rule.DescontoCalculado);
        }

        [Fact]
        public void GetDiscount_600M_Should_Return_Rate_10_And_Value_60()
        {
            var rule = descontoManager.GetDesconto(600M);
            Assert.Equal(.10M, rule.Rate);
            Assert.Equal(60M, rule.DescontoCalculado);
        }

        [Fact]
        public void GetDiscount_699_99M_Should_Return_Rate_10_And_Value_70M()
        {
            var rule = descontoManager.GetDesconto(699.99M);
            Assert.Equal(.10M, rule.Rate);
            Assert.Equal(70M, rule.DescontoCalculado);
        }

        [Fact]
        public void GetDiscount_700M_Should_Return_Rate_15_And_Value_105()
        {
            var rule = descontoManager.GetDesconto(700M);
            Assert.Equal(.15M, rule.Rate);
            Assert.Equal(105M, rule.DescontoCalculado);
        }

        [Fact]
        public void GetDiscount_10000M_Should_Return_Rate_15_And_Value_1500()
        {
            var rule = descontoManager.GetDesconto(10000M);
            Assert.Equal(.15M, rule.Rate);
            Assert.Equal(1500M, rule.DescontoCalculado);
        }
    }
}
