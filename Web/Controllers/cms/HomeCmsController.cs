using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using EntityFrameworkPaginateCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.StaticHelpers;
using Web.ViewModels.Cms;

namespace Web.Controllers.cms
{
    public class HomeCmsController : CommonController
    {
        private readonly IHostingEnvironment _appEnvironment;
        
        public HomeCmsController(Context context, IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            Context = context;
        }

        [Authorize]
        public async Task<IActionResult> GetList(string type, int page = 0, string sorted = null, string filter = null)
        {
            var t = Type.GetType("Armada.Models." + type);
            if (CheckIsSingle(t))
                return RedirectToAction("EditFirst", new {type = type});

            ViewBag.Type = t;
            ViewBag.NameType = type;
            ViewBag.Page = page;
            dynamic objdect = Activator.CreateInstance(t);
            var set = Context.GetDbSet(objdect);

            var includeSets = Provider(ReflectionHelper.InsertInclude<ShowTitleAttribute>(set, t), objdect);

            var localFilter = filter;
            if (string.IsNullOrEmpty(localFilter))
            {
                localFilter = CookieHelper.GetFilter(HttpContext);
            }
            else
            {
                CookieHelper.SetFilter(localFilter, HttpContext);
            }

            var pageResult = (await Paginate(objdect, includeSets, t, page + 1, 20, null, localFilter));
            var result = ProviderPage(objdect, pageResult);
            var sortedSet = result.Results;
            ViewBag.PageCount = result.PageCount;

            var cmsModel = Context.CmsModels.FirstOrDefault(w => w.Class == type);
            if (cmsModel != null)
            {
                cmsModel.NewCount = 0;
                Context.SaveChanges();
            }

            return View(sortedSet);
        }

        private bool CheckIsSingle(Type type)
        {
            return type.GetCustomAttribute<SingleObjectAttribute>() != null;
        }

        private IQueryable<T> Provider<T>(IQueryable<IEntity> list, T objectType) where T : ISortFilterEntity<T>
        {
            return list.Cast<T>();
        }

        private Page<T> ProviderPage<T>(T objectType, Page<T> page) where T : ISortFilterEntity<T>
        {
            return page;
        }

        public Task<Page<T>> Paginate<T>(T objectType, IQueryable<T> list, Type type, int pageNumber, int pageSize, string sorteds = null, string filter = null)
            where T : ISortFilterEntity<T>
        {
            var filters = objectType.GetDefaultFilters(filter) ?? new Filters<T>();

            var sorted = objectType.GetDefaultSorted() ?? new Sorts<T>();

            sorted.Add(true, arg => (arg as IEntity).DateCreated, true);

            return list.PaginateAsync<T>(pageNumber, pageSize, sorted, filters);
        }

        #region Воин начало

        public enum DirectionSort
        {
            Asc,
            Desc,
        }

        public IQueryable<IEntity> AddOrder<T, TE>(IQueryable<IEntity> list, T typeObject, TE propertyTypeObject, string propertyName, DirectionSort desc) where T:IEntity
        {
            var orderExpression = GetExpression(typeObject, propertyTypeObject, propertyName);
            IQueryable<T> c;

            switch (desc)
            {
                case DirectionSort.Asc:
                    c = ((IQueryable<T>)list).OrderBy(orderExpression);
                    break;
                case DirectionSort.Desc:
                    c = ((IQueryable<T>)list).OrderByDescending(orderExpression);
                    break;
                default:
                    c = ((IQueryable<T>)list).OrderBy(orderExpression);
                    break;
            }

            return c as IQueryable<IEntity>;
        }

        public Expression<Func<T, TE>> GetExpression<T, TE>(T typeObject, TE propertyTypeObject, string propertyName)
        {
            var paramentr = Expression.Parameter(typeof(T), "member");
            var member = Expression.Property(paramentr, propertyName);
            var lymbda = Expression.Lambda<Func<T, TE>>(member, paramentr);
            return lymbda;
        }
        #endregion

        [Authorize]
        public IActionResult Delete(string type, int id)
        {
            var t = Type.GetType("Armada.Models." + type);

            if(CheckIsSingle(t))
                return RedirectToAction("EditFirst", new { type = type });

            dynamic objdect = Activator.CreateInstance(t);
            Delete(objdect, id);

            return RedirectToAction("GetList", new { type = type});
        }

        private void Delete<T>(T objec, int id) where T : Entity<T>
        {
            var set = Context.Set<T>().AsQueryable();

            var first = set.FirstOrDefault(w => w.Id == id);

            if(first == null) return;

            Context.Remove(first);
            Context.SaveChanges();
        }

        [Authorize]
        public IActionResult Details(string type, int id)
        {
            var t = Type.GetType("Armada.Models." + type);
            dynamic objdect = Activator.CreateInstance(t);

            ViewBag.Type = t;
            ViewBag.NameType = type;

            return View(GetObject(objdect, id));
        }

