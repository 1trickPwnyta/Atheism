using Atheism.Discovery;
using RimWorld;
using Verse;

namespace Atheism.Scripture
{
    public class ScriptureDiscoverySource : IDiscoverySource
    {
        private Pawn reader;
        private Book book;

        public ScriptureDiscoverySource(Pawn reader, Book book)
        {
            this.reader = reader;
            this.book = book;
        }

        public TaggedString GetDiscoveredDescription(Precept precept)
        {
            return "Atheism_ScriptureDiscoveryDiscoveredDesc".Translate(reader.Named("READER"), precept.Named("PRECEPT"), book.Named("BOOK"));
        }

        public TaggedString GetProgressDescription(Precept precept)
        {
            return "Atheism_ScriptureDiscoveryProgressDesc".Translate(reader.Named("READER"), precept.Named("PRECEPT"), book.Named("BOOK"));
        }

        public LookTargets GetLookTargets()
        {
            return reader;
        }
    }
}
