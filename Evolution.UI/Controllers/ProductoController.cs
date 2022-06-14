using AutoMapper;
using Evolution.Infraestructure.DTO;
using Evolution.UI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Evolution.UI.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ILogger<ProductoController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly string _uri;

        public ProductoController(ILogger<ProductoController> logger,
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
            ProductoViewModel model = new();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using var respuesta = await httpClient.GetAsync($"{_uri}Producto");
                    if (respuesta.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        var response = await respuesta.Content.ReadAsStringAsync();
                        await httpClient.GetStringAsync($"{_uri}Producto");
                        var listProducto = JsonSerializer.Deserialize<List<ProductoDTO>>(response,
                            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                        model.ListProducto = listProducto;
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
            }
            return View(model);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(ProductoViewModel model)
        {
            try
            {
                var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                using (var httpClient = new HttpClient())
                {
                    var respuesta = await httpClient.PostAsJsonAsync($"{_uri}Producto", _mapper.Map<ProductoDTO>(model));

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
                    TempData["message"] = "Se registro correctamente la Producto";

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
            ProductoViewModel model = new();
            using (var httpClient = new HttpClient())
            {
                using var respuesta = await httpClient.GetAsync($"{_uri}Producto");
                if (respuesta.StatusCode.Equals(HttpStatusCode.OK))
                {
                    var response = await respuesta.Content.ReadAsStringAsync();
                    await httpClient.GetStringAsync($"{_uri}Producto");
                    var listProducto = JsonSerializer.Deserialize<List<ProductoDTO>>(response,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    model = _mapper.Map<ProductoViewModel>(listProducto.Where(x => x.Id.Equals(id)).FirstOrDefault());
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAsync(ProductoViewModel model)
        {
            try
            {
                var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                using (var httpClient = new HttpClient())
                {
                    await httpClient.PutAsJsonAsync($"{_uri}/Producto/{model.Id}", _mapper.Map<ProductoDTO>(model));
                    TempData["message"] = "Se actualizo correctamente la Producto";
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
                    await httpClient.DeleteAsync($"{_uri}/Producto/{id}");
                    TempData["message"] = "Se Elimino correctamente la Producto";
                }
            }
            catch (WebException)
            {
                TempData["message"] = "Se presento algunos inconvenientes con el registro ";
            }
            return RedirectToAction("Index");
        }
    }
}
