﻿using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace OpenAIONDPS
{
    public static class AION
    {
        public class LogPattern
        {
            private const string SkillNameRegexPattern                    = @"(?<SkillName>[\p{IsKatakana}：\s]+)";
            private const string SourceNameRegexPattern                   = @"(?<SourceName>[^、]+)";
            private const string SourceNameReplacedMemberNameRegexPattern = @"(?<SourceName>[[[MemberName]]])";
            private const string TargetNameRegexPattern                   = @"(?<TargetName>[^、]+)";
            private const string DamageRegexPattern                       = @"(?<Damage>[0-9,]+)";

            /// <summary>
            /// 行のパターン
            /// </summary>
            public const string LineRegex = @"^(20[0-9][0-9]\.[0-9][0-9]\.[0-9][0-9]\s[0-9][0-9]:[0-9][0-9]:[0-9][0-9])\s:\s(.*。)";

            /// <summary>
            /// クリティカルヒットのパターン
            /// </summary>
            public const string CriticalHitRegexPattern = "^クリティカルヒット！(.*)$";

            /* 通常攻撃 */

            /// <summary>
            ///  通常攻撃のダメージのパターン(自分)
            /// </summary>
            public const string AttackSimpleDamageWithoutSourceName = "^"+ TargetNameRegexPattern + "に" + DamageRegexPattern + "の(致命的な|)ダメージを与えました。";

            /// <summary>
            /// 通常攻撃のダメージのパターン(他人)(要SourceNameの置換)
            /// </summary>
            public const string AttackSimpleDamageWithSourceNameReplacedMemberName = "^" + SourceNameReplacedMemberNameRegexPattern + "が" + TargetNameRegexPattern + "に" + DamageRegexPattern + "のダメージを与えました。";

            /// <summary>
            /// 通常攻撃のダメージのパターン(他人)
            /// </summary>
            public const string AttackSimpleDamageWithSourceName = "^" + SourceNameRegexPattern + "が" + TargetNameRegexPattern + "に" + DamageRegexPattern + "のダメージを与えました。";

            /* スキル攻撃 */

            /// <summary>
            /// スキル攻撃のダメージのパターン
            /// </summary>
            public const string AttackSkillDamageWithoutSourceName = "^" + SkillNameRegexPattern + "の効果により、" + TargetNameRegexPattern + "に" + DamageRegexPattern + "のダメージを与えました。";




        }

        public enum JobType { None, Sword, Shield, Shadow, Bow, Spell, Spirit, Cure, Chant, Bullet, Gia, Melody };

        public enum SkillType { Normal, DelayDamage, Dot, Summon, EffectDamage, Others };

        public static string GetJobName(JobType Type)
        {
            switch (Type)
            {
                case JobType.Sword:
                    return "ソード";
                case JobType.Shield:
                    return "シールド";
                case JobType.Shadow:
                    return "シャドウ";
                case JobType.Bow:
                    return "ボウ";
                case JobType.Spell:
                    return "スペル";
                case JobType.Spirit:
                    return "スピリット";
                case JobType.Cure:
                    return "キュア";
                case JobType.Chant:
                    return "チャント";
                case JobType.Bullet:
                    return "バレット";
                case JobType.Gia:
                    return "ギア";
                case JobType.Melody:
                    return "メロディ";
                case JobType.None:
                    return "未選択";
                default:
                    return "";
            }
        }

        public static JobType GetJobType(string Name)
        {
            if (String.IsNullOrEmpty(Name))
            {
                return JobType.None;
            }
            else if (Name.Equals("ソード"))
            {
                return JobType.Sword;
            }
            else if (Name.Equals("シールド"))
            {
                return JobType.Shield;
            }
            else if (Name.Equals("シャドウ"))
            {
                return JobType.Shadow;
            }
            else if (Name.Equals("ボウ"))
            {
                return JobType.Bow;
            }
            else if (Name.Equals("スペル"))
            {
                return JobType.Spell;
            }
            else if (Name.Equals("スピリット"))
            {
                return JobType.Spirit;
            }
            else if (Name.Equals("キュア"))
            {
                return JobType.Cure;
            }
            else if (Name.Equals("チャント"))
            {
                return JobType.Chant;
            }
            else if (Name.Equals("バレット"))
            {
                return JobType.Bullet;
            }
            else if (Name.Equals("ギア"))
            {
                return JobType.Gia;
            }
            else if (Name.Equals("メロディ"))
            {
                return JobType.Melody;
            }
            else if (Name.Equals("未選択"))
            {
                return JobType.None;
            }
            else
            {
                return JobType.None;
            }
        }

        public class JobSet
        {
            private JobType _Type = JobType.None;
            private string _Name = "";

            public JobSet(JobType Type, string Name)
            {
                this._Type = Type;
                this._Name = Name;
            }

            public JobType Type
            {
                get
                {
                    return this._Type;
                }
            }

            public string Name
            {
                get
                {
                    return this._Name;
                }
            }

        }

        public class Skill
        {
            public string Name = "";
            public JobType Job = JobType.None;
            public SkillType SkillType = SkillType.Others;

            public Skill(string Name, JobType Job, SkillType SkillType)
            {
                this.Name = Name;
                this.Job = Job;
                this.SkillType = SkillType;
            }
        }

        public static Dictionary<string, AION.Skill> GetSkillList()
        {
            Dictionary<string, AION.Skill> SkillList = new Dictionary<string, Skill>();
            string SkillName = "";

            /**************************************************************************************************************************************/
            /* シールド ***************************************************************************************************************************/
            /**************************************************************************************************************************************/
            // エフェクトダメージ
            SkillName = "アップセット エフェクト";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Shield, AION.SkillType.EffectDamage));


            /**************************************************************************************************************************************/
            /* ソード *****************************************************************************************************************************/
            /**************************************************************************************************************************************/


            /**************************************************************************************************************************************/
            /* シャドウ ***************************************************************************************************************************/
            /**************************************************************************************************************************************/
            // ドット
            SkillName = "エクターミネーション";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Shadow, AION.SkillType.Dot));
            SkillName = "デッドリー ポイズン サドン アタック";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Shadow, AION.SkillType.Dot));
            SkillName = "バック ダメージ";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Shadow, AION.SkillType.Dot));

            // エフェクトダメージ
            SkillName = "エクスプローシブ アプライ エフェクト";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Shadow, AION.SkillType.EffectDamage));
            SkillName = "スティレット コミットメント アディショナル エフェクト";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Shadow, AION.SkillType.EffectDamage));
            SkillName = "ドラスティック ポイズン エフェクト";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Shadow, AION.SkillType.EffectDamage));


            /**************************************************************************************************************************************/
            /* ボウ *******************************************************************************************************************************/
            /**************************************************************************************************************************************/
            // エフェクトダメージ
            SkillName = "ヴェンジフル トラップ エフェクト";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Bow, AION.SkillType.EffectDamage));
            SkillName = "ダスト トラップ エフェクト";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Bow, AION.SkillType.EffectDamage));
            SkillName = "バンプ トラップ エフェクト";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Bow, AION.SkillType.EffectDamage));
            /* 要調査 */
            SkillName = "バースト トラップ エフェクト";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Bow, AION.SkillType.EffectDamage));
            SkillName = "クレアポイアンス トラップ エフェクト";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Bow, AION.SkillType.EffectDamage));


            /**************************************************************************************************************************************/
            /* スペル *****************************************************************************************************************************/
            /**************************************************************************************************************************************/
            // ディレイダメージ
            SkillName = "ボルカニック ブローアウト";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Spell, AION.SkillType.DelayDamage));
            SkillName = "ボルカニック ビック イラプション";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Spell, AION.SkillType.DelayDamage));

            // ドット
            SkillName = "バーン";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Spell, AION.SkillType.Dot));
            SkillName = "ヒート サモン";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Spell, AION.SkillType.Dot));

            // サモン
            SkillName = "タイフーン サモン";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Spell, AION.SkillType.Summon));


            /**************************************************************************************************************************************/
            /* スピリット *************************************************************************************************************************/
            /**************************************************************************************************************************************/
            // ドット
            SkillName = "イロージョン";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Spirit, AION.SkillType.Dot));
            SkillName = "ワイドエリア イロージョン";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Spirit, AION.SkillType.Dot));
            SkillName = "アース チェーン";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Spirit, AION.SkillType.Dot));
            SkillName = "マジック エクスプロージョン";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Spirit, AION.SkillType.Dot));
            SkillName = "アンガー サイクロン";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Spirit, AION.SkillType.Dot));
            SkillName = "カース クラウド";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Spirit, AION.SkillType.Dot));
            SkillName = "サモン：ウインド エネルギー";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Spirit, AION.SkillType.Dot));
            SkillName = "サモン：サイクロン エネルギー";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Spirit, AION.SkillType.Dot));

            // サモン
            SkillName = "ファイア スピリット";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Spirit, AION.SkillType.Summon));
            SkillName = "ウォーター スピリット";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Spirit, AION.SkillType.Summon));
            SkillName = "ウインド スピリット";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Spirit, AION.SkillType.Summon));
            SkillName = "グラウンド スピリット";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Spirit, AION.SkillType.Summon));
            SkillName = "タイフーン スピリット";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Spirit, AION.SkillType.Summon));


            /**************************************************************************************************************************************/
            /* キュア *****************************************************************************************************************************/
            /**************************************************************************************************************************************/
            // ドット
            SkillName = "パニッシュ";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Cure, AION.SkillType.Dot));
            SkillName = "アース アンガー";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Cure, AION.SkillType.Dot));
            SkillName = "ウイークン ブランド";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Cure, AION.SkillType.Dot));
            SkillName = "ペイン サクセッシブ";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Cure, AION.SkillType.Dot));
            SkillName = "ディストラクション ボイス";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Cure, AION.SkillType.Dot));

            // サモン
            SkillName = "ディシプリン エネルギー";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Cure, AION.SkillType.Summon));
            SkillName = "ノーブル エネルギー";
            SkillList.Add(SkillName, new Skill(SkillName, AION.JobType.Cure, AION.SkillType.Summon));


            /**************************************************************************************************************************************/
            /* チャント ***************************************************************************************************************************/
            /**************************************************************************************************************************************/
            // ドット
            SkillName = "マウンテン クラッシュ";
            SkillList.Add(SkillName, new AION.Skill(SkillName, AION.JobType.Chant, AION.SkillType.Dot));

            // エフェクトダメージ
            SkillName = "ウインド ブレス";
            SkillList.Add(SkillName, new AION.Skill(SkillName, AION.JobType.Chant, AION.SkillType.EffectDamage));
            SkillName = "アース プロミス エフェクト";
            SkillList.Add(SkillName, new AION.Skill(SkillName, AION.JobType.Chant, AION.SkillType.EffectDamage));
            SkillName = "アース コミットメント エフェクト";
            SkillList.Add(SkillName, new AION.Skill(SkillName, AION.JobType.Chant, AION.SkillType.EffectDamage));


            /**************************************************************************************************************************************/
            /* バレット ***************************************************************************************************************************/
            /**************************************************************************************************************************************/
            // ドット
            SkillName = "ホーム ペネトレート";
            SkillList.Add(SkillName, new AION.Skill(SkillName, AION.JobType.Bullet, AION.SkillType.Dot));


            /**************************************************************************************************************************************/
            /* ギア *******************************************************************************************************************************/
            /**************************************************************************************************************************************/
            // ディレイダメージ
            SkillName = "チャージ ファイヤ";
            SkillList.Add(SkillName, new AION.Skill(SkillName, AION.JobType.Gia, AION.SkillType.DelayDamage));


            /**************************************************************************************************************************************/
            /* メロディ ***************************************************************************************************************************/
            /**************************************************************************************************************************************/
            // ディレイダメージ
            SkillName = "ダメージ エコー";
            SkillList.Add(SkillName, new AION.Skill(SkillName, AION.JobType.Melody, AION.SkillType.DelayDamage));
            SkillName = "シャープ フリカティブ";
            SkillList.Add(SkillName, new AION.Skill(SkillName, AION.JobType.Melody, AION.SkillType.DelayDamage));

            // ドット
            SkillName = "モスキー ラプソディ";
            SkillList.Add(SkillName, new AION.Skill(SkillName, AION.JobType.Melody, AION.SkillType.Dot));


            return SkillList;
        }
    }
}
