using System.Buffers;
using TwitchDownloaderCore.Extensions;

namespace TwitchDownloaderCore.Tests.ExtensionTests
{
    // ReSharper disable StringLiteralTypo
    public class StringReplaceAnyTests
    {
        [Fact]
        public void MatchesMultipleStringReplaceUses()
        {
            const string STRING = "SORRY FOR TRAFFIC NaM.";
            const string OLD_CHARS = "FRM";
            const char NEW_CHAR = 'L';

            var replaceResult1 = STRING.Replace(OLD_CHARS[0], NEW_CHAR);
            var replaceResult2 = replaceResult1.Replace(OLD_CHARS[1], NEW_CHAR);
            var replaceResult3 = replaceResult2.Replace(OLD_CHARS[2], NEW_CHAR);

            var replaceAnyResult = replaceResult2.ReplaceAny(SearchValues.Create(OLD_CHARS), NEW_CHAR);

            Assert.Equal(replaceResult3, replaceAnyResult);
        }

        [Fact]
        public void MatchesMultipleStringReplaceUses_Callback()
        {
            const string STRING = "SORRY FOR TRAFFIC NaM.";
            const string OLD_CHARS = "FRM";
            const char NEW_CHAR = 'L';

            var replaceResult1 = STRING.Replace(OLD_CHARS[0], NEW_CHAR);
            var replaceResult2 = replaceResult1.Replace(OLD_CHARS[1], NEW_CHAR);
            var replaceResult3 = replaceResult2.Replace(OLD_CHARS[2], NEW_CHAR);

            var replaceAnyResult = replaceResult2.ReplaceAny(SearchValues.Create(OLD_CHARS), _ => NEW_CHAR);

            Assert.Equal(replaceResult3, replaceAnyResult);
        }

        [Fact]
        public void CorrectlyReplacesAnyCharacter()
        {
            const string STRING = "SORRY FOR TRAFFIC NaM.";
            const string OLD_CHARS = "FRM";
            const char NEW_CHAR = 'L';
            const string EXPECTED = "SOLLY LOL TLALLIC NaL.";

            var result = STRING.ReplaceAny(SearchValues.Create(OLD_CHARS), NEW_CHAR);

            Assert.Equal(EXPECTED, result);
        }

        [Fact]
        public void CorrectlyReplacesAnyCharacter_Callback()
        {
            const string STRING = "SORRY FOR TRAFFIC NaM.";
            const string OLD_CHARS = "FRM";
            const char NEW_CHAR = 'L';
            const string EXPECTED = "SOLLY LOL TLALLIC NaL.";

            var result = STRING.ReplaceAny(SearchValues.Create(OLD_CHARS), _ => NEW_CHAR);

            Assert.Equal(EXPECTED, result);
        }

        [Fact]
        public void ReturnsOriginalString_WhenEmpty()
        {
            const string STRING = "";
            const string OLD_CHARS = "";
            const char NEW_CHAR = 'L';

            var result = STRING.ReplaceAny(SearchValues.Create(OLD_CHARS), NEW_CHAR);

            Assert.Same(STRING, result);
        }

        [Fact]
        public void ReturnsOriginalString_WhenEmpty_Callback()
        {
            const string STRING = "";
            const string OLD_CHARS = "";
            const char NEW_CHAR = 'L';

            var result = STRING.ReplaceAny(SearchValues.Create(OLD_CHARS), _ => NEW_CHAR);

            Assert.Same(STRING, result);
        }

        [Fact]
        public void ReturnsOriginalString_WhenOldCharsNotPresent()
        {
            const string STRING = "SORRY FOR TRAFFIC NaM.";
            const string OLD_CHARS = "PogU";
            const char NEW_CHAR = 'L';

            var result = STRING.ReplaceAny(SearchValues.Create(OLD_CHARS), NEW_CHAR);

            Assert.Same(STRING, result);
        }

        [Fact]
        public void ReturnsOriginalString_WhenOldCharsNotPresent_Callback()
        {
            const string STRING = "SORRY FOR TRAFFIC NaM.";
            const string OLD_CHARS = "PogU";
            const char NEW_CHAR = 'L';

            var result = STRING.ReplaceAny(SearchValues.Create(OLD_CHARS), _ => NEW_CHAR);

            Assert.Same(STRING, result);
        }

        [Fact]
        public void CallbackReturnsOldChars()
        {
            const string STRING = "SORRY FOR TRAFFIC NaM.";
            const string OLD_CHARS = "FRM";
            const char NEW_CHAR = 'L';
            const string EXPECTED = "SOLLY LOL TLALLIC NaL.";

            var result = STRING.ReplaceAny(SearchValues.Create(OLD_CHARS), ch =>
            {
                Assert.Contains(ch, OLD_CHARS);
                return NEW_CHAR;
            });

            Assert.Equal(EXPECTED, result);
        }
    }
}