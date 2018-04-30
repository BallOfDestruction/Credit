using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EntityFrameworkPaginateCore;
using Web.Controllers.cms;

namespace Web.Models
{
    public class Entity<T> : ISortFilterEntity<T>
        where T : IEntity
    {
        [Show(false, false, false, false)]
        public int Id { get; set; }

        [DisplayName("Дата создания")]
        [DataType(DataType.DateTime)]
        [Show(true, true, false, false)]
        public virtual DateTime DateCreated { get; set; }

        [Show(false, false, false, false)]
        public virtual string Title { get; set; }

        public Entity()
        {
            DateCreated = DateTime.Now;
        }

        public virtual string GetValueFromNameProperty(string nameProperty)
        {
            return null;
        }

        public virtual string GetLinkFromNameProperty(string nameProperty)
        {
            return null;
        }

        public virtual string GetTitleFromNameProperty(string nameProperty)
        {
            return null;
        }

        public virtual Filters<T> GetDefaultFilters(params string[] filters)
        {
            var filter = new Filters<T>();
            return filter;
        }

        public virtual Sorts<T> GetDefaultSorted()
        {
            var sorted = new Sorts<T>();
            return sorted;
        }
    }

    public interface IEntity
    {
        int Id { get; }
        string Title { get; }
        DateTime DateCreated { get; }
        string GetValueFromNameProperty(string nameProperty);
        string GetLinkFromNameProperty(string nameProperty);
        string GetTitleFromNameProperty(string nameProperty);
    }

    public interface ISortFilterEntity<T> : IEntity 
        where T : IEntity
    {
        Filters<T> GetDefaultFilters(params string[] filters);
        Sorts<T> GetDefaultSorted();
    }
}
