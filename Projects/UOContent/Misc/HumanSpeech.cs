using Internal;using System;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server.Mobiles;
using Server.Logging;

namespace Server.Misc
{
    /// <summary>
    /// Handles random ambient speech for human NPCs via BaseCreature.OnThink().
    /// Configure with Settings.cs: "npc.humanSpeechEnabled" and "npc.humanSpeechIntervalMin/Max".
    /// </summary>
    public static class HumanSpeech
    {
        private static readonly ILogger logger = LogFactory.GetLogger(typeof(Mobile));

        public static bool Enabled => ServerConfiguration.GetSetting("npc.humanSpeechEnabled", true);

        public static double IntervalMinSeconds => ServerConfiguration.GetSetting("npc.humanSpeechIntervalMin", 30.0);

        public static double IntervalMaxSeconds => ServerConfiguration.GetSetting("npc.humanSpeechIntervalMax", 120.0);

        // Phrases Based on Jobs

        // Common ambient phrases for guards and town citizens
        private static readonly string[] _guardPhrases =
        {
            "Stay in line.",
            "Move along.",
            "Keep the peace.",
            "Any trouble, let me know.",
            "The people of Britannia depends on us.",
            "Stay alert.",
            "I've got this post.",
            "Report to the castle if you see anything suspicious.",
            "Hail Britannia.",
            "Keep the streets safe.",
            "Watch for thieves.",
            "All seems quiet today.",
            "Duty calls.",
            "Stand tall.",
            "The king's peace is our peace."
        };

        private static readonly string[] _vendorPhrases =
        {
            "Take a look.",
            "Take a look at these goods!",
            "Oh, a bit of this and a bit of that.",
            "Trinkets, odds and ends, that sort of thing.",
            "Some may call this junk. Me, I call them treasures.",
            "Choose wisely.",
            "Do not loiter.",
            "Everything has a price.",
            "I have one of a kind items.",
            "What do you seek?",
            "Your gold is welcome here.",
            "Buy? Trade?",
            "You find many things here!",
            "Take time if you need.",
            "Good prices here!",
            "All prices here are reasonable.",
            "Please browse my wares.",
            "Sale here.",
            "Nothing wrong with taking a look.",
            "Something catch your eye?",
            "What are you looking to sell?",
            "I'd buy that for a coin!",
            "I've got the best prices around!",
            "You got the coin, I've got the goods.",
            "Your coin goes farther here.",
            "Did you see our ad posted at the bank?",
            "Don't forget to ask about today's sales.",
            "I'm not cut out to be a salesman."
        };

        private static readonly string[] _magicPhrases =
        {
            "Got a pretty full stock of potions and alchemy reagents.",
            "Ah, so you're an alchemist, then?",
            "Spells and incantations for those with the talent to cast them.",
            "So, you wish to master the arcane arts...",
            "Hmph. I had you figured for a mage. I think you'll appreciate this..."
        };

        private static readonly string[] _barkeepPhrases =
        {
            "Drink for the thirsty, food for the hungry.",
            "Let's sate that appetite, hmm?",
            "What do we have you say? It depends! Are you thirsty, hungry, both?",
            "Warm beer ain't as bad as people say.",
            "The food ain't great, but it fills a hole.",
            "You can choose any beer you'd like, as long it's the house ale.",
            "We make the ale in the back, in a tub. It's how it gets its taste.",
            "Don't ask what that tangy flavor is.",
            "Today's special is pottage with bread.",
            "Pottage is the special for any day ending in 'Y'.",
            "Contrary to popular belief, a little mold is good for you.",
            "Ale, we have ale and nothing else.",
            "Ohhh, look at fancy pants over here wanting a wine!",
            "Would you like a little cheese with that wine?",
            "Stop running! This is a bar not a barn!",
            "Careful the locals get rowdy, once they get a few in them.",
            "I don't judge the alcoholics, I provide a safe space for them.",
            "Have another round! Let the horse drive you home!",
            "You puke, you're out.",
            "You get sick in here, you'll be cleaning it up!",
            "Welcome to Britannia's most okay bar.",
            "If you wanted good food, go down the street!",
            "Best prices for a warm meal.",
            "Best prices for a half-eating meal.",
            "You can have sloppy seconds if you're short on coin.",
            "Don't listen to the locals, we ain't serving cat!",
            "If you find a cat hair in your food, your meal is free.",
            "Lizard men are a delicacy around here.",
            "Have you ever tried Lizard man tail?",
            "Be warned, I've seen trolls turn down our food."
        };

