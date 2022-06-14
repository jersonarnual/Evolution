using AutoMapper;
using Evolution.Buiness.Interface;
using Evolution.Data.Interface;
using Evolution.Data.models;
using Evolution.Infraestructure.DTO;
using Evolution.Infraestructure.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Evolution.Buiness.Repository
{
    public class PedidoBusiness : IPedidoBusiness
    {
        #region Member
        private readonly IDefaultRepository<Pedido> _repository;
        private readonly IDefaultRepository<Usuario> _repositoryUsuario;
        private readonly IDefaultRepository<Producto> _repositoryProducto;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public PedidoBusiness(IDefaultRepository<Pedido> repository,
                              IDefaultRepository<Usuario> repositoryUsuario,
                              IDefaultRepository<Producto> repositoryProducto,
                              IMapper mapper)
        {
            _repository = repository;
            _repositoryUsuario = repositoryUsuario;
            _repositoryProducto = repositoryProducto;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public Result Delete(int id)
        {
            Result result = new();
            try
            {
                Pedido Pedido = _repository.GetById(id);
                if (object.Equals(Pedido, null))
                {
                    result.MessageException = $"ERROR: El modelo se encuentra vacio";
                    result.State = false;
                    return result;
                }

                if (_repository.Delete(Pedido))
                {
                    result.State = true;
                    result.Message = "Operacion Exitosa";
                    return result;
                }
                result.State = false;
                result.Message = "No se logro completar la operacion";
                return result;

            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
                result.State = false;
                return result;
            }
        }

        public Result GetAll()
        {
            Result result = new();
            List<PedidoDTO> PedidoDTO = new();
            try
            {
                var model = _repository.GetAll();
                if (!model.Any() || object.Equals(model, null))
                {
                    result.MessageException = $"ERROR: El objeto se encuentra vacio";
                    result.State = false;
                    return result;
                }
                foreach (var item in model) {
                    item.Usuario =_repositoryUsuario.GetById(item.PedUsu);
                    item.Producto = _repositoryProducto.GetById(item.PedProd);
                    PedidoDTO.Add(_mapper.Map<PedidoDTO>(item));
                }

                result.ListModel = PedidoDTO;
                result.Message = "Operacion Exitosa";
                result.State = true;
                return result;
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
                result.State = false;
                return result;
            }
        }

        public Result GetById(int id)
        {
            Result result = new();
            try
            {
                var model = _repository.GetById(id);
                if (object.Equals(model, null))
                {
                    result.MessageException = $"ERROR: No se encontraron registros";
                    result.State = false;
                }
                result.Model = _mapper.Map<PedidoDTO>(model);
                result.Message = "Operacion Exitosa";
                result.State = true;
                return result;
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
                result.State = false;
                return result;
            }
        }

        public Result Insert(PedidoDTO entity)
        {
            Result result = new();
            try
            {
                if (object.Equals(entity, null))
                {
                    result.MessageException = $"ERROR: El modelo se encuentra vacio";
                    result.State = false;
                    return result;
                }

                var model = _mapper.Map<Pedido>(entity);
                if (_repository.Insert(model))
                {
                    result.State = true;
                    result.Message = "Operacion Exitosa";
                    return result;
                }
                result.State = false;
                result.Message = "No se logro completar la operacion";
                return result;
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
                result.State = false;
                return result;
            }
        }

        public Result Update(PedidoDTO entity)
        {
            Result result = new();
            try
            {
                if (object.Equals(entity, null))
                {
                    result.MessageException = $"ERROR: El modelo se encuentra vacio";
                    result.State = false;
                    return result;
                }

                var model = _mapper.Map<Pedido>(entity);
                if (_repository.Update(model))
                {
                    result.State = true;
                    result.Message = "Operacion Exitosa";
                    return result;
                }
                result.State = false;
                result.Message = "No se logro completar la operacion";
                return result;
            }
            catch (Exception ex)
            {
                result.MessageException = $"ERROR: {ex.Message} {ex.StackTrace}";
                result.State = false;
                return result;
            }
        }
        #endregion
    }
}
