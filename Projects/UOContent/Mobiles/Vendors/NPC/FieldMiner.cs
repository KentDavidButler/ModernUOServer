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

        Utility.AssignRandomHair(this, true);
        if (Female = Utility.RandomBool())
        {
            Body = 0x191;
            Name = NameList.RandomName("female");
        }
        else
        {
            Body = 0x190;
            Name = NameList.RandomName("male");
            Utility.AssignRandomFacialHair(this, true);
        }

        AddItem(
            Utility.Random(3) switch
            {
                0 => new FancyShirt(Utility.RandomDyedHue()),
                1 => new Doublet(Utility.RandomDyedHue()),
                _ => new Shirt(Utility.RandomDyedHue()) // 2
                }
        );
        AddItem(new LongPants(Utility.RandomDyedHue()));
        AddItem(new Pickaxe());
        AddItem(new ThighBoots(Utility.RandomDyedHue()));

        var pack = new Backpack
        {
            Movable = false
        };

        pack.DropItem(new Gold(1, 25));
        AddItem(pack);
    }

    public override bool ClickTitle => false;
}
