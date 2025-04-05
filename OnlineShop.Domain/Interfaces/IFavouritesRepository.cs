﻿using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Interfaces
{
    public interface IFavouritesRepository
    {
        Task<Favourites?> GetById(string userId);
        Task<Product?> GetProductById(Guid Id);
        Task Add(Guid id, string userId);
        Task Remove(Guid id, string userId);
    }
}
