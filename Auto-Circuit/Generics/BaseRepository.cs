﻿using Auto_Circuit.Data;
using Auto_Circuit.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Auto_Circuit.Generics;

public class BaseRepository(CircuitContext context) : IRepository
{
    public async Task AddAsync<T>(T entity) where T : class
    {
        context.Set<T>().Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync<T>(string id) where T : class
    {
        var itemToDelete = await context.Set<T>().FindAsync(id);
        if (itemToDelete == null)
        {
            throw new Exception($"Item of type {typeof(T).Name} with ID '{id}' not found.");
        }

        context.Set<T>().Remove(itemToDelete);
        await context.SaveChangesAsync();
    }


    public async Task<T> GetByIdAsync<T>(string id) where T : class
    {
        var item = await context.Set<T>().FindAsync(id);
        try
        {
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new Exception("Item is not Found !");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Server Error", ex);
        }
    }

    public IQueryable<T> GetData<T>() where T : class
    {
        var Data = context.Set<T>().AsNoTracking().AsQueryable();
        return Data;
    }

    public async Task UpdateAsync<T>(T entity) where T : class
    {
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

}
