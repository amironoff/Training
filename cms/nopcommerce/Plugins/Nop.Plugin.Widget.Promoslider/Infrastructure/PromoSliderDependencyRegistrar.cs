using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Widget.PromoSliderPlugin.Data;
using Nop.Plugin.Widget.PromoSliderPlugin.Domain;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widget.PromoSliderPlugin.Infrastructure
{
    public class PromoSliderDependencyRegistrar : IDependencyRegistrar
    {
        private static readonly string contextName = "nop_object_context_promo_slider";

        public int Order
        {
            get { return 1; }
        }

        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {

            //data context
            this.RegisterPluginDataContext<PromoSliderContext>(builder, contextName);

            //override required repository with our custom context
            builder.RegisterType<EfRepository<PromoSlider>>()
                .As<IRepository<PromoSlider>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(contextName))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<PromoImage>>()
                .As<IRepository<PromoImage>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(contextName))
                .InstancePerLifetimeScope();
        }

     
    }
}
