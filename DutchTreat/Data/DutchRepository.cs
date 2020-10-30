﻿using DutchTreat.Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _context;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext context, ILogger<DutchRepository> logger)
        {
            this._context = context;
            this._logger = logger;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Get all products was called");
                return _context.Products.OrderBy(p => p.Title).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all the produchts: {ex}");
                throw ex;
            }
        }
        public IEnumerable<Product> GetProductsByCategoty(string category)
        {
            try
            {
                return _context.Products.Where(p => p.Category == category).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get the produchts by category: {ex}");
                throw ex;
            }
        }
        public bool SaveAll()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed save: {ex}");
                throw ex;
            }

        }
    }
}
