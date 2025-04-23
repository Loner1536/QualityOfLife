namespace QualityOfLife.Utils.Products;

public class GetDrugName
{
    public static string Get(string line)
    {
        if (string.IsNullOrEmpty(line))
        {
            return string.Empty;
        }
        string text = line.Split(new char[1] { ',' })[0].Trim();
        int num = text.IndexOf("x ");
        return (num == -1) ? "" : text.Substring(num + 2).Trim();
    }
}
