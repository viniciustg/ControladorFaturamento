using ControladorFaturamento.Domain;
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

                for (int i = 0; i < 1000; i++)
                {
                    var ticket = new Ticket()
                    {
                        Id = i,
                        Valor = i,
                        Valor2 = i,
                        Valor3 = i,
                        Valor4 = i,
                        Valor5 = i,
                        Valor6 = i,
                        Valor12 = i,
                        Valor13 = i,
                        Valor14 = i,
                        Valor15 = i,
                        Valor16 = i,
                        Valor17 = i,
                        Valor18 = i,
                        Valor19 = i,
                        Valor20 = i,
                        Valor21 = i,
                        Valor22 = i                       
                    };

                    filtro.Tickets.Add(ticket);
                }

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
