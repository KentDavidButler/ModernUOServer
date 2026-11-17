using ModernUO.Serialization;
using Server.Items;

namespace Server.Mobiles;

[SerializationGenerator(0, false)]
public partial class Lumberjack : BaseCreature
{
    [Constructible]
    public Lumberjack() : base(AIType.AI_Animal, FightMode.None)
    {
        InitStats(100, 100, 25);
        SetSkill(SkillName.Lumberjacking, 65.0, 88.0);
        SetCanLumberjack(true);

        SetSpeed(0.2, 0.4);
        SpeechHue = Utility.RandomDyedHue();
        Title = "the lumberjack";
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

        AddItem(new FancyShirt(Utility.RandomDyedHue()));
        AddItem(new LongPants(Utility.RandomDyedHue()));
        AddItem(new Hatchet());
        AddItem(new ThighBoots(Utility.RandomDyedHue()));

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
