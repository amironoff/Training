using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nop.Plugin.Widget.PromoSliderPlugin.Domain
{
    public class PromoImage : BaseEntity
    {
        public virtual int PromoImageId { get; set; }
        public virtual int PromoSliderId { get; set; }
        public virtual string Caption { get; set; }
        public virtual string Url { get; set; }
        public virtual string FilePath { get; set; }
        public virtual int DisplayOrder { get; set; }

        public PromoSlider PromoSlider { get; set; }
    }
}
