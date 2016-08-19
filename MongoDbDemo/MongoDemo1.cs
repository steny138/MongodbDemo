using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbDemo
{
    public class MongoDemo1
    {
        private const string COLLECTION_NAME = "demo";
        public DemoViewModel create()
        {
            MongodbHelper helper = MongodbHelper.CreateConnction();
            DemoViewModel demo = createDemo();

            try
            {
                var task = helper.insertAync<DemoViewModel>(COLLECTION_NAME, demo);


                task.Wait();
            }
            catch (Exception) {

            }

            return demo;
        }

        public bool update(MongoDB.Bson.ObjectId id, string lastModifiedDateField, IEnumerable<MongodbParameter> parameters)
        {
            MongodbHelper helper = MongodbHelper.CreateConnction();
            bool result = false;
            try
            {
                var task = helper.updateByIdAsync<DemoViewModel>(COLLECTION_NAME, id, lastModifiedDateField, parameters)
                    .ContinueWith(x => { throw x.Exception; }, TaskContinuationOptions.OnlyOnFaulted);


            }
            catch (Exception) { result = false; }


            return result;
        }

        public bool update(DemoViewModel source)
        {
            MongodbHelper helper = MongodbHelper.CreateConnction();
            bool result = false;
            try
            {
                var task = helper.updateByIdAsync<DemoViewModel>(COLLECTION_NAME, source)
                    .ContinueWith(x => { throw x.Exception; }, TaskContinuationOptions.OnlyOnFaulted);
            }
            catch (Exception) { result = false; }


            return result;
        }

        private DemoViewModel createDemo()
        {
            DemoViewModel result = new DemoViewModel();

            result.name = "demo";
            result.date = DateTime.Now;
            result.key = "demo_test_1";
            result.message = "demo測試mongodb";
            result.detail = new DemoDetailViewModel();
            result.detail.idno = "A123456789";
            result.detail.content = "泰山謝先生表示:別再叫我去跑法院拉!";

            return result;
        }
    }

    public class DemoViewModel: MongodbDocument
    {
        public string name { get; set; }
        public DateTime? date { get; set; }
        public string key { get; set; }
        public string message { get; set; }

        public DemoDetailViewModel detail { get; set; }

        public List<DemoPackageViewModel> packages { get; set; }
    }

    public class DemoDetailViewModel
    {
        public string content { get; set; }
        public string idno { get; set; }
    }

    public class DemoPackageViewModel
    {
        public string name { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
    }
}
