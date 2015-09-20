using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using SKYPE4COMLib;
using System.IO;
using Twitch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using HtmlAgilityPack;
using System.Net.Http;
using System.Xml.XPath;
using System.Xml;
using System.Diagnostics;

namespace SkypeBot2
{

    public partial class Donger:DongerBattler
    {

        public const int defHp=30 ;
        public const double defDamageMod = 1;
        public const int defAccMod = 85;
        public const int defSkillChanse = 15;
        public const int defSkillUses = 1;
        public const double defArmor = 1;
        public const double defMagicDef = 1;
        public const string defRapeString = "";
        public const bool defRapeble = true;

      
        public int Time
        {
            get
            {
                return hp * 2;
            }
        }

        public Donger systemEnemy;
        public Donger enemy;
        public string dongerName;

        public Action<Donger> skill;


        public int hp = defHp;
        public double damageMod = defDamageMod;
        public int acurityMod = defAccMod;
        public int skillChance = defSkillChanse;
        public int skillUses = defSkillUses;
        public double armor = defArmor;
        public double magicDef = defMagicDef;
        public bool rapeble = defRapeble;
        public string rapeString = defRapeString;
        public Action<Donger> attack = Donger.DeafaultAttack;
        public List<Effect> effects =new List<Effect>();
        public string SkillName
        {
            get
            {
                if (SkillNames.ContainsKey(skill))
                    return SkillNames[skill];
                else
                    return "???";
            }
        }
        public string Info
        {
            get
            {
                
                return dongerName + "\n" + "HP: " + hp + "\n" + "Damage modifer: " + damageMod + "\n" + "Armor modifer: " + armor + "\n" + "Magic defence: " + magicDef + "\n" + "Hit chance: " + acurityMod + "\n" + "Skill: " + SkillName + "\n" + "Skill chance: " + skillChance;
            }

        }
        public string DongerInfo
        {
            get
            {
                return dongerName;
            }
        }
       
        
        public Donger Clone()
        {
            return (Donger)this.MemberwiseClone();
        }
        public void AddEffect(int time, Action<Donger> ex)
        {
            Effect temp = effects.Find(x => x.efc == ex);
            if (temp!=null)
            {
                effects.Remove(temp);
            }
            effects.Add(new Effect(time, ex));
        }
        public void AddEndEffect(int time, Action<Donger> ex)
        {
            EndEffect temp = (EndEffect)effects.Find(x => x.efc == ex);
            if (temp != null)
            {
                effects.Remove(temp);
            }
            effects.Add(new EndEffect(time, ex));
        }
        public Donger(string n,int HP,Action<Donger> skll)
        {
            skill = skll;
            dongerName = n;
            hp = HP;
        }
        public Donger()
        {

        }
       
        public void TakeDamage(int dmg)
        {
            hp = hp - (int)(dmg/armor);
        }

        public void TakeMagicDamage(int dmg)
        {
            hp = hp - (int)(dmg / magicDef);
        }
        public void TakeDamageFromSkill(int dmg)
        {
            if (magicDef==defMagicDef)
                Donger.SendMessages("\t" + DongerInfo + " takes " + dmg.ToString() + " damage");
            else
                Donger.SendMessages("\t" + DongerInfo + " takes " + ((int)(dmg / magicDef)).ToString() + " damage");

            hp = hp - (int)(dmg/magicDef);
        }
        public void Heal(int heal)
        {
            Donger.SendMessages("\t"+DongerInfo + " heals by " + heal.ToString() + " hp");
            hp = hp + heal;
        }

        public string SkillMessage(string message)
        {
            return dongerName + " " + message + " " + dongerName;
        }
    }


public struct Player
    {
        public string name;
        public Donger donger;
   
