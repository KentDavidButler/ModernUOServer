using ModernUO.Serialization;
using Server.Items;

namespace Server.Mobiles;

[SerializationGenerator(0, false)]
public partial class FieldMiner : BaseCreature
{
    [Constructible]
    public FieldMiner() : base(AIType.AI_Animal, FightMode.None)
    {
        InitStats(100, 100, 25);
        SetSkill(SkillName.Mining, 65.0, 88.0);
        SetCanMine(true);

        SetSpeed(0.2, 0.4);
        SpeechHue = Utility.RandomDyedHue();
        Title = "the miner";
        Hue = Race.Human.RandomSkinHue();

        if (Female = Utility.RandomBool())
        {
            Body = 0x191;
            Name = NameList.RandomName("female");
        }
        else
        {
            Body = 0x190;
            Name = NameList.RandomName("male");
        }

        AddItem(new FancyShirt(0x3E4));
        AddItem(new LongPants(0x192));
        AddItem(new Pickaxe());
        AddItem(new ThighBoots(0x283));

        Utility.AssignRandomHair(this);

        var pack = new Backpack
        {
            Movable = false
        };

        pack.DropItem(new Gold(1, 25));
        AddItem(pack);
    }

    public override bool ClickTitle => false;
}
