# 🚗 Car Segment Predictor

Müşteri verilerine dayalı araba segment tahmini yapan C# uygulaması. ML.NET kullanarak müşterilerin demografik bilgilerinden hangi araba segmentini tercih edeceklerini tahmin eder.

![Car Segment Predictor Application](https://i.imgur.com/6UJoGLK.png)

## 🛠️ Teknolojiler

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![ML.NET](https://img.shields.io/badge/ML.NET-512BD4?style=for-the-badge&logo=.net&logoColor=white)
![Windows Forms](https://img.shields.io/badge/Windows%20Forms-0078D4?style=for-the-badge&logo=windows&logoColor=white)
![.NET Framework](https://img.shields.io/badge/.NET%20Framework-512BD4?style=for-the-badge&logo=.net&logoColor=white)

## ⚡ Özellikler

- 🤖 **Makine Öğrenmesi**: ML.NET ile güçlü tahmin modeli
- 🖥️ **Kullanıcı Dostu Arayüz**: Windows Forms ile basit kullanım
- 📊 **CSV Veri Desteği**: Kendi verilerinizle model eğitimi
- ⚡ **Gerçek Zamanlı Tahmin**: Anında sonuç alma
- 📈 **Performans Metrikleri**: Model doğruluğu görüntüleme

## 📥 Kurulum

```bash
# Projeyi klonlayın
git clone https://github.com/AsliAsln/CarSegmentPredict

# Visual Studio ile açın
# NuGet paketleri otomatik yüklenecek

# Veri dosyasını hazırlayın (CSV formatında)
# Uygulamayı derleyip çalıştırın
```

## 📊 Veri Formatı

CSV dosyanız şu sütunları içermeli:

| Sütun | Tip | Örnek Değerler |
|-------|-----|----------------|
| ID | Integer | 1, 2, 3... |
| Gender | String | Male, Female |
| Married | String | Yes, No |
| Age | Integer | 18-80 |
| Graduated | String | Yes, No |
| Profession | String | Healthcare, Engineer, Lawyer |
| WorkExperience | Integer | 0-50 |
| SpendingScore | String | Low, Average, High |
| FamilySize | Integer | 1-10 |
| Category | String | Customer category |
| Segmentation | String | A, B, C, D |

## 🚀 Kullanım

1. **Model Eğitimi**: "Train Model" butonuna tıklayarak modeli eğitin
2. **Veri Girişi**: Müşteri bilgilerini forma girin
3. **Tahmin**: "Predict Segment" ile segment tahminini alın
4. **Sonuç**: Tahmin edilen segment ve güven skorunu görün

## 🎯 Segment Kategorileri

- **Segment A**: Premium müşteriler (Lüks araçlar)
- **Segment B**: Orta-üst segment (Konfor odaklı)
- **Segment C**: Orta segment (Dengeli seçim)
- **Segment D**: Ekonomik segment (Bütçe dostu)

## 📈 Model Performansı

Uygulama SDCA (Stochastic Dual Coordinate Ascent) algoritması kullanır ve aşağıdaki metrikleri sağlar:
- Model doğruluğu
- Confusion matrix
- Precision ve Recall değerleri

## 🔧 Gereksinimler

- Visual Studio 2019 veya üzeri
- .NET Framework 4.7.2+
- ML.NET NuGet paketi

## 📝 Notlar

Bu proje, makine öğrenmesi ve C# becerilerini göstermek için geliştirilmiştir. Otomotiv sektöründe müşteri segmentasyonu için pratik bir örnek sunar.


---

⭐ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!
