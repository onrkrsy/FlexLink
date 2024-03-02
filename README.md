# FlexLink

FlexLink, dinamik URL yönlendirme ve link kısaltma ihtiyaçları için tasarlanmış bir ASP.NET Core uygulamasıdır. Bu uygulama, çok uzun URL'leri kısaltmak, e-posta veya kısa mesaj gibi iletişim kanallarında kullanmak ve belgeler içinde referans olarak eklemek üzere esnek bir altyapı sağlar. Yönlendirmeler, sabit linkler veya uygulama mağazaları gibi özelleştirilebilir hedeflere yapılabilmektedir. FlexLink, sonradan yapılandırılabilir URL yönlendirmeleri sayesinde, linkler üzerinde tam kontrol ve yönetim olanağı sunar.

Özellikler
----------

*   **Dinamik URL Yönlendirme**: Kullanıcıları önceden tanımlanmış hedeflere yönlendirir.
*   **Link Kısaltma**: Uzun URL'leri kısaltarak daha yönetilebilir ve estetik linkler oluşturur.
*   **Esnek Yönlendirme Kuralları**: Kullanıcı cihaz türüne göre farklı hedeflere yönlendirme yapabilir.
*   **Kolay Yapılandırma ve Güncelleme**: `appsettings.json` üzerinden yönlendirme kurallarını kolayca ekleyebilir ve güncelleyebilirsiniz.
*   **404 Yönetimi**: Tanımlanmamış yollara erişimde kullanıcıya anlamlı geri bildirim sağlar.

Kurulum
-------

Projeyi yerel makinenize klonlamak için:

```bash 

`git clone https://github.com/onrkrsy/FlexLink.git cd FlexLink`
```
Uygulamayı çalıştırmak için:

``` bash 
`dotnet run`
```
Yapılandırma
------------

`appsettings.json` dosyası, yönlendirme kurallarınızı tanımlamak için kullanılır. Yeni bir yönlendirme kuralı eklemek için, bu dosyaya uygun yapıyı ekleyin:

```json 
"RedirectionSettings": {
  "Redirects": {
    "example": {
      "type": "normal",
      "url": "https://www.example.com"
    },
    "mobileapp": {
      "type": "multiple",
      "urls": {
        "Android": "https://play.google.com/store/apps/details?id=com.yourapp",
        "iOS": "https://apps.apple.com/app/your-app-id",
        "Huawei": "https://appgallery.huawei.com/#/app/your-app-id",
        "Others": "https://www.yourwebsite.com/"
      }
    },
    // Diğer yönlendirmeler...
  }
}
```
Kullanım
--------

Uygulama çalışırken, `/example` veya `/mobileapp` gibi tanımlı endpoint'lere yapılan istekler, ilgili yönlendirmelerle işlenir. Tanımlanmamış bir yol için istek yapıldığında, kullanıcılar 404 Not Found yanıtı alır.


Örnek URL Yönlendirmeleri
-------------------------

1.  **Normal Yönlendirme**:
    
    *   **Amaç**: Bir kampanya için oluşturulan uzun bir URL'yi kısaltmak ve bu kısa link üzerinden ilgili kampanya sayfasına yönlendirmek.
    *   **Giriş URL'si**: `https://localhost:7288/kampanya`
    *   **Hedef URL**: `https://www.example.com/uzun-ve-karmaşık-kampanya-urlsi?utm_source=xyz`
    *   `appsettings.json` yapılandırması:
        
        ```json 
         "kampanya": {   "type": "normal",   "url": "https://www.example.com/uzun-ve-karmaşık-kampanya-urlsi?utm_source=xyz" } ```

        
2.  **Mobil Uygulama Yönlendirmesi**:
    
    *   **Amaç**: Kullanıcıları cihazlarına uygun uygulama mağazalarına yönlendirmek.
    *   **Giriş URL'si**: `https://localhost:7288/mobileapp`
    *   **Hedef URL'ler**:
        *   Android için: Google Play Store
        *   iOS için: Apple App Store
        *   Diğer: Uygulamanın web sayfası
    *   `appsettings.json` yapılandırması:
        
 ```json 
  "mobileapp": {
  "type": "multiple",
  "urls": {
    "Android": "https://play.google.com/store/apps/details?id=com.yourapp",
        "iOS": "https://apps.apple.com/app/your-app-id",
        "Huawei": "https://appgallery.huawei.com/#/app/your-app-id",
        "Others": "https://www.yourwebsite.com/"
  }
}

```
 
Yönlendirme Nasıl Çalışır?
--------------------------

Kullanıcı, belirli bir giriş URL'sine eriştiğinde, FlexLink uygulaması `appsettings.json` dosyasında tanımlanan yönlendirme kurallarına göre kullanıcıyı ilgili hedef URL'ye yönlendirir. Bu işlem, kullanıcının cihaz türünü (mobil uygulama yönlendirmesi için) ve isteğin giriş yolunu (path) dikkate alarak gerçekleştirilir.

*   Bir kullanıcı `https://localhost:7288/kampanya` adresine gittiğinde, doğrudan `https://www.example.com/uzun-ve-karmaşık-kampanya-urlsi?utm_source=xyz` adresine yönlendirilir.
*   Eğer bir kullanıcı `https://localhost:7288/mobileapp` adresine Android bir cihazdan erişirse, Google Play Store'daki uygulama sayfasına yönlendirilir. iOS cihazları için benzer şekilde Apple App Store'a, Huawei cihazları için Huawei AppGallery'ye yönlendirme yapılır.

Bu yönlendirmeler, kullanıcı deneyimini iyileştirmenize ve pazarlama kampanyalarınızın etkinliğini artırmanıza yardımcı olabilir. Ayrıca, FlexLink üzerinden yapılan yönlendirmelerin yönetimi ve güncellenmesi kolaydır, bu sayede linklerinizi ve yönlendirme kurallarınızı ihtiyaçlarınıza göre hızla değiştirebilirsiniz. 

Lisans
------

Bu proje MIT Lisansı altında lisanslanmıştır.