        private static readonly string[] _innkeeperPhrases =
        {
            "Looking for a place to rest? I have rooms available.",
            "No funny business in my inn, or you'll be out on the street.",
            "No vacancy, try the next place",
            "Go away, we're full",
            "Would you like a room? Cause we ain't got any available.",
            "We got a free room, if you'd like to stay.",
            "This place beats sleeping on the street, but not by much.",
            "How do you like sleeping with rats?",
            "You can share a room if you're short on coin.",
            "No horses inside of the rooms!",
            "For extra we can provide a warm bath.",
            "For extra we can provide a warm bath, and for a little less you let me watch",
            "Sorry, it's not a bed and breakfast.",
            "The continental breakfast will run you extra",
            "Don't mind the light coming from the hole in the wall.",
            "If the rooms are too pricy, you can sleep with the horses",
            "If the rooms are too pricy, you can sleep with the horses, but don't expect privacy.",
            "The rooms look empty, but I assure you they are reserved",
            "It's quite in the Inn during the middle of the day",
            "We are not responsible for missing items",
            "Be sure to lock your belongings up while you are out",
            "I'll charge you extra if the guards come looking for you",
            "This is an Inn! You can't do that here",
            "This is an Inn! Not a fighting ring!",
            "Please leave your mounts outside",
            "You look shady, I don't know if I'll be renting a room to you",
            "It's normal to have ghosts visit you at night.",
            "Sheets are always changed, every fifth day.",
            "The beds are clean, but the sheets are not.",
            "Waking up with red dots is perfectly normal!",
            "Feeling rested?",
            "Have a good night's sleep?",
            "Did you hear a bump in the night?",
            "Careful the bed bugs bite."
        };

        private static readonly string[] _smithPhrases =
        {
            "The finest weapons and armor.",
            "Hmm... Blades, helmets. Pretty much anything to suit your needs.",
            "Looking to protect yourself, or deal some damage?",
            "Blades so sharp you would think it's magical",
            "Our weapons come with plus one attack power!",
            "You need to beat something other than your meat? Check out our mace selection!",
            "Beat it! With a mace, a hammer, or a quarterstaff!",
            "Fine selection of weapons at the ready.",
            "If I don't have a weapon you are looking for, I'll make it!",
            "You don't need magic when you got an axe!",
            "You looking for two handed weapons or one handed?",
            "you looking to cut, beat, or stab? We've got it all!",
            "I made each one of these weapons with my bear hands!",
            "I got several ways to protect yourself",
            "What kind of protection you looking for?",
            "Suiting up like a fortress? I've got the plate armor for you!",
            "Looking for flexibility, try padded armor.",
            "Even wizards need armor around these parts.",
            "You won't live beyond these walls without protection",
            "It's dangerous to go alone! Buy a weapon here.",
            "People underestimate the protection of a shield",
            "With that shield, you look like a super hero!",
            "With that hammer, you look like a super hero!",
            "With my armor, people will be calling you Armor Man!",
            "Watch out for the forge, it's hot.",
            "If you can't take the heat, stay out of the forge!",
            "Nothing like a good hammering to keep you in shape",
            "Ting, ting, ting goes the forge!",
            "You bring the ingots, I can make what you want."
        };

        private static readonly string[] _minerPhrases =
        {
            "I'm not allowed to drink, I'm a miner.",
            "Diggin' in des mines is all I got.",
            "I'm here to mine. So unless you want to dig, get out.",
            "Good ore brings strength to our people.",
            "The mines are hard, but rewards those who rise to the challenge.",
            "Rock on!",
            "Rock and Stone!",
            "Where's the damn mine cart?",
            "Where's the mule?",
            "There's no way I can carry all of this.",
            "I don't mind being covered in dirt.",
            "What do you think they'll make from this ore?",
            "Sometimes I'll pocket a gem or two.",
            "Mining is an honest living.",
            "God dammit there's a pebble in my boot!",
            "The smith takes all the credit, but we do the hard work.",
            "Smiths never have to worry about rock slides and cave-ins! Lazy buggers.",
            "I laugh when I hear a smith complaining about swinging a hammer.",
            "Smiths don't even smelt their own ore! Lazy buggers",
            "Nothin like digging a hole!",
            "I keep digging, I'm going to hit The Lost Lands!",
            "You gotta watch out for the bugs! Terathans are no joke.",
            "You see a bug! Smash it!",
            "Spider people? You mean terathans!? They're only good for killin.",
            "I hate spiders, and I hate people. I especially hate terathans!",
            "Nothing smells better than fresh cut dirt",
            "Is there anything better than mining?",
            "Which do you prefer, shovel or pickaxe",
            "Why would anyone pick a shovel over a pickaxe?",
            "Why would anyone use a pickaxe over a shovel?",
            "All the greatest miners use pickaxes!",
            "Are you team shovel, or team pickaxe?",
            "Who uses a pack llama?!",
            "Pack horses over pack llamas.",
            "We aren't dwarves! It just happens that miners are short people",
            "Being small gets you ahead in this business",
            "Some of the best ore can be found in dangerous places.",
            "If you looking for rare ore, venture far from town.",
            "Valorite is the rarest ore around.",
            "I've been mining all my life and I've never seen Valorite.",
            "Valorite is only a legend! It's not real."
        };
        
