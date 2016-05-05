using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Shingle;
using Lucene.Net.Analysis.Standard;
using System.IO;

namespace SitecoreCookbook.Search
{
    // Give Reference to Lucene.Net.Contrib.Analyzers.dll
    public class NGramAnalyzer : Analyzer
    {
        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            TokenStream ts = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30).ReusableTokenStream(fieldName, reader);
            ts = new LowerCaseFilter(ts);
			int shinglesSize = 3;
            ts = new ShingleFilter(ts, shinglesSize);
			
			// Uncomment below to use StopFilter that will skip few predefined set of words.
			// ts = new StopFilter(true, ts, StopAnalyzer.ENGLISH_STOP_WORDS_SET);
            return ts;
        }
    }
}
