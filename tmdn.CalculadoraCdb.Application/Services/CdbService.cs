using System;
using tmdn.CalculadoraCdb.Application.Models.Cdb;
using tmdn.CalculadoraCdb.Application.Ports.Cdb;

namespace tmdn.CalculadoraCdb.Application.Services
{
    public class CdbService
    {
        private readonly ICdbPort cdbImpostoPort;

        public CdbService(ICdbPort cdbImpostoPort)
        {
            this.cdbImpostoPort = cdbImpostoPort;
        }

        public ResultadoCalculoCdb Calcular(decimal valorInicial, int qtdMeses)
        {
            if (valorInicial <= 0)
                throw new Exception("Por favor, informe um valor maior que 0");

            if (qtdMeses <= 0)
                throw new Exception("Por favor, informe uma quantidade de meses maior que 0");

            if (valorInicial > 1000000000m)
                throw new Exception("O valor inicial não pode ser maior que R$ 1.000.000.000");

            if (qtdMeses > 1000)
                throw new Exception("A quantidade de meses não pode ser maior que 1000");



            ResultadoCalculoCdb rcc = new ResultadoCalculoCdb();

            CalcularValorBruto(rcc, valorInicial, qtdMeses);
            CalcularValorLiquido(rcc, valorInicial, cdbImpostoPort.ObterPorcentagemImposto(qtdMeses));

            /* 
                não foi informado o critério de arredondamento,
                como normalmente o cliente é sempre o beneficiado foi implementado o arredondamento sempre para cima.
            */

            rcc.Bruto = decimal.Ceiling(rcc.Bruto * 100) / 100m;
            rcc.Liquido = decimal.Ceiling(rcc.Liquido * 100) / 100m;

            return rcc;
        }

        private static void CalcularValorBruto(ResultadoCalculoCdb rcc, decimal valorInicial, int qtdMeses)
        {
            rcc.Bruto = valorInicial;

            for (int i = 0; i < qtdMeses; i++)
            {
                rcc.Bruto *= 1m + (0.009m * 1.08m);
            }
        }

        private static void CalcularValorLiquido(ResultadoCalculoCdb rcc, decimal valorInicial, decimal imposto)
        {
            rcc.Liquido = rcc.Bruto - ((rcc.Bruto - valorInicial) * imposto);
        }
    }
}
