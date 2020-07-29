using System;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace Sharper
{
    /// <summary>
    /// Renders a progress bar hud in the top left corner of the screen
    /// </summary>
    public class Shaper : ModSystem
    {
        public override bool ShouldLoad(EnumAppSide side)
        {
            return true;
        }

        public override void Start(ICoreAPI api)
        {
            api.RegisterItemClass("Whetstone", typeof(Whetstone));
            api.RegisterItemClass("ItemSpear", typeof(Spear));
        }
    }

    public class Whetstone : Item
    {
    }

    public class Spear : Item
    {
        public override void OnHeldInteractStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handling)
        {
            if (byEntity.LeftHandItemSlot.Itemstack != null)
            {
                Item leftHandItem = byEntity.LeftHandItemSlot.Itemstack.Item;
                if (leftHandItem.Code.ToString() == "sharper:whetstone")
                {
                    if (this.api.Side == EnumAppSide.Client)
                    {
                        ((ICoreClientAPI)this.api).StartTrack(new AssetLocation("sharper", "music/whetstone"), 1, EnumSoundType.Sound);
                    }
                    this.AttackPower += 0.1f;
                    this.DamageItem(byEntity.World, byEntity, slot, 10);
                    leftHandItem.DamageItem(byEntity.World, byEntity, byEntity.LeftHandItemSlot, 10);
                    return;
                }
            }
            base.OnHeldInteractStart(slot, byEntity, blockSel, entitySel, firstEvent, ref handling);
        }
    }
}
