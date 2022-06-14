using AutoMapper;
using Evolution.Buiness.Interface;
using Evolution.Infraestructure.DTO;
using Evolution.Infraestructure.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Evolution.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        #region Members
        private readonly IPedidoBusiness _PedidoBusiness;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public PedidoController(IPedidoBusiness PedidoBusiness)
        {
            _PedidoBusiness = PedidoBusiness;
        }
        #endregion

        #region Methods
        [HttpGet]
        public IEnumerable<object> Get()
        {
            Result model = _PedidoBusiness.GetAll();
            if (object.Equals(model.ListModel, null))
                return null;
            return model.ListModel;
        }

        [HttpGet("{id}")]
        public object GetById(int id)
        {
            Result model = _PedidoBusiness.GetById(id);
            if (object.Equals(model, null))
                return null;
            return model;
        }

        [HttpPost]
        public Result Post(PedidoDTO model)
        {
            Result result = new();
            try
            {
                result = _PedidoBusiness.Insert(model);
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
            }
            return result;
        }

        [HttpPut("{id}")]
        public Result Put(PedidoDTO model)
        {
            Result result = new();
            try
            {
                result = _PedidoBusiness.Update(model);
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
            }
            return result;
        }

        // DELETE api/<PedidoController>/5
        [HttpDelete("{id}")]
        public Result Delete(int id)
        {
            Result result = new();
            try
            {
                result = _PedidoBusiness.Delete(id);
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
            }
            return result;
        }
        #endregion
    }
}
