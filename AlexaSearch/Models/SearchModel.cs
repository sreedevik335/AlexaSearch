using AlexaSearch.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

using System.Web;

namespace AlexaSearch.Models
{
    public class SearchModel : SearchResultItem
    {
        [IndexField("_name")]
        public virtual string ItemName { get; set; }

        [IndexField("author_t")]
        public virtual string Author { get; set; }

        [IndexField("description_t")]
        public virtual string Description { get; set; } // Custom field on my template

        [IndexField("title_t")]
        public virtual string Title { get; set; } // Custom field on my template
        [IndexField("Id_t")]
        public virtual string ID { get; set; }
        
    }

    public class SearchResult
    {
        public string ItemName { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string ItemId { get; set; }
    }

    /// <summary>
    /// Custom search result model for binding to front end
    /// </summary>
    public class SearchResults
    {
        public List<SearchResult> Results { get; set; }
    }
}
