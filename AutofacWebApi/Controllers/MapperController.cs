using AutofacWebApi.Models;
using AutoMapper;
using Interfaces;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AutofacWebApi.Controllers
{
    public class MapperController : ApiController
    {
        readonly IMapper _mapper;
        readonly IEntypeRepository _entype;
        public MapperController(
            IMapper mapper,
            IEntypeRepository enType
            )
        {
            _mapper = mapper;
            _entype = enType;
        }
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var list =await _entype.QueryAsync();
            var model =  _mapper.Map<List<EntypeModel>>(list);
            return this.Json(model);
        }
    }
}
