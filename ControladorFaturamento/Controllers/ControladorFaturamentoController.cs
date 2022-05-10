using ControladorFaturamento.Dominio;
using ControladorFaturamento.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControladorFaturamento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorFaturamentoController : Controller
    {
        public IActionResult Processar(FiltroFaturamento filtro)
        {
            try
            {
                new ControladorFaturamentoService().ProcessarFila(filtro);

                return Ok(filtro);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