        public bool nll;
        public string konfa; 
        public Player(string n, Donger dong, string konf)
        {
            name = n;
            donger = dong;
            nll = false;
            konfa = konf;
        }
       public Player(string n)
        {
            nll = true;
            name = "";
            donger = null;
            konfa= "";
        }
    }

public class DongerBattler:ICloneable
{
    //Donger t=new Donger("123",0,null);
    static List<Donger> dongers; 
    static Dictionary<string, string> confs =
                   new Dictionary<string, string>
                   {
                       {"#splasher-_-/$e36c265204653a65","ДотаКонфа"},
                       {"#splasher-_-/$omgwtfgglol;7fa80f21182dcf70","ДомСейджи"},
                       {"#nekonyak/$88001bf7f531a99e","Кантаеконфа"},
                       {"#xaos42/$1d4f74af8d0e6d36","НеведомаяКонфа"},
                   };

    public static Dictionary<Action<Donger>, string> SkillNames = new Dictionary<Action<Donger>, string>
    {
        {Donger.skillAutism,"Autism Strike"},
        {Donger.skillDongerhood,"Dongerhood"},
        {Donger.skillDongerMagic,"Donger Magic"},
        {Donger.skillDoubleAttack,"Shoot two niggers at once, shoot one nigger twice"},
        {Donger.skillEbola,"Ebola Strike"},
        {Donger.skillHealME,"Masturbate With Nivea Cream"},
        {Donger.skillMirrorCopy,"Skill Copy"},
        {Donger.skillOWLSEN,"Silence"},
        {Donger.skillPoisonBite,"Poison Bite"},
        {Donger.AttackSeija,"Seija Power"},
        {Donger.skillSTOOPED,"TRUMP aura"},
        {Donger.skillVampiricBite,"Vampiric Bite"},
        {Donger.skllDankMemes,"Dank Memes"},
        {Donger.skllENRAGE,"Enrage"},
        {Donger.skllMindControll,"Mind Control"},
       // {Donger.sklJaraxxusAttack,"Jaraxxus Attack"},
        {Donger.skilAllahAkbar,"Explosive Nature"},
        {Donger.skillKnight,"Stay Back Moderino"},
        {Donger.skillShrapnell,"Sｈｒａｐｎｅｌ"},
        {Donger.skillNakachan,"Кантай Но Айдору"},
        {Donger.skillSpirits,"HP swap"},
        {Donger.skillAntiMage,""},
        {Donger.skillRape,"Rape"},

    };
    public Object Clone()
    {
        return this.MemberwiseClone();
    }
    public static void Init()
    {
        dongers= new List<Donger>
        {
             {new Donger {
                 dongerName=@"└( ° ͜ʖ͡°)┐",
                 skill=Donger.skllDankMemes
                }
             },
             {new Donger {
                 dongerName=@"(✿-̶●̃益●̶̃)",
                 hp=35,
                 skill= Donger.skllENRAGE,
                }
             },
             {new Donger {
                 dongerName=@"ヽ(◉◡◔)ﾉ",
                 skill=Donger.skillAutism
                }
             },
             {new Donger {
                 dongerName=@"༼ ºل͟º ༽",
                 skill=Donger.skillDongerhood
                }
             },
             {new Donger {
                 dongerName=@"୧༼ ͡◉ل͜ ͡◉༽୨",
                 skill=Donger.skllMindControll,
                 hp=25
                }
             },
              {new Donger {
                 dongerName=@" ̿ ̿ ̿ ̿ ̿'̿'\̵͇̿̿\з=( ͠° ͟ʖ ͡°)=ε/̵͇̿̿/'̿̿ ̿ ̿ ̿ ̿ ̿ ",
                 rapeble=false,
                 rapeString=@" ̿ ̿ ̿ ̿ ̿'̿'\̵͇̿̿\з=( ͠■ ͟ʖ ͡■)=ε/̵͇̿̿/'̿̿ ̿ ̿ ̿ ̿ ̿  I am to cool to be raped  ̿ ̿ ̿ ̿ ̿'̿'\̵͇̿̿\з=( ͠■ ͟ʖ ͡■)=ε/̵͇̿̿/'̿̿ ̿ ̿ ̿ ̿ ̿ ",
                 skill=Donger.skillDoubleAttack,
                }
             },
             {new Donger {
                 dongerName=@"乁[ᓀ˵▾˵ᓂ]ㄏ",
                 skill=Donger.skillOWLSEN,
                 skillChance=25
                }
             },
             {new Donger {
                 dongerName=@"(つ ͡° ͜ʖ ͡°)つ",
                 skill=Donger.skillHealME,
                 skillUses=2
                }
             },
             {new Donger {
                 dongerName=@"(ﾉ･ｪ･)ﾉ",
                 skill=Donger.skillMirrorCopy,
                 skillUses=100,
                 skillChance=25,
                }
             },
              {new Donger {
                 dongerName=@"(๑•﹏•)",
                 skill=Donger.skillEbola,
                }
             },
             {new Donger {
                 dongerName=@"ᕙ( ◉益◔ )ᕗ",
                 hp=35,
                 skill= Donger.skillSTOOPED,
                 skillChance=40,
                 skillUses=100,
                 acurityMod=100,
                 damageMod=2
                }
             },
             {new Donger {
                 dongerName=@"(∩ ͡° ͜ʖ ͡°)⊃━☆ﾟ. * ･ ｡ﾟ",
                 skill=Donger.skillDongerMagic,
                 skillChance=80,
                 skillUses=100,
                 damageMod=0.5,
                }
             },
             {new Donger {
                 dongerName=@"/╲/\╭( ͡° ͡° ͜ʖ ͡° ͡°)╮/\╱\",
                 rapeble=false,
                 rapeString=@"/╲/\╭( ͡° ͡° ͜ʖ ͡° ͡°)╮/\╱\ You can't rape the Rape Spider!  /╲/\╭( ͡° ͡° ͜ʖ ͡° ͡°)╮/\╱\",
                 skill=Donger.skillPoisonBite,
                 skillUses=2
                }
             },
              {new Donger {
                 dongerName=@"ԅ( ͒ ۝ ͒ )ᕤ",
                 skill=Donger.skilAllahAkbar,
                 skillChance=25,
                }
             },

             {new Donger {
                 dongerName=@"<:::::[]=¤༼ຈل͜ຈ༽ﾉ",
                 rapeble=false,
                 rapeString=@"<:::::[]=¤( ͡° ͜ʖ ͡°)ﾉ My SWORD is too big for you, boy? <:::::[]=¤( ͡° ͜ʖ ͡°)ﾉ",
                 hp=20,
                 skill=Donger.skillKnight,
                 skillUses=2,
                 acurityMod=100,
                 damageMod=0.75,
                 armor=2
                }
             },
             {new Donger {
                 dongerName=@"༼ຈل͜ຈ༽ﾉ·︻̷̿┻̿═━一",
                 skill=Donger.skillShrapnell,
                 acurityMod=100
                }
             },
             {new Donger {
                 dongerName=@"ヽ༼@ل͜ຈ༽ﾉ",
                 skill=Donger.skillVampiricBite,
                 hp=35
                }
             },
             {new Donger {
                 dongerName=@"\( ͡~ ͜ʖ ͡°)z",
                 skill=Donger.skillNakachan,
                 hp=40
                }
             },
             {new Donger {
                 dongerName=@"( ͡° ͜ʖ ͡°)",
                 skill=Donger.skillRape,
                 attack=Donger.LewdAttack,
                }
             },
             {new Donger {
                 dongerName=@"ヽ༼ʘ̚ل͜ʘ̚༽ﾉ",
                attack=Donger.spiritAttack,
                skill=Donger.skillSpirits,
                }
             },
             {new Donger {
                 dongerName=@"(ง ͡ʘ ͜ʖ ͡ʘ)ง",
                 hp=25,
                 magicDef=2.5,
                 skill=Donger.skillAntiMage,
                 attack=Donger.AntimageAttack,
                }
             },
             
            //{new Donger(@"└( ° ͜ʖ͡°)┐",Donger.defHp, Donger.skllDankMemes)},
            //{new Donger(@"(✿-̶●̃益●̶̃)",35, Donger.skllENRAGE)},
            //{new Donger(@"ヽ(◉◡◔)ﾉ",Donger.defHp, Donger.skillAutism)},
            //{new Donger(@"༼ ºل͟º ༽",Donger.defHp, Donger.skillDongerhood)},
            //{new Donger(@"୧༼ ͡◉ل͜ ͡◉༽୨",25, Donger.skllMindControll,10,Donger.defSkillUses,Donger.defAccMod,0.5)},
            //{new Donger(@" ̿ ̿ ̿ ̿ ̿'̿'\̵͇̿̿\з=( ͠° ͟ʖ ͡°)=ε/̵͇̿̿/'̿̿ ̿ ̿ ̿ ̿ ̿ ",Donger.defHp, Donger.skillDoubleAttack)},
            //{new Donger(@"乁[ᓀ˵▾˵ᓂ]ㄏ",Donger.defHp, Donger.skillOWLSEN,25,Donger.defSkillUses,Donger.defAccMod,Donger.defDamageMod)},
            //{new Donger(@"(つ ͡° ͜ʖ ͡°)つ",Donger.defHp, Donger.skillHealME,Donger.defSkillChanse,2,Donger.defAccMod,Donger.defDamageMod)},
            //{new Donger(@"(ﾉ･ｪ･)ﾉ",Donger.defHp, Donger.skillMirrorCopy,25,100,Donger.defAccMod,Donger.defDamageMod)},
            //{new Donger(@"(๑•﹏•)",Donger.defHp, Donger.skillEbola)},
            //{new Donger(@"ᕙ( ◉益◔ )ᕗ",35,Donger.skillSTOOPED,40,99,100,2)},
            //{new Donger(@"＼＼\(۶•̀ᴗ•́)۶//／／",Donger.defHp,Donger.skillDongerMagic,100,99,0,0)},
            //{new Donger(@"/╲/\╭( ͡° ͡° ͜ʖ ͡° ͡°)╮/\╱\",Donger.defHp,Donger.skillPoisonBite,Donger.defSkillChanse,2,Donger.defAccMod,Donger.defDamageMod)},
            //{new Donger(@"ԅ( ͒ ۝ ͒ )ᕤ",30,Donger.skilAllahAkbar,25,1,Donger.defAccMod,Donger.defDamageMod)},
            //{new Donger(@"<:::::[]=¤༼ຈل͜ຈ༽ﾉ",20,Donger.skillKnight,15,2,100,0.75,2)},
            //{new Donger(@"༼ຈل͜ຈ༽ﾉ·︻̷̿┻̿═━一",30,Donger.skillShrapnell,Donger.defSkillChanse,Donger.defSkillUses,99,Donger.defDamageMod)},
            //{new Donger(@"\( ͡~ ͜ʖ ͡°)z",40, Donger.skillNakachan)},
            //{new Donger(@"ヽ༼@ل͜ຈ༽ﾉ",35,Donger.skillVampiricBite)},

        };

    }
    Player p1;
    Player p2;
    protected static Chat ch1;
    protected static Chat ch2;
    System.Windows.Threading.DispatcherTimer restTimer;

