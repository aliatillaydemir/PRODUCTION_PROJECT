using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ADO_Project
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas(); //ASP.NET MVC'de "Areas" (bölgeler) kullanılarak uygulamanın farklı bölümleri mantıksal olarak ayrılabilir. uygulamadaki tüm tanımlı bölgeleri (Areas) kaydeder.
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters); //MVC'de kullanılan global filtreleri kaydeder. Global filtreler, uygulamanın tümünde geçerli olan aksiyon filtreleridir (örneğin, hata yönetimi, yetkilendirme vb.)
            GlobalConfiguration.Configure(WebApiConfig.Register); //Web API rotalarını ve ilgili yapılandırmaları kaydeder. WebApiConfig.Register metodu, Web API'nin nasıl çalışacağını belirleyen rota ve yapılandırma ayarlarını içerir.
                                                                  //Web API'nin MVC rotalarıyla çakışmasını önlemek için bu metodun çağrılması gerekir. Bu çağrı, MVC rotalarından önce yapılmalıdır ki Web API rotaları doğru şekilde işlenebilsin. Eğer bu satır MVC rotalarından sonra gelirse, MVC rotaları Web API isteklerini yakalayabilir ve doğru rotaya yönlendirme yapılmaz.

            // JSON formatını varsayılan yapma
            var config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            RouteConfig.RegisterRoutes(RouteTable.Routes); //Bu metod, MVC rotalarını kaydeder. MVC'deki rotalar, URL'lerin belirli controller ve action'lara nasıl yönlendirileceğini belirler.
            BundleConfig.RegisterBundles(BundleTable.Bundles); //Bu metod, JavaScript ve CSS dosyalarını paketlemek için kullanılan Bundle yapılandırmasını kaydeder. Paketleme, istemci tarafı varlıklarının yükleme sürelerini azaltmak için kullanılır.
        }
    }
}
