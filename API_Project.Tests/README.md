# ✅ API_Project.Tests — Test Rehberi

Bu klasör, `ProductRestService` projesinin birim testlerini içerir. Testler, `API_Project.API` içinde yer alan `ProductController` mantığını hızlı ve güvenilir şekilde doğrular.

## 🧰 Kullanılan Teknolojiler

- xUnit — test çatı sistemi
- FluentAssertions — okunabilir ve güçlü assertion'lar
- EF Core InMemory — gerçek veritabanına ihtiyaç duymadan test veri kaynağı

## 📦 Kapsam ve Senaryolar

`ProductControllerTests`
- Tüm ürünleri listeleme: `GetAllProducts_ShouldReturnAllProducts`
- Ürün ekleme: `CreateProduct_ShouldAddProduct`
- Ürün güncelleme: `UpdateProduct_ShouldModifyExistingProduct`
- Ürün silme: `DeleteProduct_ShouldRemoveProduct`
- Hatalı JSON tipleri için doğrulama: `Post_WithInvalidJsonTypeForInt_ShouldFailTestWithoutException`, `Post_WithInvalidJsonType_ShouldThrowJsonException`

> Not: Testler, her çalıştırmada benzersiz bir InMemory veritabanı (`Guid.NewGuid()`) oluşturur; böylece testler birbirini etkilemez.

## ▶️ Testleri Çalıştırma

```bash
# Tüm testleri çalıştır
dotnet test

# Değişiklik izleme ile testleri sürekli çalıştır (opsiyonel)
dotnet watch --project ./API_Project.Tests test

# Kod kapsamı toplama (cross-platform yerleşik rapor)
dotnet test --collect:"XPlat Code Coverage"
```

## 🧪 Hızlı Bakış: EF Core InMemory yaklaşımı

```csharp
// InMemory veritabanı ile izole bir DbContext üretimi
var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseInMemoryDatabase("ProductTestDb_" + Guid.NewGuid()) // her test için ayrı DB
    .Options;
var context = new AppDbContext(options);
```

## ✍️ Yeni Test Yazım Notları

- Test isimlerini “Should...” formatında, davranışa odaklı yazın.
- AAA deseni kullanın: Arrange, Act, Assert.
- Test verilerini açık ve minimum gerekli alanlarla oluşturun.
- Controller dönen sonuçları `OkObjectResult` gibi tiplerle kontrol edin; `Value` içeriğini doğrulayın.

## 🔧 Sık Karşılaşılan Durumlar

- Model serileştirme hatalarını doğrularken `System.Text.Json.JsonException` beklenen durum olabilir.
- Sayısal alanlar (ör. `Price`, `StokSayisi`) için hatalı tipte JSON geldiğinde test davranışlarını net ifade eden assertion'lar kullanın.

---

Daha fazla bilgi için proje ana dokümantasyonuna göz atın: `../README.md`


