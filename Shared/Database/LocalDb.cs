﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Shared.Models;
using SQLite;

namespace Shared.Database
{
    public class LocalDb : ILocalDb
    {
        public static LocalDb Instance { get; }

        static LocalDb()
        {
            Instance = new LocalDb();
        }

        private static readonly object Locker = new object();

        private readonly SQLiteConnection _database;
        private const int CountDateRecords = 30;
        private const string SqliteFilename = "database1.db3";

        private static string DatabasePath => GetFullPath(SqliteFilename);

        public static string GetFullPath(string filename)
        {
    #if __IOS__
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var libraryPath = Path.Combine(documentsPath, "..", "Library");
                var path = Path.Combine(libraryPath, filename);
    #else
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var path = Path.Combine(documentsPath, filename);
    #endif
                return path;
        }

        /// <summary>
        /// https://github.com/praeclarum/sqlite-net/wiki
        /// Всю информацию по автоматическим миграциям и польность по sql lite et pcl можно найти по этой ссылке
        /// </summary>
        private LocalDb()
        {
            lock (Locker)
            {
                try
                {
                    _database = new SQLiteConnection(DatabasePath);

                    UpdateTable<LocalUserModel>();
                    UpdateTable<Models.Credit>();
                    UpdateTable<Models.AvialableCredit>();

                }
                catch (Exception e)
                {
                }
            }
        }

        private void UpdateTable<T>()
        {
            try
            {
                _database.CreateTable<T>();
            }
            catch (Exception e)
            {
            }
        }

        #region Удаление
        #region Удаление элементов по локальному id
        /// <summary>
        /// Удаление элемента по локальному id
        /// </summary>
        public void DeleteItemByLocalId<T>(int id) where T : IEntity, new()
        {
            lock (Locker)
            {
                _database.Delete<T>(id);
            }
        }
        /// <summary>
        /// Удаление элемента по локальному id
        /// </summary>
        public void DeleteItemByLocalId<T>(T item) where T : IEntity, new()
        {
            lock (Locker)
            {
                _database.Delete(item);
            }
        }
        /// <summary>
        /// Удаление элементов по локальному id
        /// </summary>
        public void DeleteItemsByLocalId<T>(IEnumerable<T> items) where T : IEntity, new()
        {
            foreach (var item in items)
            {
                DeleteItemByLocalId<T>(item);
            }
        }
        /// <summary>
        /// Удаление списка элементов по внутреннему id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemIds"></param>
        public void DeleteItemsByLocalId<T>(IEnumerable<int> itemIds) where T : IEntity, new()
        {
            foreach (var itemId in itemIds)
            {
                DeleteItemByLocalId<T>(itemId);
            }
        }

        /// <summary>
        /// Удаление всех элементов таблицы
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void DeleteAll<T>() where T : IEntity, new()
        {
            lock (Locker)
            {
                _database.DeleteAll<T>();
            }
        }
        #endregion
        #endregion

        #region Добавление нового элемента
        /// <summary>
        /// Добавление нового элемента
        /// </summary>
        public int AddNewItem<T>(T item) where T : IEntity, new()
        {
            lock (Locker)
            {
                _database.Insert(item);
                return item.Id;
            }
        }
        /// <summary>
        /// Добавление новых элементов
        /// </summary>
        public void AddNewItems<T>(IEnumerable<T> items) where T : IEntity, new()
        {
            lock (Locker)
            {
                _database.InsertAll(items);
            }
        }
        #endregion

        #region Обновление элементов
        /// <summary>
        /// Обновление элемента по локальному id
        /// </summary>
        public void UpdateItemByLocalId<T>(T item) where T : IEntity, new()
        {
            lock (Locker)
            {
                _database.Update(item);
            }
        }
        /// <summary>
        /// Обновление элементов по локальному id
        /// </summary>
        public void UpdateItemsByLocalId<T>(IEnumerable<T> items) where T : IEntity, new()
        {
            foreach (var item in items)
            {
                UpdateItemByLocalId(item);
            }
        }
        #endregion

        #region Получение элементов
        /// <summary>
        /// Получение элемента по локальному id
        /// </summary>
        public T GetItemByLocalId<T>(int id) where T : IEntity, new()
        {
            lock (Locker)
            {
                return _database.Table<T>().ToList().FirstOrDefault(w => w.Id == id);
            }
        }
        /// <summary>
        /// Получение первого элемента в таблице
        /// </summary>
        public T GetFirstItem<T>() where T : IEntity, new()
        {
            lock (Locker)
            {
                return _database.Table<T>().FirstOrDefault();
            }
        }

        public IEnumerable<T> GetItems<T>() where T : IEntity, new()
        {
            lock (Locker)
            {
                return _database.Table<T>()?.ToList() ?? new List<T>();
            }
        }

        #endregion

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}
