using Main_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main_Web_Application.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        public CollectablesDbContext Context { get; set; }

        public CollectionController()
        {
            Context = new CollectablesDbContext();
        }


        [HttpGet]
        [Route("~/Collection/List")]
        public ActionResult List(string sortBy = null, string searchString = null)
        {
            //Defining the list that will be returned
            List <CollectionObject> lstCollectionObjectsToReturn;

            //When the searcString parameter is not empty, return a matching object list by search string
            if (!String.IsNullOrWhiteSpace(Server.HtmlEncode(searchString)))
            {
                //Retrieve Collection Objects by search string
                lstCollectionObjectsToReturn = Context.CollectionObjects
                    .Where(x => x.Name.ToLower()
                    .Contains(searchString.ToLower()) || x.Description.ToLower().Contains(searchString.ToLower()))
                    .OrderByDescending(x => x.Id)
                    .ToList();

                return View("~/Views/Inventory/ListObjects.cshtml", lstCollectionObjectsToReturn);
            }

            //When the sort parameter is supplied, generate a list of objects that is sorted by the parameter value
            if (sortBy != null)
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        lstCollectionObjectsToReturn = Context.CollectionObjects.OrderBy(x => x.Name).ToList();
                        break;
                    case "hasbox":
                        lstCollectionObjectsToReturn = Context.CollectionObjects.OrderByDescending(x => x.HasBox).ToList();
                        break;
                    case "hasshipper":
                        lstCollectionObjectsToReturn = Context.CollectionObjects.OrderByDescending(x => x.HasShipper).ToList();
                        break;
                    case "condition":
                        lstCollectionObjectsToReturn = Context.CollectionObjects.OrderBy(x => x.Condition).ToList();
                        break;
                    case "manufacturer":
                        lstCollectionObjectsToReturn = Context.CollectionObjects.OrderBy(x => x.Manufacturer).ToList();
                        break;
                    case "genre":
                        lstCollectionObjectsToReturn = Context.CollectionObjects.OrderBy(x => x.Genre).ToList();
                        break;
                    case "type":
                        lstCollectionObjectsToReturn = Context.CollectionObjects.OrderBy(x => x.Type).ToList();
                        break;
                    case "value":
                        lstCollectionObjectsToReturn = Context.CollectionObjects.OrderByDescending(x => x.EstimatedValue).ToList();
                        break;
                    case "sold":
                        lstCollectionObjectsToReturn = Context.CollectionObjects.OrderByDescending(x => x.Sold).ToList();
                        break;
                    case "shipped":
                        lstCollectionObjectsToReturn = Context.CollectionObjects.OrderByDescending(x => x.DateShipped).ToList();
                        break;
                    default:
                        lstCollectionObjectsToReturn = Context.CollectionObjects.OrderByDescending(x => x.DateUpdated).ToList();
                        break;
                }

                return View("~/Views/Inventory/ListObjects.cshtml", lstCollectionObjectsToReturn);
            }
            else
            {
                //This is the default behavior - returns a list of objects sorted by the date on which the object was last updated, in descending order. 
                lstCollectionObjectsToReturn = Context.CollectionObjects.OrderByDescending(x => x.DateUpdated).ToList();

                return View("~/Views/Inventory/ListObjects.cshtml", lstCollectionObjectsToReturn);
            }
        }


        [HttpGet]
        [Route("~/Collection/View/{Id:int}")]
        public ActionResult View(int Id)
        {
            return View("~/Views/Inventory/ViewObject.cshtml", Context.CollectionObjects.Where(x => x.Id == Id).FirstOrDefault());
        }

        [HttpGet]
        [Route("~/Collection/Create")]
        public ActionResult Create()
        {
            return View("~/Views/Inventory/CreateObject.cshtml");
        }

        [HttpPost]
        [Route("~/Collection/Create")]
        public ActionResult Create(CollectionObject model)
        {

            if (ModelState.IsValid)
            {
                Context.CollectionObjects.Add(model);
                Context.SaveChanges();
                ModelState.Clear();
            }

            return View("~/Views/Inventory/CreateObject.cshtml");
        }

        [HttpGet]
        [Route("~/Collection/Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            return View("~/Views/Inventory/EditObject.cshtml", Context.CollectionObjects.Where(x => x.Id == id).FirstOrDefault());
        }

        [HttpPost]
        [Route("~/Collection/Edit/{id:int}")]
        public ActionResult Edit(CollectionObject model)
        {
            //If the model supplied is invalid, return the user back to the edit page
            if(!ModelState.IsValid)
            { 
                return View(model);
            }

            //using a new collection object, rather than the model object to avert overposting security vulnerability. 
            CollectionObject collectionObject = new CollectionObject();
            collectionObject = Context.CollectionObjects.Find(model.Id);
            collectionObject.HasBox = model.HasBox;
            collectionObject.Genre = model.Genre;
            collectionObject.Condition = model.Condition;
            collectionObject.Description = model.Description;
            collectionObject.HasShipper = model.HasShipper;
            collectionObject.Manufacturer = model.Manufacturer;
            collectionObject.Name = model.Name;
            collectionObject.Type = model.Type;
            collectionObject.Sold = model.Sold;
            collectionObject.EstimatedValue = model.EstimatedValue;
            collectionObject.SoldValue = model.SoldValue;
            collectionObject.BuyerName = model.BuyerName;
            collectionObject.BuyerAddress = model.BuyerAddress;
            collectionObject.DateShipped = model.DateShipped;
            collectionObject.BuyerComments = model.BuyerComments;

            if (User.IsInRole("Admin"))
            {
                collectionObject.CommissionPaid = model.CommissionPaid;
                collectionObject.CommissionDate = model.CommissionDate;
                collectionObject.CommissionComments = model.CommissionComments;
            }

            collectionObject.DateUpdated = DateTime.Now;
            Context.Entry(collectionObject).State = EntityState.Modified;
            Context.SaveChanges();

            ViewBag.EditSuccess = true;
            return View("~/Views/Inventory/EditObject.cshtml", Context.CollectionObjects.Where(x => x.Id == model.Id).FirstOrDefault());
        }

        [HttpPost]
        [Route("~/Collection/Edit/AddImage/{id:int}")]
        public ActionResult Edit_PostImage(int id, HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0 && (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".jpeg" || Path.GetExtension(file.FileName).ToLower() == ".png"))
                {

                    using (var dbContextTransaction = Context.Database.BeginTransaction())
                    {

                        var colObject = Context.CollectionObjects.Where(x => x.Id == id).FirstOrDefault();
                        if (colObject == null)
                            throw new Exception("There is no such collection item to store the image for!");

                        var image = new CollectionObjectImage() { CollectionItem = colObject, DateAdded = DateTime.Now, ImageName = file.FileName };

                        colObject.DateUpdated = DateTime.Now;

                        colObject.Images.Add(image);
                        Context.SaveChanges();



                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/temp_images"), $"{image.Id}_{_FileName}");

                        //a check to determine if the file already exists before saving it into the directory
                        if (!System.IO.File.Exists(_path))
                        {
                            file.SaveAs(_path);
                        }
                        else
                            throw new Exception("A file already exists in this directory, cannot create a new file with the same name!");



                        var imageSecond = colObject.Images.Where(x => x.Id == image.Id).FirstOrDefault();

                        imageSecond.FullFilePath = _path;
                        imageSecond.ImageUrl = $"{image.Id}_{_FileName}";

                        Context.SaveChanges();


                        dbContextTransaction.Commit();
                    }

                    ViewBag.FileUploadsucceeded = true;
                }
                else
                {
                    ViewBag.FileUploadsucceeded = false;

                }

                return Redirect($"~/Collection/Edit/{id}");

            }
            catch
            {
                ViewBag.FileUploadsucceeded = false;
                return Redirect($"~/Collection/Edit/{id}");
            }
        }

        [HttpPost]
        [Route("~/Collection/Delete")]
        public ActionResult Delete(int id)
        {

            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {

                var objDelete = Context.CollectionObjects.Where(x => x.Id == id).FirstOrDefault();

                if (objDelete != null)
                {

                    var imageItems = Context.CollectionObjectImages.Where(x => x.CollectionItem.Id == objDelete.Id).ToList();

                    foreach (var file in imageItems)
                    {
                        try
                        {
                            if (System.IO.File.Exists(file.FullFilePath))
                            {
                                System.IO.File.Delete(file.FullFilePath);
                            }
                        }
                        catch (IOException ioExp)
                        {
                        }
                    }

                    Context.CollectionObjectImages.RemoveRange(imageItems);
                    Context.CollectionObjects.Remove(objDelete);
                    Context.SaveChanges();
                    dbContextTransaction.Commit();
                }
            }

            return View("~/Views/Inventory/ListObjects.cshtml", Context.CollectionObjects.OrderByDescending(x => x.Id).ToList());
        }

        [HttpGet]
        [Route("~/Collection/Report")]
        public ActionResult Report()
        {

            var items = Context.CollectionObjects.ToList();

            var report = new ReportObjectModel();

            //basic calculations

            report.TotalCollectionItems = items.Count;

            report.TotalSoldCollectionItems = items.Count(x => x.Sold == true);

            report.TotalItemsShipped = items.Count(x => x.DateShipped != null);

            report.TotalSoldValueOfCollection = items.Sum(x => x.SoldValue);

            report.TotalEstimatedValueOfCollection = items.Sum(x => x.EstimatedValue);

            report.TotalItemsWithBoxes = items.Count(x => x.HasBox);

            report.TotalItemsWithShippers = items.Count(x => x.HasShipper);

            report.CollectionItemsNeedShipping = items.Where(x => x.DateShipped == null && x.Sold == true).ToList();

            //item breakdown calculations

            //manufacturer
            var manufacturerDictionary = new Dictionary<CollectionItemIdAndName, string>();
            foreach (var item in items)
            {
                manufacturerDictionary.Add(new CollectionItemIdAndName() { Id = item.Id, Name = item.Name }, item.Manufacturer.GetDisplayName().ToString());
            }
            report.ItemManufacturerBreakdown = manufacturerDictionary;

            var conditionDictionary = new Dictionary<CollectionItemIdAndName, string>();
            foreach (var item in items)
            {
                conditionDictionary.Add(new CollectionItemIdAndName() { Id = item.Id, Name = item.Name }, item.Condition.ToString());
            }
            report.ItemConditionBreakdown = conditionDictionary;

            var itemsNeedImages = new List<CollectionItemIdAndName>();
            foreach (var item in items)
            {
                if (item.Images == null || item.Images.Count == 0)
                    itemsNeedImages.Add(new CollectionItemIdAndName() { Id = item.Id, Name = item.Name });
            }
            report.ItemsWithoutImages = itemsNeedImages;

            var itemGenreDictionary = new Dictionary<CollectionItemIdAndName, string>();
            foreach (var item in items)
            {
                itemGenreDictionary.Add(new CollectionItemIdAndName() { Id = item.Id, Name = item.Name }, item.Genre.GetDisplayName().ToString());
            }
            report.ItemGenreBreakdown = itemGenreDictionary;

            var itemTypeDictionary = new Dictionary<CollectionItemIdAndName, string>();
            foreach (var item in items)
            {
                itemTypeDictionary.Add(new CollectionItemIdAndName() { Id = item.Id, Name = item.Name }, item.Type.GetDisplayName().ToString());
            }
            report.ItemTypeBreakdown = itemTypeDictionary;

            return View("~/Views/Inventory/Report.cshtml", report);
        }



    }
}