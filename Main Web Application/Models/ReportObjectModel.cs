using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Main_Web_Application.Models
{
    public class ReportObjectModel
    {
        [Display(Name = "Total Items")]
        public int TotalCollectionItems { get; set; }

        [Display(Name = "Items Sold")]
        public int TotalSoldCollectionItems { get; set; }

        [Display(Name = "Items Shipped")]
        public int TotalItemsShipped { get; set; }
        
        [Display(Name = "Estimated Value")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double TotalEstimatedValueOfCollection { get; set; }

        [Display(Name = "Sold Value")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double TotalSoldValueOfCollection { get; set; }

        [Display(Name = "Have Boxes")]
        public double TotalItemsWithBoxes { get; set; }

        [Display(Name = "Have Shippers")]
        public double TotalItemsWithShippers { get; set; }

        [Display(Name = "Items need shipping")]
        public List<CollectionObject> CollectionItemsNeedShipping { get; set; }

        //breaking down items by type

        public Dictionary<CollectionItemIdAndName, string> ItemManufacturerBreakdown { get; set; }

        public Dictionary<CollectionItemIdAndName, string> ItemConditionBreakdown{ get; set; }

        public Dictionary<CollectionItemIdAndName, string> ItemGenreBreakdown { get; set; }

        public Dictionary<CollectionItemIdAndName, string> ItemTypeBreakdown { get; set; }

        public List<CollectionItemIdAndName> ItemsWithoutImages { get; set; }
    }

    public class CollectionItemIdAndName
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

}