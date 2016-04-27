using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Widgets.PromoSlider.Domain;

namespace Nop.Plugin.Widgets.PromoSlider.Data
{
    public class PromoImageMap : EntityTypeConfiguration<PromoImage>
    {
        public PromoImageMap()
        {
            ToTable("PromoSlider_PromoImages");

            //Map the primary key
            HasKey(m => m.PromoImageId);

            Property(m => m.PromoSliderId);
            Property(m => m.Caption);
            Property(m => m.DisplayOrder);
            Property(m => m.Url);

            this.HasRequired(i => i.PromoSlider)
                .WithMany(s => s.Images)
                .HasForeignKey(i => i.PromoSliderId);
        }
    }
}
