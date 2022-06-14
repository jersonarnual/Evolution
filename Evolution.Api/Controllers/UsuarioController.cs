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
    public class UsuarioController : ControllerBase
    {

        #region Members
        private readonly IUsuarioBusiness _UsuarioBusiness;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public UsuarioController(IUsuarioBusiness UsuarioBusiness)
        {
            _UsuarioBusiness = UsuarioBusiness;
        }
        #endregion

        #region Methods
        [HttpGet]
        public IEnumerable<object> Get()
        {
            Result model = _UsuarioBusiness.GetAll();
            if (object.Equals(model.ListModel, null))
                return null;
            return model.ListModel;
        }

        [HttpGet("{id}")]
        public object GetById(int id)
        {
            Result model = _UsuarioBusiness.GetById(id);
            if (object.Equals(model, null))
                return null;
            return model;
        }

        [HttpPost]
        public Result Post(UsuarioDTO model)
        {
            Result result = new();
            try
            {
                result = _UsuarioBusiness.Insert(model);
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
            }
            return result;
        }

        [HttpPut("{id}")]
        public Result Put(UsuarioDTO model)
        {
            Result result = new();
            try
            {
                result = _UsuarioBusiness.Update(model);
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
            }
            return result;
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public Result Delete(int id)
        {
            Result result = new();
            try
            {
                result = _UsuarioBusiness.Delete(id);
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
