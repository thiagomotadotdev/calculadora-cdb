namespace tmdn.CalculadoraCdb.Application.Ports.Cdb
{
    public interface ICdbPort
    {
        decimal ObterPorcentagemImposto(int qtdMeses);

        decimal ObterCdi(string mesFinal);
    }
}