    public DongerBattler()
    {
        p1 = new Player("");
        p2 = new Player("");

    }
    protected static string GetKonfaName(string knf)
    {
        if (!confs.ContainsKey(knf))
            return "???";
        else
            return confs[knf];
    }
    protected static void SendMessages(string msg)
    {
        ch1.SendMessage(msg);
        if (ch2.Name != ch1.Name)
            ch2.SendMessage(msg);
    }
    void RestTick(object sender, EventArgs e)
    {
        //DoShit("SeijaBot", ch1);
        InitBatle("SeijaBot", ch1);
        restTimer.Stop();
    }
    public void InitBatle(string Name, Chat ch)
    {
        if (p1.nll == true)
        {
            p1 = new Player(Name, dongers[SeijaHelper.RandomProvider.GetThreadRandom().Next(0, dongers.Count)].Clone(), GetKonfaName(ch.Name));
            ch.SendMessage("Первый игрок - " + p1.name + " и его донгер: \n\r" + p1.donger.Info + "\n\rОжидание второго игрока");
            ch1 = ch;
            restTimer = new System.Windows.Threading.DispatcherTimer();
            restTimer.Interval = new TimeSpan(0, 0, 30);
            restTimer.Tick += RestTick;
            restTimer.Start();
        }
        else
        {
            Init();
            restTimer.Stop();
            ch2 = ch;
            if (Name == p1.name)
                p2 = new Player("SeijaBot", Donger.seija.Clone(), GetKonfaName(ch.Name));
            else
            {
                Donger temp;
                do
                {
                    temp = dongers[SeijaHelper.RandomProvider.GetThreadRandom().Next(0, dongers.Count)].Clone();
                }
                while (temp.dongerName == p1.donger.dongerName);
                p2 = new Player(Name, temp.Clone(), GetKonfaName(ch.Name));
            }
            SendMessages("Второй игрок - " + p2.name + " и его донгер: \n\r" + p2.donger.Info);
            StartBattle();
        }
    }
    public void InitBatle(string Name, Chat ch, string donger)
    {
        Init();
        if (p1.nll == true)
        {
            Donger tmp=dongers.Find(x => x.dongerName == donger);
            if (tmp==null)
                p1 = new Player(Name, dongers[SeijaHelper.RandomProvider.GetThreadRandom().Next(0, dongers.Count)].Clone(), GetKonfaName(ch.Name));
            else
                p1 = new Player(Name, tmp.Clone(), GetKonfaName(ch.Name));
            ch.SendMessage("Первый игрок - " + p1.name + " и его донгер: \n\r" + p1.donger.Info + "\n\rОжидание второго игрока");
            ch1 = ch;
            restTimer = new System.Windows.Threading.DispatcherTimer();
            restTimer.Interval = new TimeSpan(0, 0, 30);
            restTimer.Tick += RestTick;
            restTimer.Start();
        }
        else
        {
            restTimer.Stop();
            ch2 = ch;
            if (Name == p1.name)
                p2 = new Player("SeijaBot", Donger.seija.Clone(), GetKonfaName(ch.Name));
            else
            {
                Donger tmp=dongers.Find(x => x.dongerName == donger);
                if (tmp == null)
                {
                    Donger temp;
                    do
                    {
                        temp = dongers[SeijaHelper.RandomProvider.GetThreadRandom().Next(0, dongers.Count)].Clone();
                    }
                    while (temp.dongerName == p1.donger.dongerName);
                    p2 = new Player(Name, temp.Clone(), GetKonfaName(ch.Name));
                }
                else
                    p2 = new Player(Name, tmp.Clone(), GetKonfaName(ch.Name));
                
            }
            SendMessages("Второй игрок - " + p2.name + " и его донгер: \n\r" + p2.donger.Info);
            StartBattle();
        }
    }
    public void StartBattle()
    {
        SeijaHelper.isEn = false;
        Donger currentDonger;
        System.Threading.Thread.Sleep(1000);
                SendMessages("Да начнется битва между [" + p1.konfa + "] " + p1.name + " и [" + p2.konfa + "] " + p2.name + "!");
                p1.donger.enemy = p2.donger;
                p1.donger.systemEnemy = p2.donger;
                p2.donger.enemy = p1.donger;
                p2.donger.systemEnemy = p1.donger;
                currentDonger = p1.donger;
                while (p1.donger.hp > 0 && p2.donger.hp > 0)
                {
                    System.Threading.Thread.Sleep(1000);
                    currentDonger.attack(currentDonger);

                    //currentDonger.RollSkill();
                    currentDonger = currentDonger.systemEnemy;
                    //SendMessages(p1.donger + " (" + p1.dongerhp + ") VS " + p2.donger + " (" + p2.dongerhp + ")");
                    //System.Threading.Thread.Sleep(1000);
                    //p1.dongerhp = p1.dongerhp - (SeijaHelper.FixedRandom(1, 10));
                    //p2.dongerhp = p2.dongerhp - (new Random().Next(1, 10));
                }
                SendMessages(p1.donger.dongerName + " (" + p1.donger.hp + ") VS " + p2.donger.dongerName + " (" + p2.donger.hp + ")");
                System.Threading.Thread.Sleep(1000);
                if (p1.donger.hp <= 0 && p2.donger.hp <= 0)
                    SendMessages("Ничья!");
                else
                {
                    Player winner = p1.donger.hp <= 0 ? p2 : p1;
                    //Donger winner_dong = p1.donger.hp <= 0 ? p2.donger.Clone() : p1.donger.Clone();

                    SendMessages("В битве победил: " + winner.name + " и его донгер " + winner.donger.dongerName);

                }
                p1 = new Player("");
                p2 = new Player("");
                ch1 = null;
                ch2 = null;
                SeijaHelper.isEn = true;
            }
    //public void DoShit(string Name, Chat ch)
    //{
    //    try
    //    {
    //        if (p1.nll == true)
    //        {
    //            p1 = new Player(Name, dongers[SeijaHelper.RandomProvider.rnd().Next(0, dongers.Count)].Clone(), GetKonfaName(ch.Name));
    //            ch.SendMessage("Первый игрок - " + p1.name + " и его донгер: \n\r" + p1.donger.Info + "\n\rОжидание второго игрока");
    //            ch1 = ch;
    //            restTimer = new System.Windows.Threading.DispatcherTimer();
    //            restTimer.Interval = new TimeSpan(0, 0, 30);
    //            restTimer.Tick += RestTick;
    //            restTimer.Start();
    //        }
    //        else
    //        {
    //            Init();
    //            Donger currentDonger;
    //            restTimer.Stop();
    //            ch2 = ch;
    //            if (Name == p1.name)
    //                p2 = new Player("SeijaBot", Donger.seija.Clone(), GetKonfaName(ch.Name));
    //            else
    //            {
    //                Donger temp;
    //                do
    //                {
    //                    temp = dongers[SeijaHelper.RandomProvider.rnd().Next(0, dongers.Count)].Clone();
    //                }
    //                while (temp.dongerName == p1.donger.dongerName);
    //                p2 = new Player(Name, temp.Clone(), GetKonfaName(ch.Name));
    //            }
    //            SendMessages("Второй игрок - " + p2.name + " и его донгер: \n\r" + p2.donger.Info);
    //            System.Threading.Thread.Sleep(1000);
    //            SendMessages("Да начнется битва между [" + p1.konfa + "] " + p1.name + " и [" + p2.konfa + "] " + p2.name + "!");
    //            p1.donger.enemy = p2.donger;
    //            p1.donger.systemEnemy = p2.donger;
    //            p2.donger.enemy = p1.donger;
    //            p2.donger.systemEnemy = p1.donger;
    //            currentDonger = p1.donger;
    //            while (p1.donger.hp > 0 && p2.donger.hp > 0)
    //            {
    //                System.Threading.Thread.Sleep(1000);
    //                currentDonger.attack(currentDonger);

