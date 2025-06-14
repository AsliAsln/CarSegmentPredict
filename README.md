<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Car Segment Predictor</title>
    <style>
        body {
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
            line-height: 1.6;
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
            color: #333;
            background: #f8f9fa;
        }
        
        .container {
            background: white;
            padding: 40px;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }
        
        h1 {
            color: #2c3e50;
            text-align: center;
            margin-bottom: 30px;
            border-bottom: 2px solid #3498db;
            padding-bottom: 10px;
        }
        
        h2 {
            color: #34495e;
            margin-top: 30px;
            margin-bottom: 15px;
        }
        
        .tech-stack {
            display: flex;
            flex-wrap: wrap;
            gap: 8px;
            margin: 15px 0;
        }
        
        .tech-badge {
            background: #3498db;
            color: white;
            padding: 4px 12px;
            border-radius: 12px;
            font-size: 0.85em;
        }
        
        .screenshot {
            width: 100%;
            max-width: 600px;
            height: auto;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
            margin: 20px 0;
            display: block;
            margin-left: auto;
            margin-right: auto;
        }
        
        code {
            background: #f4f4f4;
            padding: 2px 6px;
            border-radius: 4px;
            font-family: 'Consolas', monospace;
        }
        
        ul, ol {
            padding-left: 20px;
        }
        
        li {
            margin: 8px 0;
        }
        
        .highlight {
            background: #e8f4f8;
            padding: 15px;
            border-radius: 6px;
            border-left: 4px solid #3498db;
            margin: 15px 0;
        }
    </style>
</head>
<body>
    <div class="container">
        <h1>🚗 Car Segment Predictor</h1>
        
        <p>Müşteri verilerine dayalı araba segment tahmini yapan C# uygulaması. ML.NET kullanarak müşterilerin demografik bilgilerinden hangi araba segmentini tercih edeceklerini tahmin eder.</p>
        
        <img src="https://i.imgur.com/6UJoGLK.png" alt="Car Segment Predictor Application" class="screenshot">
        
        <h2>🛠️ Teknolojiler</h2>
        <div class="tech-stack">
            <span class="tech-badge">C#</span>
            <span class="tech-badge">ML.NET</span>
            <span class="tech-badge">Windows Forms</span>
            <span class="tech-badge">.NET Framework</span>
        </div>
        
        <h2>⚡ Özellikler</h2>
        <ul>
            <li>Makine öğrenmesi ile segment tahmini</li>
            <li>Kullanıcı dostu Windows Forms arayüzü</li>
            <li>CSV verisi ile model eğitimi</li>
            <li>Gerçek zamanlı tahmin sonuçları</li>
            <li>Model performans metrikleri</li>
        </ul>
        
        <h2>📥 Kurulum</h2>
        <ol>
            <li>Projeyi klonlayın: <code>git clone https://github.com/AsliAsln/CarSegmentPredict</code></li>
            <li>Visual Studio ile açın</li>
            <li>NuGet paketlerini yükleyin</li>
            <li>Veri dosyasını hazırlayın</li>
            <li>Çalıştırın</li>
        </ol>
        
        <h2>📊 Veri Formatı</h2>
        <div class="highlight">
            <strong>CSV dosyası şu sütunları içermelidir:</strong><br>
            ID, Gender, Married, Age, Graduated, Profession, WorkExperience, SpendingScore, FamilySize, Category, Segmentation
        </div>
        
        <h2>🚀 Kullanım</h2>
        <ol>
            <li><strong>Train Model:</strong> Modeli eğitin</li>
            <li><strong>Müşteri bilgilerini girin:</strong> Yaş, meslek, gelir vb.</li>
            <li><strong>Predict Segment:</strong> Segment tahminini alın</li>
        </ol>
        
        <h2>🎯 Segment Kategorileri</h2>
        <ul>
            <li><strong>A:</strong> Premium segment</li>
            <li><strong>B:</strong> Orta-üst segment</li>
            <li><strong>C:</strong> Orta segment</li>
            <li><strong>D:</strong> Ekonomik segment</li>
        </ul>
        
        <h2>📝 Not</h2>
        <p>Bu proje, makine öğrenmesi ve C# becerilerini göstermek için geliştirilmiştir. Otomotiv sektöründe müşteri segmentasyonu için pratik bir örnek sunar.</p>
    </div>
</body>
</html>