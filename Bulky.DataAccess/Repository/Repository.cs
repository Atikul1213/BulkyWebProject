﻿using Bulky.DataAccess.Repository.IRepository;
using BulkyWeb.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            // _db.Categories = dbSet
            _db.Products.Include(u => u.Category).Include(u=>u.CategoryId);
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }


        //public T Get(Expression<Func<T, bool>> filter)
        //{
        //    IQueryable<T> query = dbSet;
        //    query = query.Where(filter);
        //    return query.FirstOrDefault();
        //}


        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var obj in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(obj);
                }
            }
            return query.FirstOrDefault();

        }






        //public IEnumerable<T> Getall()
        //{
        //    IQueryable<T> query = dbSet;
        //    return query.ToList();
        //}

        // Category , CategoryId
        public IEnumerable<T> Getall(string? includeProperties=null)
        {


            IQueryable<T> query = dbSet;
            if(!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var obj in includeProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(obj);
                }
            }
            return query.ToList();
        }



        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
          {
              dbSet.RemoveRange(entity);
          }




        }
}
