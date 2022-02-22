using MVCTechnoWorld.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTechnoWorld.Abstractions
{
    public interface IAccessoryService
    {
        bool Create(string type, string brand, string color, decimal price, string description, string picture);
        bool UpdateAccessory(int accessoryId, string type, string brand, string color, decimal price, string description, string picture);
        List<Accessory> GetAccessory();
        Accessory GetAccessoryById(int accessoryId);
        bool RemoveById(int accessoryId);
        List<Accessory> GetAccessories(string searchStringType, string searchStringBrand);
    }
}
