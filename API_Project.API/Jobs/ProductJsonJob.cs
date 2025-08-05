using API_Project.API.Dto;
using Newtonsoft.Json;
using System.Globalization;
namespace API_Project.API.Jobs;
public class ProductJsonJob
{
    private readonly HttpClient _httpClient;
    private const string ApiUrl = "https://localhost:7274/api/products"; // REST API endpoint'in

    public ProductJsonJob(HttpClient httpClient)
    {
        _httpClient = new HttpClient();
    }

    public async Task ExecuteAsync()
    {
        // 1. Veriyi çek
        var response = await _httpClient.GetAsync(ApiUrl);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var productList = JsonConvert.DeserializeObject<List<ProductDto>>(content);

        // 2. Önceki dosyayı sil
        // 2. JSON dosyasının kaydedileceği klasör
        var directory = @"C:\Users\ali.ayas\source\repos\aliayass\ProductRestService\API_Project.API\Json";
        Directory.CreateDirectory(directory); // klasör yoksa oluştur
        var oldFiles = Directory.GetFiles(directory, "*.json");
        foreach (var file in oldFiles)
            File.Delete(file);

        // 3. Yeni dosya adını oluştur
        var now = DateTime.Now;
        var timestamp = now.ToString("yyyyMMdd_HHmm", CultureInfo.InvariantCulture);
        var fileName = $"products_{timestamp}.json";
        var filePath = Path.Combine(directory, fileName);

        // 4. JSON olarak kaydet
        var json = JsonConvert.SerializeObject(productList, Formatting.Indented);
        await File.WriteAllTextAsync(filePath, json);

        Console.WriteLine($"[✓] Ürün listesi kaydedildi: {fileName}");
    }
}
