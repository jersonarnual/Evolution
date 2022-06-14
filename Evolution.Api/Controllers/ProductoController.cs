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
    public class ProductoController : ControllerBase
    {
        #region Members
        private readonly IProductoBusiness _ProductoBusiness;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public ProductoController(IProductoBusiness ProductoBusiness)
        {
            _ProductoBusiness = ProductoBusiness;
        }
        #endregion

        #region Methods
        [HttpGet]
        public IEnumerable<object> Get()
        {
            Result model = _ProductoBusiness.GetAll();
            if (object.Equals(model.ListModel, null))
                return null;
            return model.ListModel;
        }

        [HttpGet("{id}")]
        public object GetById(int id)
        {
            Result model = _ProductoBusiness.GetById(id);
            if (object.Equals(model, null))
                return null;
            return model;
        }

        [HttpPost]
        public Result Post(ProductoDTO model)
        {
            Result result = new();
            try
            {
                result = _ProductoBusiness.Insert(model);
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
            }
            return result;
        }

        [HttpPut("{id}")]
        public Result Put(ProductoDTO model)
        {
            Result result = new();
            try
            {
                result = _ProductoBusiness.Update(model);
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
            }
            return result;
        }

        // DELETE api/<ProductoController>/5
        [HttpDelete("{id}")]
        public Result Delete(int id)
        {
            Result result = new();
            try
            {
                result = _ProductoBusiness.Delete(id);
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
