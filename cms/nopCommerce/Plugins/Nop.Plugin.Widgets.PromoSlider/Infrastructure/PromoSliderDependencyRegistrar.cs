using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Widgets.PromoSlider.Data;
using Nop.Plugin.Widgets.PromoSlider.Domain;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.PromoSlider.Infrastructure
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
            builder.RegisterType<EfRepository<Domain.PromoSlider>>()
                .As<IRepository<Domain.PromoSlider>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(contextName))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<PromoImage>>()
                .As<IRepository<PromoImage>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(contextName))
                .InstancePerLifetimeScope();
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            Register(builder, typeFinder);
        }
    }
}
