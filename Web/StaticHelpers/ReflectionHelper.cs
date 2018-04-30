using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Web.Controllers.cms;
using Web.Models;

namespace Web.StaticHelpers
{
    public static class ReflectionHelper
    {
        public static string GetNameProperty(PropertyInfo info)
        {
            var attr = info.GetCustomAttribute<DisplayNameAttribute>();
            return attr == null ? info.Name : attr.DisplayName;
        }

        public static string GetValuePropertyAsString(PropertyInfo info, dynamic objec)
        {
            if (objec is IEntity entity)
            {
                var valueFromObject = entity.GetValueFromNameProperty(info.Name);

                if (valueFromObject != null) return valueFromObject;
            }
            try
            {
                var value = info.GetValue(objec, null);
                return value?.ToString();
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static string GetValuePropertyAsTitle(PropertyInfo info, dynamic objec)
        {
            if (objec is IEntity entity)
            {
                var valueFromObject = entity.GetTitleFromNameProperty(info.Name);

                if (valueFromObject != null) return valueFromObject;
            }
            try
            {
                var value = info.GetValue(objec, null);

                return (value as IEntity)?.Title;
            }
            catch (Exception e)
            {
                return "Объект";
            }
        }

        public static string GetValuePropertyAsLink(PropertyInfo info, dynamic objec)
        {
            if (objec is IEntity entity)
            {
                var valueFromObject = entity.GetLinkFromNameProperty(info.Name);

                if (valueFromObject != null) return valueFromObject;
            }
            try
            {
                var value = info.GetValue(objec, null);
                return $"/HomeCms/Details?type={info.Name}&id={(value as IEntity)?.Id}";
            }
            catch (Exception e)
            {
                return "HomeCms/GetList?type=Order";
            }
        }

        public static IEnumerable<PropertyInfo> GetPropertiesInList(Type type)
        {
            return type.GetProperties().Where(w => w.GetCustomAttribute<ShowAttribute>() == null || w.GetCustomAttribute<ShowAttribute>().ShowInList);
        }

        public static IEnumerable<PropertyInfo> GetProperiesInDetails(Type type)
        {
            return type.GetProperties().Where(w => w.GetCustomAttribute<ShowAttribute>() == null || w.GetCustomAttribute<ShowAttribute>().ShowInDetails);
        }

        public static IEnumerable<PropertyInfo> GetProperiesInEdit(Type type)
        {
            return type.GetProperties().Where(w => w.GetCustomAttribute<ShowAttribute>() == null || w.GetCustomAttribute<ShowAttribute>().ShowInEdit);
        }
        public static IEnumerable<PropertyInfo> GetProperiesInCreate(Type type)
        {
            return type.GetProperties().Where(w => w.GetCustomAttribute<ShowAttribute>() == null || w.GetCustomAttribute<ShowAttribute>().ShowInCreate);
        }

        public static IQueryable<IEntity> InsertInclude<T>(IQueryable<IEntity> query, Type type) 
            where T: Attribute
        {
            var showTitles = type.GetProperties().Where(w => w.GetCustomAttribute<T>() != null);

            return showTitles.Aggregate(query, (current, property) => current.Include(property.Name));
        }

        public static bool IsHtml(PropertyInfo info)
        {
            var type = info.GetCustomAttribute<DataTypeAttribute>();
            return type?.DataType == DataType.Html;
        }
    }
}