        private T GetObject<T>(T type, int id) where T : Entity<T>
        {
            var set = Context.Set<T>().AsQueryable();

            var strType = typeof(string);
            var ienumerableType = typeof(IEnumerable);
            var clases = type.GetType().GetProperties().Where(w => (w.PropertyType.IsClass || w.PropertyType.IsArray) && w.PropertyType != strType);

            var classesSet = clases.Aggregate(set, (current, property) =>
            {
                var ccc = current.Include(property.Name);

                if (property.PropertyType.IsArray || property.PropertyType.GetInterfaces().Any(w => w == ienumerableType))
                {
                    var types = property.PropertyType.GenericTypeArguments;

                    foreach (var type2 in types)
                    {
                        ccc = type2.GetProperties().Where(w => (w.PropertyType.IsClass || w.PropertyType.IsArray) && w.PropertyType != strType).Aggregate(ccc, (cc, pro) =>
                        {
                            var a = cc.Include($"{property.Name}.{pro.Name}");
                            return a;
                        });
                    }
                }
                return ccc;
            });

            return classesSet.FirstOrDefault(w => w.Id == id);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create(string type)
        {
            var t = Type.GetType("Web.Models." + type);

                        if(CheckIsSingle(t))
                return RedirectToAction("EditFirst", new { type = type });

            dynamic objdect = Activator.CreateInstance(t);
            ViewBag.NameType = type;

            return View(objdect);
        }

        [HttpGet]
        [Authorize]
        public IActionResult EditFirst(string type)
        {
            var t = Type.GetType("Armada.Models." + type);
            dynamic objdect = Activator.CreateInstance(t);
            var first = GetObjectToFirstEdit(objdect);

            if (first == null) return NotFound();

            return RedirectToAction("Edit", new {type = type, id = first.Id});
        }

        private T GetObjectToFirstEdit<T>(T type) where T : Entity<T>
        {
            var first = Context.Set<T>().FirstOrDefault();
            return first;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(string type, int id)
        {
            var t = Type.GetType("Armada.Models." + type);
            dynamic objdect = Activator.CreateInstance(t);
            ViewBag.NameType = type;
            ViewBag.Type = t;

            return View(GetObject(objdect, id));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit()
        {
            var keys = Request.Form.Keys?.Select(w => w.ToLower()).ToList() ?? new List<string>();

            if (!keys.Contains("type") || !keys.Contains("id")) RedirectToAction("GetList", new { type = "Order" }); ;

            var typeName = Request.Form["type"];
            var id = Request.Form["id"];
            var type = Type.GetType("Armada.Models." + typeName);
            var properties = type.GetProperties();
            dynamic newObject = Activator.CreateInstance(type);
            var editObject = GetObject(newObject, int.Parse(id));

            var existsProperties = properties.Where(w =>
            {
                var propertyName = w.Name.ToLower();
                return keys.Contains(propertyName);
            });

            foreach (var key in existsProperties)
            {
                var value = Request.Form[key.Name];
                var strValue = value.ToString();

                //TODO Костыль ебаный
                if (key.PropertyType == Types.Bool && strValue == "true,false")
                {
                    strValue = "true";
                }

                var typeConvert = key.PropertyType;
                if (typeConvert.IsGenericType && typeConvert.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    if (!string.IsNullOrEmpty(value.FirstOrDefault()))
                    {
                        typeConvert = Nullable.GetUnderlyingType(typeConvert);

                        var changedType = Convert.ChangeType(strValue, typeConvert);

                        key.SetValue(editObject, changedType);
                    }
                }
                else
                {
                    var changedType = Convert.ChangeType(strValue, key.PropertyType);

                    key.SetValue(editObject, changedType);
                }
            }

            Context.Update(editObject);
            Context.SaveChanges();

            return CheckIsSingle(type) ? RedirectToAction("EditFirst", new { type = typeName }) : RedirectToAction("GetList", new { type = typeName });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create()
        {
            var keys = Request.Form.Keys?.Select(w => w.ToLower()).ToList() ?? new List<string>();

            if (!keys.Contains("type")) RedirectToAction("GetList", new { type = "Order" });

            var typeName = Request.Form["type"];
            var type = Type.GetType("Armada.Models." + typeName);

            if (CheckIsSingle(type))
                return RedirectToAction("EditFirst", new { type = type });

            var properties = type.GetProperties();
            var newObject = Activator.CreateInstance(type);

            var existsProperties = properties.Where(w =>
            {
                var propertyName = w.Name.ToLower();
                return keys.Contains(propertyName);
            });

            foreach (var key in existsProperties)
            {
                var value = Request.Form[key.Name];

                var strValue = value.ToString();

                //TODO Костыль ебаный
                if (key.PropertyType == Types.Bool && strValue == "true,false")
                {
                    strValue = "true";
                }

                var typeConvert = key.PropertyType;
                if (typeConvert.IsGenericType && typeConvert.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    if (!string.IsNullOrEmpty(value.FirstOrDefault()))
                    {
                        typeConvert = Nullable.GetUnderlyingType(typeConvert);

                        var changedType = Convert.ChangeType(strValue, typeConvert);

                        key.SetValue(newObject, changedType);
                    }
                }
                else
                {
                    var changedType = Convert.ChangeType(strValue, key.PropertyType);

                    key.SetValue(newObject, changedType);
                }
            }

            Context.Add(newObject);
            Context.SaveChanges();

            return RedirectToAction("GetList", new {type = typeName});
        }

        #region FileWork
        [HttpPost]
        public int AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                var file = new FileModel
                {
                    Name = uploadedFile.FileName ,
                    FileType = uploadedFile.ContentType,
                };
                Context.Files.Add(file);
                Context.SaveChanges();

                var indexOf = uploadedFile.ContentType.IndexOf('/');
                var extension = indexOf == -1 ? "file" : uploadedFile.ContentType.Substring(indexOf + 1);
                
                string path = "/Files/" + file.Id + "." + extension;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    uploadedFile.CopyTo(fileStream);
                }

                file.Path = path;
                Context.SaveChanges();

                return file.Id;
            }
            return -1;
        }

        [HttpGet]
        public IActionResult AddFile(int id, string idFileProperty)
        {
            var fileViewModel = new FileViewModel()
            {
                Id = idFileProperty,
                FileId = id,
            };

            return View("File", fileViewModel);
        }

        public IActionResult RemoveFile(int id, string idFileProperty)
        {
            var fileViewModel = new FileViewModel()
            {
                Id = idFileProperty
            };

            var file = Context.Files.FirstOrDefault(w => w.Id == id);
            if (file != null)
            {
                Context.Files.Remove(file);
                Context.SaveChanges();
            }

            return View("File", fileViewModel);
        }
        #endregion

        #region ImageWork
        [HttpPost]
        public int AddImage(IFormFile uploadedImage)
        {
            if (uploadedImage != null)
            {
                var indexOf = uploadedImage.ContentType.IndexOf('/');
                var extension = indexOf == -1 ? "file" : uploadedImage.ContentType.Substring(indexOf + 1);

                var image = new Image
                {
                    Title = uploadedImage.FileName,
                    ImageType = uploadedImage.ContentType,
                    Extension = extension,
                };

                Context.Images.Add(image);
                Context.SaveChanges();

                var path = "/UploadImages/" + image.Id + "." + extension;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    uploadedImage.CopyTo(fileStream);
                }

                image.Url = path;
                Context.SaveChanges();

                return image.Id;
            }
            return -1;
        }

