using ZhaiFanhuaDemo.BaiDu.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZhaiFanhuaDemo.BaiDu.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 页面搜索
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSearch(string key)
        {
            JsonResult jsonResult = new JsonResult();
            List<SearchModel> searchModelList = new List<SearchModel>();
            searchModelList = GetSearchText(key);
            if (searchModelList.Count > 0)
                jsonResult.Data = searchModelList;
            else
                jsonResult.Data = "404";
            return jsonResult;
        }
        /// <summary>
        /// 获取搜索结果
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private List<SearchModel> GetSearchText(string key)
        {
            string sql = @"select  TOP 10  Id,Text,Frequency from Data where Text like @key order by Frequency desc";
            List<SearchModel> searchModelList = new List<SearchModel>();
            SqlParameter[] paras = {
                new SqlParameter("@key",$"{key}%")
            };
            using (SqlDataReader sqlDataReader = DBHelper.ExecuteGetReader(sql, paras))
            {
                while (sqlDataReader.Read())
                {
                    SearchModel searchModel = new SearchModel();
                    searchModel.Id = (int)sqlDataReader["Id"];
                    searchModel.Text = sqlDataReader["Text"].ToString();
                    searchModel.Frequency = (int)sqlDataReader["Frequency"];
                    searchModelList.Add(searchModel);
                }
                return searchModelList;
            }
        }
        /// <summary>
        /// 点击量增加
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public JsonResult AddSearch(int id)
        {
            JsonResult jsonResult = new JsonResult();
            string result = AddSearchFrequency(id);
            jsonResult.Data = result;
            return jsonResult;
        }
        /// <summary>
        /// 赋值点击量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string AddSearchFrequency(int id)
        {
            string sql = @"update Data set Frequency=Frequency+1 where Id=@id";
            SqlParameter[] paras = {
                new SqlParameter("@id",id)
            };
            int result = DBHelper.ExecuteGetNonQuery(sql, paras);
            if (result == 1)
                return "success";
            return "fail";
        }
    }
}