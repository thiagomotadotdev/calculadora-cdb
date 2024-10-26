using System;
using System.Text.RegularExpressions;
using tmdn.CalculadoraCdb.Application.Ports.Cdb;

namespace tmdn.CalculadoraCdb.Adapters.CdbDatabase.Repositories
{
    public class CdbRepository : ICdbPort
    {
        public CdbRepository() { }

        public decimal ObterPorcentagemImposto(int qtdMeses)
        {
            if (qtdMeses <= 0)
                throw new Exception("Por favor, informe uma quantidade de meses maior que 0");

            if (qtdMeses <= 6)
                return 0.225m;

            if (qtdMeses <= 12)
                return 0.2m;

            if (qtdMeses <= 24)
                return 0.175m;

            return 0.15m;
        }

        public decimal ObterCdi(string mesFinal)
        {
            if (Regex.Match(mesFinal, "^\\d{4}-\\d{2}$").Success)
                return 0.009m;

            throw new Exception("Por favor, informe o mês e ano no formato: YYYY-MM");
        }
    }
}
