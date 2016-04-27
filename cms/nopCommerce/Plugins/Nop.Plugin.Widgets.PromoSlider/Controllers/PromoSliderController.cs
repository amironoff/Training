using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.PromoSlider.Controllers
{
    public class PromoSliderController : BasePluginController
    {
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;

        public PromoSliderController(ISettingService settingService, ILocalizationService localizationService)
        {
            this._settingService = settingService;
            this._localizationService = localizationService;
        }
         
    }
}