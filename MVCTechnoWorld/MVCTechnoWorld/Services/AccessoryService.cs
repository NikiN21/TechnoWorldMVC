using MVCTechnoWorld.Abstractions;
using MVCTechnoWorld.Data;
using MVCTechnoWorld.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTechnoWorld.Services
{
    public class AccessoryService : IAccessoryService
    {
        private readonly ApplicationDbContext _context;

        public AccessoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(string type, string category, string brand, string description, string picture, decimal price, int quantity, decimal discount) // string userId
        {
            Accessory item = new Accessory
            {
                Type = type,
                Category = category,
                Brand = brand,
                Description = description,
                Picture = picture,
                Price = price,
                Quantity = quantity
            };

            _context.Accessories.Add(item);
            return _context.SaveChanges() != 0;// Връща броя на записите, които си обработил
        }



        public Accessory GetAccessoryById(int accessoryId)
        {
            return _context.Accessories.Find(accessoryId);
        }

        public List<Accessory> GetAccessory()
        {
            List<Accessory> accessories = _context.Accessories.ToList();
            return accessories;
        }



        public bool RemoveById(int productId)
        {
            var accessory = GetAccessoryById(productId);
            if (accessory == default(Accessory))
            {
                return false;
            }
            _context.Remove(accessory);
            return _context.SaveChanges() != 0;
        }

        public bool UpdateAccessory(int accessoryId, string type, string category, string brand, string description, string picture, decimal price, int quantity, decimal discount)
        {
            {
                var accessory = GetAccessoryById(accessoryId);
                if (accessory == default(Accessory))
                {
                    return false;
                }
                accessory.Type = type;
                accessory.Category = category;
                accessory.Brand = brand;
                accessory.Description = description;
                accessory.Picture = picture;
                accessory.Price = price;
                accessory.Quantity = quantity;
                _context.Update(accessory);
                return _context.SaveChanges() != 0;
            }
        }

        public List<Accessory> GetAccessories(string searchStringType, string searchStringBrand)
        {
            {
                List<Accessory> accessories = _context.Accessories.ToList();
                if (!String.IsNullOrEmpty(searchStringType) && !String.IsNullOrEmpty(searchStringBrand))
                {
                    accessories = accessories.Where(d => d.Type.Contains(searchStringType) && d.Brand.Contains(searchStringBrand)).ToList();
                }
                else if (!String.IsNullOrEmpty(searchStringType))
                {
                    accessories = accessories.Where(d => d.Type.Contains(searchStringType)).ToList();
                }
                else if (!String.IsNullOrEmpty(searchStringBrand))
                {
                    accessories = accessories.Where(d => d.Brand.Contains(searchStringBrand)).ToList();
                }
                return accessories;
            }
        }
    }
}