        private static readonly string[] _healerPhrases =
        {
            "Blessings upon your family",
            "May the light of the gods shine upon you",
            "May the gods protect you",
            "May the gods watch over you",
            "Good fortune",
            "Do you require medication?",
            "Each day is a blessing",
            "May the gods grant you a long life",
            "May the light embrace you",
            "May your days be long, and your hardships few.",
            "Blessing upon your family.",
            "How can I help?",
            "Light's blessing to you"
        };

        //TODO:
        // Add Fisherman phrases
        // Add Bard Phrases
        // Add Butcher Phrases
        // Add Carpenter Phrases
        // Add Cobbler Phrases
        // Add Cook Phrases
        // Add Farmer Phrases
        // Add Jewler Phrases
        // Add Provisioner Phrases
        // Add Scribe Phrases
        // Add Tailor Phrases
        // Add Tinker Phrases
        // Add Theif Phrases
        // Add Weaponsmith Phrases
        // Add Lumberjack Phrases
        // Add Noble Phrases

        // Generic Phrases

        private static readonly string[] _genericPhrases =
        {
            "Hello there.",
            "Good day.",
            "How are you?",
            "Nice to meet you.",
            "Take care.",
            "Farewell.",
            "The weather is pleasant.",
            "A busy day indeed.",
            "Greetings, traveler.",
            "Welcome to our town.",
            "Good day to you.",
            "Lovely weather we're having.",
            "Have you heard the latest news?",
            "The marketplace is bustling today.",
            "I hope the harvest is good this year.",
            "Pretty day, isn't it?",
            "Mind the mud.",
            "The roads are rough past town.",
            "I'm off to market.",
            "Busy day ahead.",
            "The tavern is the place to be tonight.",
            "Have you seen the new arrivals?",
            "Strange things happen in the old ruins.",
            "I must remember to buy more bread.",
            "The children are playing in the square.",
            "A fine town, Britannia.",
            "The locals are friendly enough.",
            "Stay safe out there.",
            "Blessings of Britannia upon you.",
            "I never knew Lord British's first name is Cantabrigian.",
            "I'm just resting for a moment.",
            "Need a break from standing.",
            "Stretching my legs.",
            "Taking a short rest.",
            "A moment of peace."
        };

        private static readonly string[] _negativePhrases =
        {
            "Something you need, you miserable wretch",
            "Trust no one.",
            "I can't remember the last time I had, well, anything.",
            "What's the point?",
            "Why go on?",
            "I don't dream anymore",
            "Looks like things are getting worse, not better.",
            "I can't remember the last time I cleaned myself",
            "I'm gonna be sick.",
            "This is bad",
            "I ate something bad...",
            "God I'm hungry",
        };

        private static readonly string[] _drunkPhrases =
        {
            "My favorite drinking buddy. Let's get some mead.",
            "Interest ya'n a pint?",
            "I don't feel anything anymore",
            "*Burp*",
            "It's normal not to remember the prior month",
            "Mead, Mead, Mead, I love mead.",
            "99 bottles of mead on the wall, 99 bottles of mead!",
            "To Lord British! May he rot in that castle!",
            "It's still only a drink if it's the entire keg.",
            "HEEEYYYY, You look like someone who needs a drinking buddy!",
            "I'll drink with you any place, anywhere!",
            "Don't threaten me with a good time!",
            "I'm on a liquid diet.",
            "dunks won't inherit the kingdom of God, eh? Keep it!",
            "Drinkin' is cheaper than eatin'",
            "After a hard day, everyone could use a drinking buddy.",
            "You only get hungover if you stop drinking.",
            "Work, drink, sleep, and repeat...",
            "I could use a refill!",
            "My cup is a bit empty, could I get some MORE!"
        };

