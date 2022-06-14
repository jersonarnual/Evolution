using AutoMapper;
using Evolution.Infraestructure.DTO;
using Evolution.UI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Evolution.UI.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly string _uri;

        public PedidoController(ILogger<PedidoController> logger,
                              IConfiguration configuration,
                              IMapper mapper)
        {
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
            _uri = _configuration.GetValue<string>("UrlApi");
        }

        public async Task<IActionResult> IndexAsync()
        {
            PedidoViewModel model = new();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using var respuesta = await httpClient.GetAsync($"{_uri}Pedido");
                    if (respuesta.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        var response = await respuesta.Content.ReadAsStringAsync();
                        await httpClient.GetStringAsync($"{_uri}Pedido");
                        var listPedido = JsonSerializer.Deserialize<List<PedidoDTO>>(response,
                            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                        model.ListPedido = listPedido;
                        BuidModel();
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
            }
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            List<ProductoDTO> listProducto = new();
            List<UsuarioDTO> listUsuario = new();
            listProducto = await GetProductDTO();
            listUsuario = await GetUsuarioDTO();
            ViewBag.ListUsuario = new SelectList(listUsuario, "Id", "UsuNombre");
            ViewBag.ListProducto = new SelectList(listProducto, "Id", "ProDesc");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(PedidoViewModel model)
        {
            try
            {
                var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                using (var httpClient = new HttpClient())
                {
                    var respuesta = await httpClient.PostAsJsonAsync($"{_uri}Pedido", _mapper.Map<PedidoDTO>(model));

                    if (respuesta.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var cuerpo = await respuesta.Content.ReadAsStringAsync();
                        var erroresDelAPI = Util.Util.ExtraerErroresDelWebAPI(cuerpo);
                        List<string> ListError = new();
                        foreach (var campoErrores in erroresDelAPI)
                        {
                            string bodyError = string.Empty;
                            bodyError += $"-{campoErrores.Key}";
                            foreach (var error in campoErrores.Value)
                                bodyError += $"-{error}";
                            ListError.Add(bodyError);
                        }
                        TempData["message"] = ListError.ToString();
                    }
                    TempData["message"] = "Se registro correctamente la Pedido";

                    return RedirectToAction("Index");
                }
            }
            catch (WebException)
            {
                TempData["message"] = "Se presento algunos inconvenientes con el registro ";
            }
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {
            PedidoViewModel model = new();
            using (var httpClient = new HttpClient())
            {
                using var respuesta = await httpClient.GetAsync($"{_uri}Pedido");
                if (respuesta.StatusCode.Equals(HttpStatusCode.OK))
                {
                    var response = await respuesta.Content.ReadAsStringAsync();
                    await httpClient.GetStringAsync($"{_uri}Pedido");
                    var listPedido = JsonSerializer.Deserialize<List<PedidoDTO>>(response,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    model = _mapper.Map<PedidoViewModel>(listPedido.Where(x => x.Id.Equals(id)).FirstOrDefault());
                    List<ProductoDTO> listProducto = new();
                    List<UsuarioDTO> listUsuario = new();
                    listProducto = await GetProductDTO();
                    listUsuario = await GetUsuarioDTO();
                    ViewBag.ListUsuario = new SelectList(listUsuario, "Id", "UsuNombre");
                    ViewBag.ListProducto = new SelectList(listProducto, "Id", "ProDesc");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(PedidoViewModel model)
        {
            try
            {
                var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                using (var httpClient = new HttpClient())
                {
                    await httpClient.PutAsJsonAsync($"{_uri}/Pedido/{model.Id}", _mapper.Map<PedidoDTO>(model));
                    TempData["message"] = "Se actualizo correctamente la Pedido";
                    return RedirectToAction("Index");
                }
            }
            catch (WebException)
            {
                TempData["message"] = "Se presento algunos inconvenientes con el registro ";
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    await httpClient.DeleteAsync($"{_uri}/Pedido/{id}");
                    TempData["message"] = "Se Elimino correctamente la Pedido";
                }
            }
            catch (WebException)
            {
                TempData["message"] = "Se presento algunos inconvenientes con el registro ";
            }
            return RedirectToAction("Index");
        }

        private async Task<List<ProductoDTO>> GetProductDTO()
        {
            List<ProductoDTO> model = new();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using var respuesta = await httpClient.GetAsync($"{_uri}Producto");
                    if (respuesta.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        var response = await respuesta.Content.ReadAsStringAsync();
                        await httpClient.GetStringAsync($"{_uri}Producto");
                        var list = JsonSerializer.Deserialize<List<ProductoDTO>>(response,
                            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                        model = list;
                    }
                }
                return model;
            }
            catch (WebException ex)
            {
                return model;
            }
        }
        private async Task<List<UsuarioDTO>> GetUsuarioDTO()
        {
            List<UsuarioDTO> model = new();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using var respuesta = await httpClient.GetAsync($"{_uri}Usuario");
                    if (respuesta.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        var response = await respuesta.Content.ReadAsStringAsync();
                        await httpClient.GetStringAsync($"{_uri}Usuario");
                        var list = JsonSerializer.Deserialize<List<UsuarioDTO>>(response,
                            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                        model = list;
                    }
                }
                return model;
            }
            catch (WebException ex)
            {
                return model;
            }
        }
        private async void BuidModel()
        {
            List<ProductoDTO> listProducto = new();
            List<UsuarioDTO> listUsuario = new();
            listProducto = await GetProductDTO();
            listUsuario = await GetUsuarioDTO();
            ViewBag.ListUsuario = new SelectList(listUsuario, "Id", "UsuNombre");
            ViewBag.ListProducto = new SelectList(listProducto, "Id", "ProDesc");
        }
    }
}
