# 🛍️ Product REST API

Modern .NET 8 ve Entity Framework Core kullanarak geliştirilmiş, PostgreSQL veritabanı ile entegre edilmiş REST API projesi.

## 📋 Proje Özeti

Bu proje, ürün yönetimi için CRUD (Create, Read, Update, Delete) operasyonları sunan bir REST API oluşturur. Postman ile test yapmayı öğrenmek ve REST mimarisinin mantığını incelemek amacıyla geliştirilmiştir.

**Not:** Bu proje, aynı işlevselliği SOAP protokolü ile gerçekleştiren [ProductSoapService](https://github.com/aliayass/ProductSoapService) projesinin REST versiyonudur.

## 🏗️ Mimari Yapı

```
API_Project.API/
├── Controllers/     # REST API endpoint'leri
├── Models/          # Entity modelleri
├── Dto/            # Data Transfer Objects
├── Data/           # Database context
└── Migrations/     # Database migrations
```

## 🛠️ Teknoloji Stack

* **.NET 8** - Modern framework
* **Entity Framework Core** - ORM
* **PostgreSQL** - Veritabanı
* **REST API** - HTTP protokolü
* **Swagger/OpenAPI** - API dokümantasyonu
* **Layered Architecture** - Temiz kod yapısı

## 🚀 Kurulum

### Gereksinimler

* .NET 8 SDK
* PostgreSQL
* Visual Studio 2022 veya VS Code
* Postman (test için)

### Adım 1: Projeyi Klonlayın

```bash
git clone https://github.com/aliayass/API_Project.API.git
cd API_Project.API
```

### Adım 2: Veritabanı Bağlantısını Yapılandırın

`appsettings.json` dosyasında PostgreSQL connection string'ini güncelleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=APIProjectDB;Username=postgres;Password=your_password"
  }
}
```

### Adım 3: Migration'ları Çalıştırın

```bash
dotnet ef database update
```

### Adım 4: Uygulamayı Başlatın

```bash
dotnet run
```

## 📡 API Endpoints

### Base URL

```
https://localhost:7001/api/products
```

### Swagger Dokümantasyonu

```
https://localhost:7001/swagger
```

## 🧪 Postman ile Test

### 1. Tüm Ürünleri Getir

**GET** `https://localhost:7001/api/products`

**Headers:**
```
Content-Type: application/json
```

**Response:**
```json
[
  {
    "id": 1,
    "barkod": "1234567890",
    "itemId": "PROD001",
    "renk": "Kırmızı",
    "beden": "M",
    "price": 99.99,
    "stokSayisi": 50
  }
]
```

### 2. Ürün Filtreleme

**GET** `https://localhost:7001/api/products/filter?barkod=1234567890&renk=Kırmızı`

**Query Parameters:**
- `id` (optional): Ürün ID'si
- `barkod` (optional): Barkod numarası
- `itemId` (optional): Ürün kodu
- `renk` (optional): Ürün rengi
- `beden` (optional): Ürün bedeni
- `price` (optional): Fiyat
- `stokSayisi` (optional): Stok sayısı

### 3. Yeni Ürün Ekle

**POST** `https://localhost:7001/api/products`

**Headers:**
```
Content-Type: application/json
```

**Body:**
```json
{
  "barkod": "1234567890",
  "itemId": "PROD001",
  "renk": "Kırmızı",
  "beden": "M",
  "price": 99.99,
  "stokSayisi": 50
}
```

### 4. Ürün Güncelle

**PUT** `https://localhost:7001/api/products`

**Headers:**
```
Content-Type: application/json
```

**Body:**
```json
{
  "id": 1,
  "barkod": "1234567890",
  "itemId": "PROD001",
  "renk": "Mavi",
  "beden": "L",
  "price": 129.99,
  "stokSayisi": 75
}
```

### 5. Ürün Sil

**DELETE** `https://localhost:7001/api/products/1`

### 6. Kısmi Güncelleme (PATCH)

**PATCH** `https://localhost:7001/api/products/1`

**Headers:**
```
Content-Type: application/json-patch+json
```

**Body:**
```json
[
  {
    "op": "replace",
    "path": "/price",
    "value": 89.99
  },
  {
    "op": "replace",
    "path": "/stokSayisi",
    "value": 25
  }
]
```