    //                //currentDonger.RollSkill();
    //                currentDonger = currentDonger.systemEnemy;
    //                //SendMessages(p1.donger + " (" + p1.dongerhp + ") VS " + p2.donger + " (" + p2.dongerhp + ")");
    //                //System.Threading.Thread.Sleep(1000);
    //                //p1.dongerhp = p1.dongerhp - (SeijaHelper.FixedRandom(1, 10));
    //                //p2.dongerhp = p2.dongerhp - (new Random().Next(1, 10));
    //            }
    //            SendMessages(p1.donger.dongerName + " (" + p1.donger.hp + ") VS " + p2.donger.dongerName + " (" + p2.donger.hp + ")");
    //            System.Threading.Thread.Sleep(1000);
    //            if (p1.donger.hp <= 0 && p2.donger.hp <= 0)
    //                SendMessages("Ничья!");
    //            else
    //            {
    //                Player winner = p1.donger.hp <= 0 ? p2 : p1;
    //                //Donger winner_dong = p1.donger.hp <= 0 ? p2.donger.Clone() : p1.donger.Clone();

    //                SendMessages("В битве победил: " + winner.name + " и его донгер " + winner.donger.dongerName);

    //            }
    //            p1 = new Player("");
    //            p2 = new Player("");
    //            ch1 = null;
    //            ch2 = null;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        SendMessages(ex.Message);
    //        p1 = new Player("");
    //        p2 = new Player("");
    //        ch1 = null;
    //        ch2 = null;

    //    }
    //}


}

}