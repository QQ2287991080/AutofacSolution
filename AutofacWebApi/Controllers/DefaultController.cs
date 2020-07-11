using Domian;
using Interfaces;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace AutofacWebApi.Controllers
{
    [RoutePrefix("api/default")]
    public class DefaultController : ApiController
    {
        private IServiceA _serviceA;
        readonly IBaseClient _client;
        readonly IEntypeRepository _entype;
        readonly IAreaRepository _area;
        public DefaultController(
            IServiceA serviceA,
            IBaseClient baseClient,
            IEntypeRepository entypeRepository,
            IAreaRepository areaRepository
            )
        {
            _serviceA = serviceA;
            _client = baseClient;
            _entype = entypeRepository;
            _area = areaRepository;
        }
        [HttpGet]
        [Route("get")]
        public IHttpActionResult Get()
        {

            return this.Json("");
        }
        [HttpGet]
        [Route("get2")]

        public async Task<IHttpActionResult> Get2()
        {
            //测试分布式事务
            List<string> message = new List<string>();
            //开始事务
            _client.BeginTran();
            try
            {
                message.Add("开始事务");
                var query = await _entype.QueryAsync();
                message.Add($"表中总数据{query.Count}");

                message.Add($"add 一条数据");
                await  _entype.AddAsync(new BA_SysEnType
                {
                    Name = "测试",
                    IsEnd = true,
                    IsSys = true,
                    Level = 1
                });
                var list = await _area.QueryAsync();
                var list2 = await _entype.QueryAsync();
                message.Add($"add之后表中总数据{list2.Count}");
                message.Add("抛出一个错误");
                ////抛出错误
                //int a = 0;
                //var b = 1 / a;
                message.Add("提交事务");
                _client.CommitTran();
                var list3 = await _entype.QueryAsync();
                message.Add($"事务提交之后表中总数据{list3.Count}");
                return Json(message);
            }
            catch (Exception ex)
            {
                _client.RollbackTran();
                message.Add("回滚事务");
                var query = await _entype.QueryAsync();
                message.Add($"回滚事务之后表中总数据{query.Count}");
                return BadRequest(string.Join(",", message));
            }
        }
        [HttpGet]
        [Route("get3")]
        public async Task<IHttpActionResult> Get3()
        {
            HttpClient client = new HttpClient();
            string key = "79ecf6befa658ddca9fa0f6704e42037";
            string origins = "121.436772,31.29614";
            string destination = "121.43912404775621,31.340702564918278";
            var result = await client.GetAsync($"https://restapi.amap.com/v3/distance?key={key}&type=0&origins={origins}&destination={destination}");
            var message= await result.Content.ReadAsStringAsync();
            var map= JsonConvert.DeserializeObject<Map>(message);
            return this.Json(map);
        }
    }

    public class ResultsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string origin_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dest_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string distance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string duration { get; set; }
    }

    public class Map
    {
        /// <summary>
        /// 
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string infocode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ResultsItem> results { get; set; }
    }
}
