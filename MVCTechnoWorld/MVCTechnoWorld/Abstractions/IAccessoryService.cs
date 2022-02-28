using MVCTechnoWorld.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTechnoWorld.Abstractions
{
    public interface IAccessoryService
    {
        bool Create(string type, string category, string brand, string description, string picture, decimal price, int quantity, decimal discount);
        bool UpdateAccessory(int accessoryId, string type, string category, string brand, string description, string picture, decimal price, int quantity, decimal discount);
        List<Accessory> GetAccessory();
        Accessory GetAccessoryById(int accessoryId);
        bool RemoveById(int accessoryId);
        List<Accessory> GetAccessories(string searchStringType, string searchStringBrand);
    }
}
