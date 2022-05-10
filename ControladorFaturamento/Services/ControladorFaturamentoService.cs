using ControladorFaturamento.Dominio;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ControladorFaturamento.Services
{
    public class ControladorFaturamentoService
    {
        public void ProcessarFila(FiltroFaturamento filtro)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "filaFaturador",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                filtro.Id = new Random().Next(777777, 999999);
                filtro.CodigoCliente = new Random().Next(4000, 9999);

                string mensagem = JsonSerializer.Serialize(filtro);
                var corpoMensagem = Encoding.UTF8.GetBytes(mensagem);

                channel.BasicPublish(exchange: "",
                                     routingKey: "filaFaturador",
                                     basicProperties: null,
                                     body: corpoMensagem);                
            }            
        }
    }
}
