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
    public class Effect
    {
        public Action<Donger> efc;
        public int Timer;
        public void Execute(Donger dong)
        {
            efc(dong);
            Timer--;
        }
        public Effect(int time, Action<Donger> ex)
        {
            efc = ex;
            Timer = time;
        }
    }
    public class EndEffect : Effect
    {
        public EndEffect(int time, Action<Donger> ex):base(time,ex){          
        }
        public void Execute(Donger dong)
        {
            Timer--;
            if (Timer==0)
                efc(dong); 
        }
    }
    //class Effect
    //{
    //    public Action<Donger> execute;
    //    public int Timer;
    //    public Effect(int time, Action<Donger> ex)
    //    {
    //        efc = ex;
    //        Timer = time;
    //    }
    //}
    public partial class Donger : DongerBattler
    {


        public static Donger seija =/*new Donger(@"(✿◕⁀◕)",defHp,Donger.sklSeija,100,100,defAccMod,defDamageMod);*/
        new Donger
        {
            //dongerName = @"(✿◕⁀◕)",
            dongerName=@"(◕‿◕✿)",
            //rapeble = false,
            //rapeString = @"(✿◕⁀◕) ǝpᴉs ƃuoɹʍ (✿◕⁀◕)",
            hp = 60,
            skill = AttackSeija,
            attack=AttackSeija,
            skillUses = 0,
            skillChance=0,
            acurityMod = 100,
            damageMod = 1,
            armor = 1,
            magicDef=1
        };

        //public static void sklJaraxxusAttack(Donger dong)
        //{
        //    Donger.SendMessages(dong.SkillMessage("INFERNO"));
        //    //Donger.SendMessages("୧༼ಠ益ಠ༽୨ INFERNO ୧༼ಠ益ಠ༽୨");
        //    dong.enemy.TakeDamageFromSkill(6);
        //    System.Threading.Thread.Sleep(1000);
        //    int damage = 3;
        //    Donger.SendMessages(dong.dongerName + " attacks " + dong.enemy.dongerName + " and deals " + damage.ToString() + " damage");
        //    dong.enemy.TakeDamage(damage);
        //}
        public static void AttackSeija(Donger dong)
        {
            if (dong.hp <= 40)
            {
                dong.dongerName = @"(✿◕⁀◕)";
                Donger.SendMessages("\t" + dong.SkillMessage("¿ǝɯ ʇɐǝɟǝp uɐɔ noʎ ʞuıɥʇ noʎ op"));
                System.Threading.Thread.Sleep(1000);
                dong.rapeble = false;
                dong.rapeString = @"(✿◕⁀◕) ǝpᴉs ƃuoɹʍ (✿◕⁀◕)";
                dong.attack = AttackSeija2;
                dong.armor *= 1.5;
                dong.magicDef *= 1.5;
                dong.attack(dong);
            }
            else
            {
                int damage = SeijaHelper.RandomProvider.GetThreadRandom().Next(3, 10);
                damage = (int)(damage * dong.damageMod);
                if (dong.enemy.armor == Donger.defArmor)
                    Donger.SendMessages(dong.DongerInfo + " attacks " + dong.enemy.DongerInfo + " and deals " + damage.ToString() + " damage");
                else
                    Donger.SendMessages(dong.DongerInfo + " attacks " + dong.enemy.DongerInfo + " and deals " + ((int)(damage / dong.enemy.armor)).ToString() + " damage");
                dong.enemy.TakeDamage(damage);
            }
        }
        public static void AttackSeija2(Donger dong)
        {
            if (dong.hp <= 20)
            {
                dong.dongerName = @"(✿>‾<)";
                Donger.SendMessages("\t" + dong.SkillMessage("¡noʎ uɯɐp"));
                System.Threading.Thread.Sleep(1000);
                dong.rapeble = false;
                dong.rapeString = @"(✿>‾<) ǝpᴉs ƃuoɹʍ (✿>‾<)";
                dong.attack = AttackSeija3;
                dong.damageMod *= 2;
                dong.attack(dong);
            }
            else
            {
                int damage = SeijaHelper.RandomProvider.GetThreadRandom().Next(3, 10);
                damage = (int)(damage * dong.damageMod);
                if (dong.enemy.armor == Donger.defArmor)
                    Donger.SendMessages(dong.DongerInfo + " attacks " + dong.enemy.DongerInfo + " and deals " + damage.ToString() + " damage");
                else
                    Donger.SendMessages(dong.DongerInfo + " attacks " + dong.enemy.DongerInfo + " and deals " + ((int)(damage / dong.enemy.armor)).ToString() + " damage");
                dong.enemy.TakeDamage(damage);
            }
        }
        public static void AttackSeija3(Donger dong)
        {
                int damage = SeijaHelper.RandomProvider.GetThreadRandom().Next(3, 10);
                damage = (int)(damage * dong.damageMod);
                if (dong.enemy.armor == Donger.defArmor)
                    Donger.SendMessages(dong.DongerInfo + " attacks " + dong.enemy.DongerInfo + " and deals " + damage.ToString() + " damage");
                else
                    Donger.SendMessages(dong.DongerInfo + " attacks " + dong.enemy.DongerInfo + " and deals " + ((int)(damage / dong.enemy.armor)).ToString() + " damage");
                dong.enemy.TakeDamage(damage);
        }
         public static void skllDankMemes(Donger dong)
        {
            Donger.SendMessages("\t" + dong.SkillMessage("DANK ℳℰℳℰS"));
            //Donger.SendMessages("└( ° ͜ʖ͡°)┐Born too late to explore the Earth, born too soon to explore the Galaxy. Born just in time to post DANK ℳℰℳℰS └( ° ͜ʖ͡°)┐");
            dong.enemy.TakeDamageFromSkill(SeijaHelper.RandomProvider.GetThreadRandom().Next(7, 16));
        }
        public static void skllMindControll(Donger dong)
        {
            Donger.SendMessages("\t" + dong.SkillMessage("YOUR SOUL BELONGS TO ME"));
            Donger.SendMessages("\t" + dong.enemy.dongerName + " had its will revoked for three turns.");
            dong.enemy.attack = Donger.MindlessAttack;
            dong.enemy.AddEndEffect(4, endAttackChange);
        }
        public static void skllENRAGE(Donger dong)
        {
            Donger.SendMessages("\t" + dong.SkillMessage("NOW I AM FUCKING SERIOUS"));
            if (dong.effects.Count>=0)
            {
                Donger.SendMessages("\t" + "All effects are purged from " + dong.dongerName);
                dong.effects = new List<Effect>();
            }          
            dong.damageMod = dong.damageMod * 3;
            dong.acurityMod = 100;
            dong.attack = Donger.DeafaultAttack;
        }
        public static void skillOWLSEN(Donger dong)
        {
            Donger.SendMessages("\t" + dong.SkillMessage("SILENCE"));
            dong.enemy.skill = emtySkill;
            if (dong.enemy.damageMod > 1)
                dong.enemy.damageMod = 1;
            if (dong.enemy.acurityMod > 85)
                dong.enemy.acurityMod = 85;
        }

        public static void skillRape(Donger dong)
        {
            if (dong.enemy.rapeble == true)
            {
                Donger.SendMessages("\t" + dong.SkillMessage("LETS DO IT, BOY"));
                System.Threading.Thread.Sleep(500);
                Donger.SendMessages("\t" + "\t" + "( ͡° ͜ʖ " + dong.enemy.dongerName);
                System.Threading.Thread.Sleep(500);
                dong.enemy.TakeDamageFromSkill(SeijaHelper.RandomProvider.GetThreadRandom().Next(50, 150));
            }
            else
            {
                Donger.SendMessages("\t" + dong.SkillMessage("LETS DO IT, BOY"));
                System.Threading.Thread.Sleep(500);
                Donger.SendMessages("\t" + "Rape attempt failed!");
                System.Threading.Thread.Sleep(500);
                Donger.SendMessages(dong.enemy.rapeString);
            }
        }

        public static void skilAllahAkbar(Donger dong)
        {
            Donger.SendMessages("\t" + dong.SkillMessage("ALLAHU AKBAR"));
            dong.enemy.TakeDamageFromSkill(SeijaHelper.RandomProvider.GetThreadRandom().Next(20, 70));
            dong.TakeDamageFromSkill(SeijaHelper.RandomProvider.GetThreadRandom().Next(20, 70));
        }
        public static void skillHealME(Donger dong)
        {
            Donger.SendMessages("\t" + dong.SkillMessage("HILIMSYA JIVEM"));
            dong.Heal(SeijaHelper.RandomProvider.GetThreadRandom().Next(9, 20));
        }

        public static void skillEbola(Donger dong)
        {
            Donger.SendMessages("\t" + dong.SkillMessage("EBOLA STRIKE"));
            dong.enemy.damageMod = dong.enemy.damageMod / 3;
        }

        public static void skillVampiricBite(Donger dong)
        {
            int dmg = SeijaHelper.RandomProvider.GetThreadRandom().Next(7, 16);
            Donger.SendMessages("\t" + dong.SkillMessage("VAMPIRIC BITE"));
            dong.enemy.TakeDamageFromSkill(dmg);
            dong.Heal(dmg);
        }
        public static void skillDongerhood(Donger dong)
        {

            Donger.SendMessages("\t" + "༼ ºل͟º༼ ºل͟º༼ ºل͟º ༽ºل͟º ༽ºل͟º ༽YOU CAME TO THE WRONG DONGERHOOD༼ ºل͟º༼ ºل͟º༼ ºل͟º ༽ºل͟º ༽ºل͟º ༽");
            System.Threading.Thread.Sleep(300);
            dong.enemy.TakeDamageFromSkill(SeijaHelper.RandomProvider.GetThreadRandom().Next(2, 6));
            System.Threading.Thread.Sleep(300);
            dong.enemy.TakeDamageFromSkill(SeijaHelper.RandomProvider.GetThreadRandom().Next(2, 6));
            System.Threading.Thread.Sleep(300);
            dong.enemy.TakeDamageFromSkill(SeijaHelper.RandomProvider.GetThreadRandom().Next(2, 6));
            System.Threading.Thread.Sleep(300);
            dong.enemy.TakeDamageFromSkill(SeijaHelper.RandomProvider.GetThreadRandom().Next(2, 6));
            System.Threading.Thread.Sleep(300);
            dong.enemy.TakeDamageFromSkill(SeijaHelper.RandomProvider.GetThreadRandom().Next(2, 6));
            System.Threading.Thread.Sleep(300);
        }

        public static void skillDoubleAttack(Donger dong)
        {
            Donger.SendMessages("\t" + dong.SkillMessage("PREPARE TO DIE"));          
            dong.enemy.TakeDamageFromSkill(SeijaHelper.RandomProvider.GetThreadRandom().Next(10, 16));
            dong.enemy.TakeDamageFromSkill(SeijaHelper.RandomProvider.GetThreadRandom().Next(10, 16));
        }
        public static void skillSpirits(Donger dong)
        {
            Donger.SendMessages("\t" + dong.SkillMessage("Da spirits be restless"));
            System.Threading.Thread.Sleep(300);
            Donger.SendMessages("\t" + "HP SWAPPED");
            int temp = dong.enemy.hp;
            dong.enemy.hp = dong.hp;
            dong.hp = temp;
            
        }
        public static void skillAutism(Donger dong)
        {
            Donger.SendMessages("\t" + dong.SkillMessage("YOU ARE NOW AUTISTIC"));
            dong.enemy.acurityMod=dong.enemy.acurityMod/2;
        }

        public static void skillDongerMagic(Donger dong)
        {
            int magic = SeijaHelper.RandomProvider.GetThreadRandom().Next(1, 8);
            switch (magic)
            {
                case 1:
                    Donger.SendMessages("\t" + dong.SkillMessage("THUNDERBOLT"));
                    dong.enemy.TakeDamageFromSkill(15);
                    break;
               case 2:
                    Donger.SendMessages("\t" + dong.SkillMessage("FAILED THUNDERBOLT"));
                    dong.TakeDamageFromSkill(15);
                    break;
                case 3:
                    Donger.SendMessages("\t" + dong.SkillMessage("FIREBALL"));
                    Effect tmp = dong.enemy.effects.Find(x => x.efc == endAttackChange);
                    if (tmp != null)
                    {
                        dong.enemy.effects.Remove(tmp);
                        Donger.SendMessages("\t" + "Fireball melted the ice, so " + dong.enemy.dongerName + " is free now!");
                    }
                    else
                    {
                        dong.enemy.TakeDamageFromSkill(SeijaHelper.RandomProvider.GetThreadRandom().Next(5, 11));
                        dong.enemy.AddEffect(2, effectBurning);
                    }
                    break;
                case 4:
                    Donger.SendMessages("\t" + dong.SkillMessage("EARTHQUAKE"));
                    //Donger.SendMessages(@"＼＼\(۶•̀ᴗ•́)۶//／／ THUNDERBOLT ＼＼\(۶•̀ᴗ•́)۶//／／");
                    dong.enemy.TakeDamageFromSkill(SeijaHelper.RandomProvider.GetThreadRandom().Next(3, 6));
                    dong.enemy.TakeDamageFromSkill(SeijaHelper.RandomProvider.GetThreadRandom().Next(3, 6));
                    dong.enemy.TakeDamageFromSkill(SeijaHelper.RandomProvider.GetThreadRandom().Next(3, 6));
                    break;
                case 5:
                    Donger.SendMessages("\t" + dong.SkillMessage("ICE LANCE"));
                    //Donger.SendMessages(@"＼＼\(۶•̀ᴗ•́)۶//／／ THUNDERBOLT ＼＼\(۶•̀ᴗ•́)۶//／／");
                    if (dong.enemy.attack == Donger.FrozenAttack)
                    {
                        dong.enemy.TakeDamageFromSkill(SeijaHelper.RandomProvider.GetThreadRandom().Next(15, 26));
                    }
                    else
                    {
                        dong.enemy.attack = Donger.FrozenAttack;
                        //dong.enemy.effectTimer = 3;
                        dong.enemy.AddEndEffect(3,endAttackChange);
                    }
                    break;
                case 6:
                    Donger.SendMessages("\t" + dong.SkillMessage("ROCK ARMOR"));
                    dong.armor = dong.armor * 1.5;
                    break;
                case 7:
                    Donger.SendMessages("\t" + dong.SkillMessage("MAGIC ARMOR"));
                    dong.magicDef = dong.magicDef* 1.5;
                    break;
            }
        }
        public static void skillKnight(Donger dong)
        {
            Donger.SendMessages("\t" + dong.SkillMessage("I am the knight of spamerino. Stand back foul moderino"));
            dong.damageMod = dong.damageMod * 1.5;
            dong.armor = dong.armor * 1.5;
        }

        public static void skillAntiMage(Donger dong)
        {
            Donger.SendMessages("\t" + dong.SkillMessage("I bring an end to magic."));
            dong.enemy.attack = Donger.AntimageCurseAttack;
        }

        public static void skillShrapnell(Donger dong)
        {
            Donger.SendMessages("\t" + @"、ヽヽ｀ヽ｀、ヽヽ｀ヽ、ヽヽ༼ຈ ل͜ຈ༽ﾉ•︻̷┻̿═━一 ｈｏｈｏ ｈａｈａ ｓｈｒａｐｎｅｌヽ༼ຈل͜ຈ༽ﾉ•︻̷┻̿═━一 、ヽヽ｀ヽ｀、ヽヽ｀ヽ、ヽ");
            dong.enemy.AddEffect(10,effectShrapnel);
        }
        public static void skillNakachan(Donger dong)
        {
            Donger.SendMessages("\t" + dong.SkillMessage("К А Н Т А Й  Н О  А Й Д О Р У  Н А К А - Ч А Н  Д А  Ё"));
            dong.enemy.TakeDamageFromSkill(1);
        }
    


        public static void effectShrapnel(Donger dong)
        {
            int dmg = SeijaHelper.RandomProvider.GetThreadRandom().Next(2, 5);
            Donger.SendMessages("\t" + dong.dongerName + " takes " + dmg.ToString() + " damage from shrapnel");
            dong.TakeMagicDamage(dmg);
        }



        public static void skillSTOOPED(Donger dong)
        {
            Donger.SendMessages("\t" + dong.SkillMessage("TRUMP NO STOOPED"));

        }

        public static void skillMirrorCopy(Donger dong)
        {
            Donger.SendMessages("\t" + dong.SkillMessage("MIRROR COPY"));
            dong.skill = dong.enemy.skill;
            if (dong.enemy.damageMod > dong.damageMod)
            dong.damageMod = dong.enemy.damageMod;
            if (dong.enemy.acurityMod > dong.acurityMod)
            dong.acurityMod = dong.enemy.acurityMod;
            if (dong.enemy.armor > dong.armor)
            dong.armor = dong.enemy.armor;
            if (dong.enemy.magicDef > dong.magicDef)
            dong.magicDef = dong.enemy.magicDef;
            if (dong.enemy.skillChance > dong.skillChance)
                dong.skillChance = dong.enemy.skillChance;
        }
        public static void skillPoisonBite(Donger dong)
        {
            Donger.SendMessages("\t" + dong.SkillMessage("POISON BITE"));
            dong.enemy.TakeDamageFromSkill(SeijaHelper.RandomProvider.GetThreadRandom().Next(3, 8));
            dong.enemy.AddEffect(5,effectPoison);
        }
        public static void effectPoison(Donger dong)
        {
            int dmg = SeijaHelper.RandomProvider.GetThreadRandom().Next(1, 6);
            Donger.SendMessages("\t" + dong.dongerName + " takes " + dmg.ToString() + " damage from poison");
            dong.TakeMagicDamage(dmg);
        }
        public static void effectBurning(Donger dong)
        {
            int dmg = SeijaHelper.RandomProvider.GetThreadRandom().Next(1, 6);
            Donger.SendMessages("\t" + dong.dongerName + " takes " + dmg.ToString() + " damage from burning");
            dong.TakeMagicDamage(dmg);
        }
        public static void endAttackChange(Donger dong)
        {
                dong.attack = Donger.DeafaultAttack;
        }
        public static void emtySkill(Donger dong)
        {

        }

        public static void MindlessAttack(Donger dong)
        {
            foreach (Effect f in dong.effects)
            {
                f.Execute(dong);
            }
            dong.effects.RemoveAll(x => x.Timer <= 0);
          
                int strike = SeijaHelper.RandomProvider.GetThreadRandom().Next(0, 100);
                if (strike < dong.acurityMod)
                {
                    int damage = SeijaHelper.RandomProvider.GetThreadRandom().Next(3, 10);
                    //damage=?;
                    damage = (int)(damage * dong.damageMod);
                    if (dong.armor == Donger.defArmor)
                        Donger.SendMessages(dong.DongerInfo + " attacks " + dong.DongerInfo + " and deals " + damage.ToString() + " damage");
                    else
                        Donger.SendMessages(dong.DongerInfo + " attacks " + dong.DongerInfo + " and deals " + ((int)(damage / dong.armor)).ToString() + " damage");
                    dong.TakeDamage(damage);
                }
                else
                    Donger.SendMessages(dong.DongerInfo + " misses!");
        }
        public static void spiritAttack(Donger dong)
        {
            foreach (Effect f in dong.effects)
            {
                f.Execute(dong);
            }
            dong.effects.RemoveAll(x => x.Timer <= 0);    
            if (dong.enemy.hp>dong.hp+7 && dong.skillUses > 0)
            {
                if (dong.skill == Donger.emtySkill)
                    Donger.SendMessages("\t" + dong.DongerInfo + " uses skill but it fails!");
                else
                {
                    //Donger.SendMessages(dongerName + " uses skill!");
                    dong.skill(dong);
                }
                dong.skillUses--;
            }

                int strike = SeijaHelper.RandomProvider.GetThreadRandom().Next(0, 100);
                if (strike < dong.acurityMod)
                {
                    int damage = SeijaHelper.RandomProvider.GetThreadRandom().Next(3, 10);
                    //damage=?;
                    damage = (int)(damage * dong.damageMod);
                    if (dong.enemy.armor == Donger.defArmor)
                        Donger.SendMessages(dong.DongerInfo + " attacks " + dong.enemy.DongerInfo + " and deals " + damage.ToString() + " damage");
                    else
                        Donger.SendMessages(dong.DongerInfo + " attacks " + dong.enemy.DongerInfo + " and deals " + ((int)(damage / dong.enemy.armor)).ToString() + " damage");
                    dong.enemy.TakeDamage(damage);
                }
                else
                    Donger.SendMessages(dong.DongerInfo + " misses!");
          
        }
        public static void AntimageAttack(Donger dong)
        {
            foreach (Effect f in dong.effects)
            {
                f.Execute(dong);
            }
            dong.effects.RemoveAll(x => x.Timer <= 0);

            if (dong.skillUses > 0)
            {
                if (dong.skill == Donger.emtySkill)
                    Donger.SendMessages("\t" + dong.DongerInfo + " uses skill but it fails!");
                else
                {
                    //Donger.SendMessages(dongerName + " uses skill!");
                    dong.skill(dong);
                }
                dong.skillUses--;
            }
           
                int strike = SeijaHelper.RandomProvider.GetThreadRandom().Next(0, 100);
                if (strike < dong.acurityMod)
                {
                    int damage = SeijaHelper.RandomProvider.GetThreadRandom().Next(3, 10);
                    //damage=?;
                    damage = (int)(damage * dong.damageMod);
                    if (dong.enemy.armor == Donger.defArmor)
                        Donger.SendMessages(dong.DongerInfo + " attacks " + dong.enemy.DongerInfo + " and deals " + damage.ToString() + " damage");
                    else
                        Donger.SendMessages(dong.DongerInfo + " attacks " + dong.enemy.DongerInfo + " and deals " + ((int)(damage / dong.enemy.armor)).ToString() + " damage");
                    dong.enemy.TakeDamage(damage);
                }
                else
                    Donger.SendMessages(dong.DongerInfo + " misses!");
        }
        public static void DeafaultAttack(Donger dong)
        {
            foreach (Effect f in dong.effects)
            {
                f.Execute(dong);
            }
            dong.effects.RemoveAll(x => x.Timer <= 0);

            //if (dong.effectTimer > 0 && dong.effect != Donger.emtySkill)
            //{
            //    dong.effect(dong);
            //    dong.effectTimer--;
            //}
            int skillstrike = SeijaHelper.RandomProvider.GetThreadRandom().Next(0, 100);
            if (skillstrike < dong.skillChance && dong.skillUses > 0)
            {
                if (dong.skill == Donger.emtySkill)
                    Donger.SendMessages("\t" + dong.DongerInfo + " uses skill but it fails!");
                else
                {
                    //Donger.SendMessages(dongerName + " uses skill!");
                    dong.skill(dong);
                }
                dong.skillUses--;
            }

            else
            {
                int strike = SeijaHelper.RandomProvider.GetThreadRandom().Next(0, 100);
                if (strike < dong.acurityMod)
                {
                    int damage = SeijaHelper.RandomProvider.GetThreadRandom().Next(3, 10);
                    //damage=?;
                    damage = (int)(damage * dong.damageMod);
                    if (dong.enemy.armor == Donger.defArmor)
                        Donger.SendMessages(dong.DongerInfo + " attacks " + dong.enemy.DongerInfo + " and deals " + damage.ToString() + " damage");
                    else
                        Donger.SendMessages(dong.DongerInfo + " attacks " + dong.enemy.DongerInfo + " and deals " + ((int)(damage / dong.enemy.armor)).ToString() + " damage");
                    dong.enemy.TakeDamage(damage);
                }
                else
                    Donger.SendMessages(dong.DongerInfo + " misses!");
            }
        }

        public static void AntimageCurseAttack(Donger dong)
        {
            foreach (Effect f in dong.effects)
            {
                f.Execute(dong);
            }
            dong.effects.RemoveAll(x => x.Timer <= 0);
            int skillstrike = SeijaHelper.RandomProvider.GetThreadRandom().Next(0, 100);
            if (skillstrike < dong.skillChance && dong.skillUses > 0)
            {
                if (dong.skill == Donger.emtySkill)
                    Donger.SendMessages("\t" + dong.DongerInfo + " uses skill but it fails!");
                else
                {
                    dong.skill(dong);
                    Donger.SendMessages("\t" + dong.enemy.SkillMessage("Such sorcery shall not prevail"));
                    dong.TakeDamageFromSkill(10);

                }
                dong.skillUses--;
            }

            else
            {
                int strike = SeijaHelper.RandomProvider.GetThreadRandom().Next(0, 100);
                if (strike < dong.acurityMod)
                {
                    int damage = SeijaHelper.RandomProvider.GetThreadRandom().Next(3, 10);
                    damage = (int)(damage * dong.damageMod);
                    if (dong.enemy.armor == Donger.defArmor)
                        Donger.SendMessages(dong.DongerInfo + " attacks " + dong.enemy.DongerInfo + " and deals " + damage.ToString() + " damage");
                    else
                        Donger.SendMessages(dong.DongerInfo + " attacks " + dong.enemy.DongerInfo + " and deals " + ((int)(damage / dong.enemy.armor)).ToString() + " damage");
                    dong.enemy.TakeDamage(damage);
                }
                else
                    Donger.SendMessages(dong.DongerInfo + " misses!");
            }
        }

        public static void LewdAttack(Donger dong)
        {
            foreach (Effect f in dong.effects)
            {
                f.Execute(dong);
            }
            dong.effects.RemoveAll(x => x.Timer <= 0);

            int skillstrike = SeijaHelper.RandomProvider.GetThreadRandom().Next(0, 100);
            if (skillstrike < dong.skillChance && dong.skillUses > 0)
            {
                if (dong.skill == Donger.emtySkill)
                    Donger.SendMessages("\t" + dong.DongerInfo + " uses skill but it fails!");
                else
                {
                    dong.skill(dong);
                }
                dong.skillUses--;
            }
            else
                Donger.SendMessages(dong.DongerInfo + " is watching carefully!");
        }

        public static void FrozenAttack(Donger dong)
        {
            foreach (Effect f in dong.effects)
            {
                f.Execute(dong);
            }
            dong.effects.RemoveAll(x => x.Timer <= 0);
            //if (dong.effectTimer > 0 && dong.effect != Donger.emtySkill)
            //{
            //    dong.effect(dong);
            //    dong.effectTimer--;
            //}
            int skillstrike = SeijaHelper.RandomProvider.GetThreadRandom().Next(0, 100);
            if (skillstrike < dong.skillChance && dong.skillUses > 0)
            {
                if (dong.skill == Donger.emtySkill)
                    Donger.SendMessages("\t" + dong.DongerInfo + " uses skill but it fails!");
                else
                {
                    //Donger.SendMessages(dongerName + " uses skill!");
                    dong.skill(dong);
                }
                dong.skillUses--;
            }
            else
                Donger.SendMessages("\t" + dong.DongerInfo + " cant attack because he is frozen!");

        }
            

    }


}