## 📊 Veritabanı Şeması

### Products Tablosu

| Alan        | Tip          | Açıklama                    | Kısıtlamalar                    |
| ----------- | ------------ | --------------------------- | ------------------------------- |
| Id          | INT          | Primary Key, Auto Increment | -                               |
| Barkod      | VARCHAR(10)  | Barkod numarası             | Sadece sayılar, max 10 karakter |
| ItemId      | VARCHAR(10)  | Ürün kodu                   | Büyük harf ve rakam, max 10    |
| Renk        | VARCHAR(50)  | Ürün rengi                  | -                               |
| Beden       | VARCHAR(50)  | Ürün bedeni                 | -                               |
| Price       | DECIMAL      | Ürün fiyatı                 | -                               |
| StokSayisi  | INT          | Stok miktarı                | -                               |

## 🔧 Konfigürasyon

### Program.cs

```csharp
// PostgreSQL veritabanı bağlantısı
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger konfigürasyonu
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
```

### Product Model Validasyonları

```csharp
[Required]
[MaxLength(10, ErrorMessage = "Barkod en fazla 10 karakter olabilir.")]
[RegularExpression(@"^\d{1,10}$", ErrorMessage = "Barkod yalnızca sayılardan oluşmalıdır.")]
public string Barkod { get; set; }

[Required]
[MaxLength(10, ErrorMessage = "ItemId en fazla 10 karakter olabilir.")]
[RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "ItemId yalnızca büyük harf ve rakamlardan oluşmalıdır.")]
public string ItemId { get; set; }
```

## 🎯 Özellikler

* ✅ **REST API** - HTTP protokolü ile modern API
* ✅ **CRUD İşlemleri** - Tam ürün yönetimi
* ✅ **Filtreleme** - Query parametreleri ile gelişmiş arama
* ✅ **Validasyon** - Model seviyesinde veri doğrulama
* ✅ **PATCH** - Kısmi güncelleme desteği
* ✅ **PostgreSQL** - Modern veritabanı
* ✅ **Swagger** - Otomatik API dokümantasyonu
* ✅ **Entity Framework Core** - Modern ORM
* ✅ **.NET 8** - En güncel framework

## 🔄 SOAP vs REST Karşılaştırması

| Özellik          | REST API (Bu Proje)           | SOAP API ([ProductSoapService](https://github.com/aliayass/ProductSoapService)) |
| ----------------- | ------------------------------ | -------------------------------------------------------------------------------- |
| **Protokol**     | HTTP                          | SOAP/XML                                                                         |
| **Veri Formatı** | JSON                          | XML                                                                              |
| **Performans**   | Daha hızlı                    | Daha yavaş                                                                       |
| **Esneklik**     | Yüksek                        | Düşük                                                                           |
| **Öğrenme**      | Kolay                         | Karmaşık                                                                         |
| **Test**         | Postman ile kolay             | SOAP UI gerekli                                                                  |

## 🚨 Sorun Giderme

### SSL Sertifikası Sorunu

```bash
dotnet dev-certs https --trust
```

### Migration Sorunu

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Port Sorunu

`Properties/launchSettings.json` dosyasında port ayarlarını kontrol edin.

## 📚 Öğrenme Kaynakları

### REST API Temelleri
- [REST API Tutorial](https://restfulapi.net/)
- [HTTP Methods](https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods)
- [HTTP Status Codes](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status)

### Postman Öğrenme
- [Postman Learning Center](https://learning.postman.com/)
- [API Testing with Postman](https://www.postman.com/collection/)

### .NET 8 ve Entity Framework
- [.NET 8 Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)

## 👥 Katkıda Bulunma

1. Fork edin
2. Feature branch oluşturun (`git checkout -b feature/AmazingFeature`)
3. Commit edin (`git commit -m 'Add some AmazingFeature'`)
4. Push edin (`git push origin feature/AmazingFeature`)
5. Pull Request oluşturun


---

⭐ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!

## İletişim

- **GitHub:** [@aliayass](https://github.com/aliayass)
- **SOAP Versiyonu:** [ProductSoapService](https://github.com/aliayass/ProductSoapService)
