using InventorySystem.Data;
using InventorySystem.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Type = InventorySystem.ViewModels.Type;

namespace InventorySystem.Repository
{
    public class TypeRepository : ITypeRepository
    {
        private readonly InventoryDb inventoryDb;
        private readonly ILogger<TypeRepository> logger;

        public TypeRepository(InventoryDb _inventoryDb, ILogger<TypeRepository> _logger)
        {
            this.inventoryDb = _inventoryDb;
            this.logger = _logger;
        }

        public async Task<Type> Create(Type obj)
        {
            try
            {
                obj.IsActive = true;
                await inventoryDb.Types.AddAsync(obj);
                await inventoryDb.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in Create New Type: {ex.Message}", ex);
                return new Type();
            }
        }

        public async Task<Type> Edit(Type obj)
        {
            try
            {
                var updatedType = await inventoryDb.Types.FirstOrDefaultAsync(u => u.TypeId == obj.TypeId);
                if (updatedType != null)
                {
                    updatedType.Name = obj.Name;
                    updatedType.IsActive = true;
                    inventoryDb.Update(updatedType);
                    await inventoryDb.SaveChangesAsync();
                    return updatedType;
                }
                return new Type();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in Edit Type method: {ex.Message}", ex);
                return new Type();
            }
        }

        public async Task<Type> Get(int id)
        {
            try
            {
                return await inventoryDb.Types.FirstOrDefaultAsync(u => u.TypeId == id && u.IsActive == true) ?? new Type();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in Get Type method: {ex.Message}", ex);
                return new Type();
            }
        }

        public async Task<Type> getType(int id)
        {
            try
            {
                return await inventoryDb.Types.FirstOrDefaultAsync(u => u.TypeId == id && u.IsActive == true) ?? new Type();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in getType method: {ex.Message}", ex);
                return new Type();
            }
        }

        public async Task<IEnumerable<Type>> GetAll()
        {
            try
            {
                return await inventoryDb.Types.Where(u => u.IsActive == true).OrderBy(u => u.TypeId).ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in GetAll Types method: {ex.Message}", ex);
                return new List<Type>();
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var type = await inventoryDb.Types.FirstOrDefaultAsync(u => u.TypeId == id);
                if (type != null)
                {
                    type.IsActive = false;
                    inventoryDb.Types.Update(type);
                    return (await inventoryDb.SaveChangesAsync()) > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in In Inactive Type method: {ex.Message}", ex);
                return false;
            }
        }
        public async Task<bool> CheckIfTypeExist(string name, int id = 0)
        {
            try
            {

                if (id != 0)
                {
                    var type = await inventoryDb.Types.FirstOrDefaultAsync(u => u.Name.ToLower() == name.ToLower() && u.IsActive == true && u.TypeId != id);
                    if (type != null)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    var type = await inventoryDb.Types.FirstOrDefaultAsync(u => u.Name.ToLower() == name.ToLower() && u.IsActive == true);
                    if (type != null)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in Check If Type Exist method: {ex.Message}", ex);
                return false;
            }
        }
    }
}