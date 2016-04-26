using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Widget.PromoSliderPlugin.Domain;

namespace Nop.Plugin.Widget.PromoSliderPlugin.Data
{
    public class PromoSliderMap : EntityTypeConfiguration<PromoSlider>
    {
        public PromoSliderMap()
        {
            ToTable("PromoSlider_PromoSliders");
            HasKey(m => m.PromoSliderId);

            Property(m => m.PromoSliderName);
            Property(m => m.ZoneName);
            Property(m => m.Interval);
            Property(m => m.KeyBoard);
            Property(m => m.PauseOnHover);
            Property(m => m.Wrap);
        }
    }
}
