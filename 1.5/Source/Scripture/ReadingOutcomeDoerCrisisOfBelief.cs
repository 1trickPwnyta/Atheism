using RimWorld;
using Verse;

namespace Atheism.Scripture
{
    public class ReadingOutcomeDoerCrisisOfBelief : BookOutcomeDoer
    {
        private const float baseCertaintyLossPerHour = 0.03f;
        private const float baseCrisisChancePerHour = 0.01f;

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
            return false;
        }

        public override void OnReadingTick(Pawn reader, float factor)
        {
            ReadingOutcomeDoerMakeDiscoveryProgress doer;
            if (((Book)Parent).BookComp.TryGetDoer<ReadingOutcomeDoerMakeDiscoveryProgress>(out doer) && doer.Subject != null)
            {
                reader.SetLastReadScriptureIdeo(doer.Subject);

                if (Rand.MTBEventOccurs(1f, 2500f, 1f))
                {
                    if (reader.Ideo != doer.Subject)
                    {
                        Precept_Role role = reader.Ideo.GetRole(reader);
                        if (reader.ideo.IdeoConversionAttempt(baseCertaintyLossPerHour * GetQualityMultiplier(), doer.Subject))
                        {
                            if (PawnUtility.ShouldSendNotificationAbout(reader))
                            {
                                string letterText = "Atheism_LetterConvertFromScripture".Translate(reader.Named("PAWN"), doer.Subject.Named("IDEO"), Parent.Named("BOOK"));
                                if (role != null)
                                {
                                    letterText += "\n\n" + "LetterRoleLostLetterIdeoChangedPostfix".Translate(reader.Named("PAWN"), role.Named("ROLE"), doer.Subject.Named("OLDIDEO")).Resolve();
                                }
                                Find.LetterStack.ReceiveLetter("Atheism_LetterLabelConvertFromScripture".Translate(reader.LabelShortCap), letterText, LetterDefOf.NeutralEvent, reader);
                            }
                        }
                    }
                    else
                    {
                        reader.ideo.OffsetCertainty(baseCertaintyLossPerHour * GetQualityMultiplier());
                    }
                }

                if (reader.Ideo != doer.Subject && Rand.MTBEventOccurs(1f / baseCrisisChancePerHour / GetQualityMultiplier(), 2500f, 1f))
                {
                    reader.mindState.mentalBreaker.TryDoMentalBreak("Atheism_ReadingOutcomeCrisisOfBelief".Translate(reader.Named("PAWN"), Parent.Named("BOOK")), DefDatabase<MentalBreakDef>.GetNamed("IdeoChange"));
                }
            }
        }
    }
}
