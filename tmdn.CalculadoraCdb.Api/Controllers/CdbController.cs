using System.Web.Http;
using System.Web.Http.Cors;
using tmdn.CalculadoraCdb.Application.Models.Cdb;
using tmdn.CalculadoraCdb.Application.Services;

namespace tmdn.CalculadoraCdb.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CdbController : ApiController
    {
        private readonly CdbService cdbService;

        public CdbController(CdbService cdbService)
        {
            this.cdbService = cdbService;
        }

        public ResultadoCalculoCdb Get(decimal valorInicial, int qtdMeses)
        {
            return cdbService.Calcular(valorInicial, qtdMeses);
        }
    }
}