        [HttpGet]
        public IActionResult AddImage(int id, string idImageProperty)
        {
            var image = Context.Images.FirstOrDefault(w => w.Id == id);

            var imageViewModel = new ImageViewModel()
            {
                Id = idImageProperty,
                ImageId = id,
                Url = image?.Url,
            };

            return View("Image", imageViewModel);
        }

        public IActionResult RemoveImage(int id, string idImageProperty)
        {
            var imageViewModel = new ImageViewModel()
            {
                Id = idImageProperty
            };

            var image = Context.Images.FirstOrDefault(w => w.Id == id);
            if (image != null)
            {
                Context.Images.Remove(image);
                Context.SaveChanges();
            }

            return View("Image", imageViewModel);
        }
        #endregion

  
        //Регион для добавления изображения в связи один ко многим
        #region ManyImageWork
        [HttpPost]
        public int AddOneToManyImage(IFormFile uploadedImage, string propertyName, int id)
        {
            if (uploadedImage != null && !string.IsNullOrEmpty(propertyName))
            {
                var indexOf = uploadedImage.ContentType.IndexOf('/');
                var extension = indexOf == -1 ? "file" : uploadedImage.ContentType.Substring(indexOf + 1);

                var image = new Image
                {
                    Title = uploadedImage.FileName,
                    ImageType = uploadedImage.ContentType,
                    Extension = extension,
                };

                var property = Types.Image.GetProperty(propertyName);
                property.SetValue(image, id);

                Context.Images.Add(image);
                Context.SaveChanges();

                var path = "/UploadImages/" + image.Id + "." + extension;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    uploadedImage.CopyTo(fileStream);
                }

                image.Url = path;
                Context.SaveChanges();

                return image.Id;
            }
            return -1;
        }

        [HttpGet]
        public IActionResult AddOneToManyImage(int id, string propertyName)
        {
            var image = Context.Images.FirstOrDefault(w => w.Id == id);

            var imageViewModel = new OneToManySingleImage()
            {
                Id = id,
                LinkPropertyName = propertyName,
                Url = image?.Url,
            };

            return View("OneToManySingleImage", imageViewModel);
        }

        public IActionResult RemoveOneToManyImage(int id)
        {
            var image = Context.Images.FirstOrDefault(w => w.Id == id);
            if (image != null)
            {
                Context.Images.Remove(image);
                Context.SaveChanges();
            }

            return new OkResult();
        }


        #endregion
    }
}