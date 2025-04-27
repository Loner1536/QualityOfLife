using MelonLoader;
using ScheduleOne.Product;
using ScheduleOne.ItemFramework;

namespace QualityOfLife.Utility.Products;

public class GetDrugType
{
    // 0 - Weed
    // 1 - Meth
    // 2 - Cocaine
    public static string Get(string drugName)
    {
        if (ProductManager.ListedProducts.Count > 0)
        {
            foreach (ProductDefinition current in ProductManager.ListedProducts)
            {
                if (((ItemDefinition)current).Name == drugName)
                {
                    return DecideType((int)current.DrugType);
                }
            }
        }
        if (ProductManager.DiscoveredProducts.Count > 0)
        {
            foreach (ProductDefinition current2 in ProductManager.DiscoveredProducts)
            {
                if (((ItemDefinition)current2).Name == drugName)
                {
                    return DecideType((int)current2.DrugType);
                }
            }
        }
        return null;
    }
    private static string DecideType(int drugType)
    {
        if (drugType == 0)
        {
            return "weed";
        }
        else if (drugType == 1)
        {
            return "meth";
        }
        else if (drugType == 2)
        {
            return "cocaine";
        }
        else
        {
            MelonLogger.Warning($"Unknown drug type: {drugType}");
            return null;
        }
    }
}
