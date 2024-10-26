using tmdn.CalculadoraCdb.Adapters.CdbDatabase.Repositories;
using tmdn.CalculadoraCdb.Application.Services;

namespace tmdn.CalculadoraCdb.Application.Tests
{
    public class CdbServiceTest
    {
        [Fact]
        public void ValoresNegativos()
        {
            var repository = new CdbRepository();
            var service = new CdbService(repository);

            _ = Assert.Throws<Exception>(() => service.Calcular(-1, -1));
            _ = Assert.Throws<Exception>(() => service.Calcular(-1, 1));
            _ = Assert.Throws<Exception>(() => service.Calcular(1, -1));
            _ = Assert.Throws<Exception>(() => service.Calcular(0, 0));
            _ = Assert.Throws<Exception>(() => service.Calcular(0, 1));
            _ = Assert.Throws<Exception>(() => service.Calcular(1, 0));
        }

        [Fact]
        public void ValoresMuitoAltos()
        {
            var repository = new CdbRepository();
            var service = new CdbService(repository);

            Assert.NotNull(() => service.Calcular(1, 1000));
            _ = Assert.Throws<Exception>(() => service.Calcular(1, 1001));
            Assert.NotNull(() => service.Calcular(1000000000m, 1));
            _ = Assert.Throws<Exception>(() => service.Calcular(1000000000.01m, 1));
        }

        [Fact]
        public void Resultados()
        {
            var repository = new CdbRepository();
            var service = new CdbService(repository);

            var resultado = service.Calcular(1000, 12);
            Assert.Equal(1123.09m, resultado.Bruto);
            Assert.Equal(1098.47m, resultado.Liquido);

            resultado = service.Calcular(14320, 53);
            Assert.Equal(23910.79m, resultado.Bruto);
            Assert.Equal(22472.17m, resultado.Liquido);

            resultado = service.Calcular(476890, 15);
            Assert.Equal(551356.63m, resultado.Bruto);
            Assert.Equal(538324.97m, resultado.Liquido);
        }
    }
}