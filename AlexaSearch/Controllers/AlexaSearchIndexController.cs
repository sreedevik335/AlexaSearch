using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AlexaSearch.Models;
using Sitecore;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace AlexaSearch.Controllers
{
    public class AlexaSearchIndexController : Controller
    {
        // GET: AlexaSearchIndex
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult blogbody()
        {
            List<Sitecore.Data.Items.Item> blogItems = new List<Sitecore.Data.Items.Item>();
            List<BlogModel> BlogCardsCollection = new List<BlogModel>();

            var itemm = Sitecore.Context.Database.GetItem("{931AFD1B-D8D1-46D1-896E-1644BEB73ED2}");
            Sitecore.Data.Fields.MultilistField multilistField = itemm.Fields["PreferredArticles"];
            int len = multilistField.GetItems().Length;
            List<Sitecore.Data.Items.Item> items = new List<Item>();



            for (int i = 0; i < multilistField.GetItems().Length; i++)
            {

                blogItems.Add(multilistField.GetItems()[i]);
            }
            for (int i = 0; i < blogItems.Count; i++)
            {
                BlogModel BlogModel = new BlogModel();
                var imageUrl = string.Empty;

                Sitecore.Data.Fields.ImageField imageField = blogItems[i].Fields["BlogSmallImage"];
                if (imageField?.MediaItem != null)
                {
                    var image = new MediaItem(imageField.MediaItem);
                    imageUrl = StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(image));
                    BlogModel.BlogSImage = imageUrl;
                }
                BlogModel.Category = blogItems[i].Fields["Category"].Value;
                BlogModel.BlogTitle = blogItems[i].Fields["Title"].Value;

                Sitecore.Data.Fields.DateField dateTimeField = blogItems[i].Fields["PostedDate"];

                string dateTimeString = dateTimeField.Value;

                DateTime dateTimeStruct = Sitecore.DateUtil.IsoDateToDateTime(dateTimeString);
                BlogModel.date = dateTimeStruct.ToShortDateString();

                BlogModel.ShortDesc = blogItems[i].Fields["ShortDescription"].Value;

                BlogModel.BlogURL = Sitecore.Links.LinkManager.GetItemUrl(blogItems[i]);
                BlogModel.sitecoreItem = blogItems[i];

                BlogCardsCollection.Add(BlogModel);
            }



            ViewBag.multi = BlogCardsCollection;
            return View("~/Views/Sitecoreblog/blogbody.cshtml");
        }



        //public ActionResult DemoComponent()
        //{
        //    var item = Sitecore.Context.Database.GetItem("/sitecore/content/Alexa/Home/Settings/GlobalSettings");

        //    return View("~/Views/Alexa/ControllerDemo.cshtml", item);
        //}
        [HttpPost]
        public ActionResult blogbody(FormCollection form)
        {
            string searchText = form["searchInput"];
            var myResults = new SearchResults
            {
                Results = new List<SearchResult>()
            };
            //var searchIndex = ContentSearchManager.GetIndex("sitecore_web_index"); // Get the search index
            //var searchPredicate = GetSearchPredicate(searchText); // Build the search predicate
            //using (var searchContext = searchIndex.CreateSearchContext()) // Get a context of the search index
            //{
            //    //Select * from Sitecore_web_index Where Author="searchText" OR Description="searchText" OR Title="searchText"
            //    //var searchResults = searchContext.GetQueryable<SearchModel>().Where(searchPredicate); // Search the index for items which match the predicate
            //    var searchResults = searchContext.GetQueryable<SearchModel>()
            //        .Where(x => x.Author.Contains(searchText) || x.Title.Contains(searchText) || x.Description.Contains(searchText));   //LINQ query

            //    var fullResults = searchResults.GetResults();

            //    // This is better and will get paged results - page 1 with 10 results per page
            //    //var pagedResults = searchResults.Page(1, 10).GetResults();
            //    foreach (var hit in fullResults.Hits)
            //    {
            //        myResults.Results.Add(new SearchResult
            //        {
            //            Description = hit.Document.Description,
            //            Title = hit.Document.Title,
            //            ItemName = hit.Document.ItemName,
            //            Author = hit.Document.Author
            //        });
            //    }
            //    return new JsonResult { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = myResults };



            //Sitecore.Data.Database master = Sitecore.Configuration.Factory.GetDatabase("web");



            List<string> solr = new List<string>();
            List<string> itemsss = new List<string>();
            List<Sitecore.Data.Items.Item> blogItems = new List<Sitecore.Data.Items.Item>();

            var itemm = Sitecore.Context.Database.GetItem("{F5BF3822-C8CC-47DA-BFA4-9C5CE6912BD3}");
            Sitecore.Data.Fields.MultilistField multilistField = itemm.Fields["PreferredArticles"];
            if (multilistField != null)
            {
                foreach (Sitecore.Data.Items.Item city in multilistField.GetItems())
                {
                    blogItems.Add(city);
                }
            }

            //var searchIndex = ContentSearchManager.GetIndex("sitecore_web_index"); // Get the search index
            //var searchPredicate = GetSearchPredicate(searchText); // Build the search predicate
            //using (var searchContext = searchIndex.CreateSearchContext()) // Get a context of the search index
            //{
            //    //Select * from Sitecore_web_index Where Author="searchText" OR Description="searchText" OR Title="searchText"
            //    //var searchResults = searchContext.GetQueryable<SearchModel>().Where(searchPredicate); // Search the index for items which match the predicate
            //    var searchResults = searchContext.GetQueryable<SearchModel>()
            //        .Where(x => x.Title.Contains(searchText) );   //LINQ query

            //    var fullResults = searchResults.GetResults();

            //    // This is better and will get paged results - page 1 with 10 results per page
            //    //var pagedResults = searchResults.Page(1, 10).GetResults();
            //    foreach (var hit in fullResults.Hits)
            //    {
            //        myResults.Results.Add(new SearchResult
            //        {
            //            Description = hit.Document.Description,
            //            Title = hit.Document.Title,
            //            ItemName = hit.Document.ItemName,
            //            Author = hit.Document.Author
            //        });
            //        solr.Add(System.Convert.ToString(hit.Document.ItemId));

            //    }
            //    
            //    List<string> res = new List<string>();

            //    foreach (string i in itemsss)
            //    {
            //        if (solr.Contains(i))
            //        {

            //            res.Add(i);

            //        }
            //    }
            //    int len = res.Count;
            //    List<Sitecore.Data.Items.Item> items = new  List<Item>();

            //    for (int j = 0; j < res.Count; j++)
            //    {
            //        Sitecore.Data.Items.Item item1 = Sitecore.Context.Database.GetItem(res[j]);
            //        items.Add(item1);
            //    }


            List<BlogModel> BlogCardsCollection = new List<BlogModel>();

            for (int i = 0; i < multilistField.GetItems().Length; i++)
            {
                BlogModel BlogModel = new BlogModel();
                var imageUrl = string.Empty;


                BlogModel.Category = blogItems[i].Fields["Category"].Value;
                BlogModel.BlogTitle = blogItems[i].Fields["Title"].Value;

                Sitecore.Data.Fields.DateField dateTimeField = blogItems[i].Fields["PostedDate"];

                string dateTimeString = dateTimeField.Value;

                DateTime dateTimeStruct = Sitecore.DateUtil.IsoDateToDateTime(dateTimeString);
                BlogModel.date = dateTimeStruct.ToShortDateString();

                BlogModel.ShortDesc = blogItems[i].Fields["LongDescription"].Value;

                BlogModel.BlogURL = Sitecore.Links.LinkManager.GetItemUrl(blogItems[i]);
                BlogModel.sitecoreItem = blogItems[i];

                BlogCardsCollection.Add(BlogModel);
            }



            List<BlogModel> results = BlogCardsCollection.FindAll(Findtitle);



            bool Findtitle(BlogModel bk)
            {

                if (bk.BlogTitle.Contains(searchText) || bk.Category.Contains(searchText) || bk.ShortDesc.Contains(searchText))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            ViewBag.multi = results;
            return View("~/Views/Sitecoreblog/blogbody.cshtml");
        }

    }
}