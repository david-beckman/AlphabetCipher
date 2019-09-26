using Xunit;

namespace AlphabetCipher.Tests
{
    public class CipherTests
    {
        private Cipher cipher = new Cipher();

        [Theory]
        [InlineData("vigilance", "meetmeontuesdayeveningatseven", "hmkbxebpxpmyllyrxiiqtoltfgzzv")]
        [InlineData("scones", "meetmebythetree", "egsgqwtahuiljgs")]
        public void Encode(string key, string clearText, string expectedCipherText)
        {
            Assert.Equal(cipher.Encode(key, clearText), expectedCipherText);
        }

        [Theory]
        [InlineData("vigilance", "hmkbxebpxpmyllyrxiiqtoltfgzzv", "meetmeontuesdayeveningatseven")]
        [InlineData("scones", "egsgqwtahuiljgs", "meetmebythetree")]
        public void Decode(string key, string cipherText, string expectedClearText)
        {
            Assert.Equal(cipher.Decode(key, cipherText), expectedClearText);
        }

        [Theory]
        [InlineData("opkyfipmfmwcvqoklyhxywgeecpvhelzg", "thequickbrownfoxjumpsoveralazydog", "vigilance")]
        [InlineData("hcqxqqtqljmlzhwiivgbsapaiwcenmyu", "packmyboxwithfivedozenliquorjugs", "scones")]
        [InlineData("hfnlphoontutufa", "hellofromrussia", "abcabcx")]
        public void Decipher(string cipherText, string clearText, string expectedKey)
        {
            Assert.Equal(cipher.Decipher(cipherText, clearText), expectedKey);
        }
    }
}
