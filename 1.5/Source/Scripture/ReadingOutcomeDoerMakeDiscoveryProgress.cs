using Atheism.Discovery;
using Atheism.Ideo;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.Grammar;

namespace Atheism.Scripture
{
    public class ReadingOutcomeDoerMakeDiscoveryProgress : BookOutcomeDoerDynamic
    {
        private const float baseHoursToRead = 10f;

        private RimWorld.Ideo subject;
        private float readingProgress = 0f;

        public RimWorld.Ideo Subject
        {
            get
            {
                return subject;
            }
        }

        private float GetQualityMultiplier()
        {
            switch (Quality)
            {
                case QualityCategory.Awful: return 0.75f;
                case QualityCategory.Poor: return 0.9f;
                case QualityCategory.Good: return 1.1f;
                case QualityCategory.Excellent: return 1.2f;
                case QualityCategory.Masterwork: return 1.35f;
                case QualityCategory.Legendary: return 1.5f;
                default: return 1f;
            }
        }

        public override bool DoesProvidesOutcome(Pawn reader)
        {
            return readingProgress < 1f && subject != null && subject.UndiscoveredPrecepts().Any();
        }

        public override List<Rule> GetDynamicRules()
        {
            List<Rule> rules = new List<Rule>();
            rules.Add(new Rule_String("ideo_name", subject.name));
            if (subject.foundation is IdeoFoundation_Deity) 
            {
                IdeoFoundation_Deity.Deity deity;
                if (((IdeoFoundation_Deity)subject.foundation).DeitiesListForReading.TryRandomElement(out deity)) 
                {
                    rules.Add(new Rule_String("deity_name", deity.name));
                }
            }
            return rules;
        }

        public override IDictionary<string, string> GetConstants()
        {
            Dictionary<string, string> constants = new Dictionary<string, string>();
            constants["has_deity"] = "false";
            if (subject.foundation is IdeoFoundation_Deity)
            {
                if (((IdeoFoundation_Deity)subject.foundation).DeitiesListForReading.Any())
                {
                    constants["has_deity"] = "true";
                }
            }
            return constants;
        }

        public override void OnBookGenerated(Pawn author = null)
        {
            if (!Ideo.IdeoUtility.GetAdversarialIdeos().Where(i => i.UndiscoveredPrecepts().Any()).TryRandomElement(out subject))
            {
                subject = Find.IdeoManager.IdeosListForReading.Where(i => !i.IsAtheism()).RandomElement();
            }
        }

        public override void Reset()
        {
            subject = null;
        }

        public override void OnReadingTick(Pawn reader, float factor)
        {
            if (readingProgress < 1f && subject != null && reader.Ideo != subject)
            {
                readingProgress += 1f / 2500 / baseHoursToRead * GetQualityMultiplier();
                if (readingProgress >= 1f)
                {
                    Precept precept;
                    if (subject.UndiscoveredPrecepts().TryRandomElement(out precept))
                    {
                        precept.IncrementDiscoveryProgress(new ScriptureDiscoverySource(reader, (Book)Parent));
                    }
                }
            }
        }

        public override IEnumerable<Dialog_InfoCard.Hyperlink> GetHyperlinks()
        {
            if (subject != null)
            {
                yield return new Dialog_InfoCard.Hyperlink(subject);
            }
        }

        public override void PostExposeData()
        {
            Scribe_References.Look(ref subject, "subject");
            Scribe_Values.Look(ref readingProgress, "readingProgress");
        }
    }
}