        private static readonly string[] _aggressivePhrases =
        {
            "Get out of my way!",
            "Move along, or else!",
            "I don't have time for this.",
            "You better watch yourself.",
            "Don't make me angry.",
            "I won't warn you again.",
            "Step aside, or face the consequences.",
            "I don't take kindly to threats.",
            "You'd best leave before things get ugly.",
            "I suggest you leave now.",
            "I should bash your face in!",
            "I hope monsters come and eat you!",
            "Something you need, you miserable wretch!",
            "What are you starin' at?",
            "Gods strike you down!",
            "What are you babbling about?",
            "Run away pest!",
            "Not very intelligent, are you?",
            "Ah, you have a death wish.",
            "You behave like a child!",
            "Your mother was a troll!",
            "Watch yer back!",
            "Y'all are stupid!",
            "How's that? Owned, suckah!",
            "Get the hell out of here!",
            "Something must be wrong with me, I almost understood that.",
            "Have you ever had an original thought?",
            "I'll put it on your tombstone."
        };

        public static bool TryRandomSpeech(BaseCreature creature)
        {
            if (!Enabled
                || creature.Deleted
                || creature.Player
                || creature.Controlled
                || creature.Summoned
                || creature.Body.IsHuman == false)
            {
                return false;
            }

            // Check if we should speak based on random interval
            var nextSpeech = creature.GetNextHumanSpeechTime();
            var now = Core.TickCount;

            if (now - nextSpeech < 0)
            {
                return false;
            }

            // Schedule next speech at a random future interval
            var intervalMs = TimeSpan.FromSeconds(
                IntervalMinSeconds + Utility.RandomDouble() * (IntervalMaxSeconds - IntervalMinSeconds)
            ).TotalMilliseconds;
            creature.SetNextHumanSpeechTime(now + (int)intervalMs);

            // Choose phrase based on body type
            var phrase = SelectPhrase(creature);
            if (phrase == null)
            {
                return false;
            }

            // Speak the phrase
            creature.Say(phrase);

            return true;
        }

        private static string SelectPhrase(BaseCreature creature)
        {
            var title = creature.Title;
            var isVendor = creature is BaseVendor vendor;
            var roll = Utility.RandomDouble();

            if (title == "the guard")
            {
                // Guards speak guard phrases more often
                if (roll < 0.15)
                {
                    return _aggressivePhrases.RandomElement();
                }
                if (roll < 0.55)
                {
                    return _guardPhrases.RandomElement();
                }
                if (roll < 0.95)
                {
                    return _genericPhrases.RandomElement();
                }
                return "";
            }

            if (creature is BaseVendor)
            {
                 if (roll < 0.6)
                    {
                        if (title == "the alchemist" || title == "the mage"){
                            return _magicPhrases.RandomElement();
                        }
                        if (title == "the barkeeper" || title == "the waiter")
                        {
                            return _barkeepPhrases.RandomElement();
                        }
                        if (creature.GetType() == typeof(InnKeeper))
                        {
                            return _innkeeperPhrases.RandomElement();
                        }
                         if (creature.GetType() == typeof(Blacksmith))
                        {
                            return _smithPhrases.RandomElement();
                        }
                        if (title == "the miner")
                        {
                            return _minerPhrases.RandomElement();
                        }
                        if (creature is BaseHealer)
                        {
                            return _healerPhrases.RandomElement();
                        }
                    }
                    if (roll < 0.85)
                    {
                        return _vendorPhrases.RandomElement();
                    }

                return _genericPhrases.RandomElement();
            }

            if (title == "the miner")
            {
                if (roll < 0.1)
                {
                    return _aggressivePhrases.RandomElement();
                }
                if (roll < 0.3)
                {
                    return _genericPhrases.RandomElement();
                }
                if (roll < 0.5)
                {
                    return _negativePhrases.RandomElement();
                }
                return _minerPhrases.RandomElement();
            }

            if (title == "the drunk")
            {
                if (roll < 0.1)
                {
                    return _aggressivePhrases.RandomElement();
                }
                if (roll < 0.3)
                {
                    return _genericPhrases.RandomElement();
                }
                if (roll < 0.5)
                {
                    return _negativePhrases.RandomElement();
                }
                return _drunkPhrases.RandomElement();
            }


            if (roll < 0.1)
            {
                return _aggressivePhrases.RandomElement();
            }
            if (roll < 0.2)
            {
                return _drunkPhrases.RandomElement();
            }
            if (roll < 0.3)
            {
                return _negativePhrases.RandomElement();
            }
            if (roll < 0.60)
            {
                return _genericPhrases.RandomElement();
            }
        
            return "";
        }
    }
}



