using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Main_Web_Application.Models
{
    public class CollectablesDbContext : DbContext
    {
        public CollectablesDbContext()
            : base("DefaultConnection")
        {
        }

        public static CollectablesDbContext Create()
        {
            return new CollectablesDbContext();
        }

        public System.Data.Entity.DbSet<Main_Web_Application.Models.CollectionObject> CollectionObjects { get; set; }

        public System.Data.Entity.DbSet<Main_Web_Application.Models.CollectionObjectImage> CollectionObjectImages { get; set; }

    }

    public class CollectionObject
    {
        public CollectionObject()
        {
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime? DateUpdated { get; set; }

        public string Name { get; set; }

        public bool Sold { get; set; }

        [Display(Name = "Estimated Value")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double EstimatedValue { get; set; }

        [Display(Name = "Sold Value")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double SoldValue { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Has Box")]
        public bool HasBox { get; set; }

        [Display(Name = "Has Shipper")]
        public bool HasShipper { get; set; }

  

        public CollectionObjectCondition Condition { get; set; }

        public CollectionObjectManufacturer Manufacturer { get; set; }

        public CollectionObjectGenre Genre { get; set; }

        public CollectionObjectType Type { get; set; }

        public virtual ICollection<CollectionObjectImage> Images { get; set; }



        #region Buyer Information

        [Display(Name = "Buyer's Name")]
        [StringLength(30)]
        public string BuyerName { get; set; }

        [Display(Name = "Buyer's Address")]
        [DataType(DataType.MultilineText)]
        [StringLength(250)]
        public string BuyerAddress { get; set; }

        [Display(Name = "Date Shipped")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateShipped { get; set; }

        [Display(Name = "Buyer Comments")]
        [DataType(DataType.MultilineText)]
        [StringLength(3000)]
        public string BuyerComments { get; set; }

        #endregion


        #region Commission Fields 

        [Display(Name = "Commission Paid")]
        public bool CommissionPaid { get; set; }

        [Display(Name = "Cmsn. Paid Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CommissionDate { get; set; }

        [Display(Name = "Cmsn. Comments")]
        [DataType(DataType.MultilineText)]
        public string CommissionComments { get; set; }

        #endregion


    }

    public enum CollectionObjectCondition
    {
        Excellent,
        Good,
        Fair,
        Poor
    }

    public enum CollectionObjectManufacturer
    {
        [Display(Name = "Code 3")]
        Code_3,
        [Display(Name = "DC Collectibles")]
        DC_Collectibles,
        [Display(Name = "DC Direct")]
        DC_Direct,
        [Display(Name = "Diamond Select")]
        Diamond_Select,
        [Display(Name = "Efx Collectibles")]
        Efx_Collectibles,
        [Display(Name = "Factory X")]
        Factory_X,
        Galoob,
        [Display(Name = "Gentle Giant")]
        Gentle_Giant,
        Hasbro,
        [Display(Name = "Hot Toys")]
        Hot_Toys,
        [Display(Name = "Iron Studios")]
        Iron_Studios,
        [Display(Name = "Jekks Pacific")]
        Jakks_Pacific,
        Kotobukiya,
        LEGO,
        McFarlane,
        [Display(Name = "Marvel Modern Era")]
        Marvel_Modern_Era,
        [Display(Name = "Master Replicas")]
        Master_Replicas,
        Medicom,
        Neca,
        Other,
        Revell,
        [Display(Name = "Sideshow Collectibles")]
        Sideshow_Collectibles,
        [Display(Name = "Warner Bros.")]
        Warner_Bros,
        Weta,
        [Display(Name = "World Limited")]
        World_Limited
    }

    public enum CollectionObjectGenre
    {
        [Display(Name = "Star Wars")]
        Star_Wars,
        Marvel,
        DC,
        Other
    }

    public enum CollectionObjectType
    {
        [Display(Name ="Action Figure")]
        Action_Figure,
        [Display(Name = "Other")]
        Other,
        [Display(Name = "1:6 Scale Figure")]
        One_Sixth_Scale_Figure,
        [Display(Name = "1:6 Scale Statue")]
        One_Sixth_Scale_Statue,
        [Display(Name = "Mini-Bust")]
        Mini_Bust,
        [Display(Name = "Premium Format Statue")]
        Premium_Format_Statue,
        [Display(Name = "1:4 Scale Figure")]
        One_Fourth_Scale_Figure,
        [Display(Name = "3.75\" Figure")]
        Three_SeventyFive_Inch_Figure,
        [Display(Name = "1:6 Scale Set")]
        One_Sixth_Scale_Set,
        [Display(Name = "1:1 Scale Lightsaber")]
        One_One_Scale_lightsaber,
        [Display(Name = "Studio Scale Vehicle")]
        Studio_Scale_Vehicle,
        [Display(Name = "3.75\" Vehicle")]
        Three_SeventyFive_Inch_Vehicle,
        [Display(Name = "Doll")]
        Doll,
        [Display(Name = "Playset")]
        Playset,
        [Display(Name = "Statue")]
        Statue,
        [Display(Name = "6\" Figure")]
        Six_Inch_Figure,
        [Display(Name = "10\" Figure")]
        Ten_Inch_Figure,
        [Display(Name = "13\" Figure")]
        Thirteen_Inch_Figure,
        [Display(Name = "18\" Figure")]
        Eighteen_Inch_Figure,
        [Display(Name = "Big Fig")]
        Big_Fig,
        [Display(Name = "Accessory")]
        Accessory

    }



    public class CollectionObjectImage
    {
        
        public CollectionObjectImage()
        {
            DateAdded = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        public string ImageName { get; set; }

        public string ImageUrl { get; set; }

        public string FullFilePath { get; set; }

        public DateTime DateAdded { get; set; }

        public CollectionObject CollectionItem { get; set; }

    }

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            if(enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault().GetCustomAttribute<DisplayAttribute>() == null)
                return enumValue.ToString();
            else
                return enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName();             
        }
    }
}