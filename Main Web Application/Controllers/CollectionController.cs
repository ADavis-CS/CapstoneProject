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
            if (!String.IsNullOrWhiteSpace(Server.HtmlEncode(searchString)))
            {
                return View("~/Views/Inventory/ListObjects.cshtml",
                    Context.CollectionObjects
                    .Where(x => x.Name.ToLower().Contains(searchString.ToLower()) || x.Description.ToLower().Contains(searchString.ToLower()))
                    .OrderByDescending(x => x.Id)
                    .ToList());
            }

            if (sortBy != null)
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        return View("~/Views/Inventory/ListObjects.cshtml", Context.CollectionObjects.OrderBy(x => x.Name).ToList());
                    case "hasbox":
                        return View("~/Views/Inventory/ListObjects.cshtml", Context.CollectionObjects.OrderByDescending(x => x.HasBox).ToList());
                    case "hasshipper":
                        return View("~/Views/Inventory/ListObjects.cshtml", Context.CollectionObjects.OrderByDescending(x => x.HasShipper).ToList());
                    case "condition":
                        return View("~/Views/Inventory/ListObjects.cshtml", Context.CollectionObjects.OrderBy(x => x.Condition).ToList());
                    case "manufacturer":
                        return View("~/Views/Inventory/ListObjects.cshtml", Context.CollectionObjects.OrderBy(x => x.Manufacturer).ToList());
                    case "genre":
                        return View("~/Views/Inventory/ListObjects.cshtml", Context.CollectionObjects.OrderBy(x => x.Genre).ToList());
                    case "type":
                        return View("~/Views/Inventory/ListObjects.cshtml", Context.CollectionObjects.OrderBy(x => x.Type).ToList());
                    case "value":
                        return View("~/Views/Inventory/ListObjects.cshtml", Context.CollectionObjects.OrderByDescending(x => x.EstimatedValue).ToList());
                    case "sold":
                        return View("~/Views/Inventory/ListObjects.cshtml", Context.CollectionObjects.OrderByDescending(x => x.Sold).ToList());
                    case "shipped":
                        return View("~/Views/Inventory/ListObjects.cshtml", Context.CollectionObjects.OrderByDescending(x => x.DateShipped).ToList());
                    default:
                        return View("~/Views/Inventory/ListObjects.cshtml", Context.CollectionObjects.OrderByDescending(x => x.DateUpdated).ToList());
                }
            }
            else
            {
                return View("~/Views/Inventory/ListObjects.cshtml", Context.CollectionObjects.OrderByDescending(x => x.DateUpdated).ToList());
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

            CollectionObject item = new CollectionObject();
            item = Context.CollectionObjects.Find(model.Id);
            item.HasBox = model.HasBox;
            item.Genre = model.Genre;
            item.Condition = model.Condition;
            item.Description = model.Description;
            item.HasShipper = model.HasShipper;
            item.Manufacturer = model.Manufacturer;
            item.Name = model.Name;
            item.Type = model.Type;
            item.Sold = model.Sold;
            item.EstimatedValue = model.EstimatedValue;
            item.SoldValue = model.SoldValue;
            item.BuyerName = model.BuyerName;
            item.BuyerAddress = model.BuyerAddress;
            item.DateShipped = model.DateShipped;
            item.BuyerComments = model.BuyerComments;

            if (User.IsInRole("Admin"))
            {
                item.CommissionPaid = model.CommissionPaid;
                item.CommissionDate = model.CommissionDate;
                item.CommissionComments = model.CommissionComments;
            }

            item.DateUpdated = DateTime.Now;
            Context.Entry(item).State = EntityState.Modified;
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
                        file.SaveAs(_path);


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