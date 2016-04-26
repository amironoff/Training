using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Plugin.Widget.PromoSliderPlugin.Data;
using Nop.Services.Cms;

namespace Nop.Plugin.Widget.PromoSliderPlugin
{
    public class PromoSliderPlugin : BasePlugin, IWidgetPlugin
    {
        private PromoSliderContext _context = null;

        public PromoSliderPlugin(PromoSliderContext context)
        {
            _context = context;
        }

        public override void Install()
        {
            _context.Install();
            base.Install();
        }

        public override void Uninstall()
        {
            _context.Uninstall();
            base.Uninstall();
        }

        public IList<string> GetWidgetZones()
        {
            return new List<string>();
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "PromoSlider";
            routeValues = new RouteValueDictionary()
            {
                {"Namespaces", "Nop.Plugin.Widget.PromoSlider.Controllers"},
                {"area", null}
            };
        }

        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName,
            out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "PromoSlider";
            routeValues = new RouteValueDictionary()
            {
                {"Namespaces", "Nop.Plugin.Widget.PromoSlider.Controllers"},
                {"area", null},
                {"widgetZone", widgetZone }
            };
        }
    }
}
