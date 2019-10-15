//-----------------------------------------------------------------------
// <copyright file="CipherTests.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AlphabetCipher.Tests
{
    using Xunit;

    public class CipherTests
    {
        private Cipher cipher = new Cipher();

        [Theory]
        [InlineData("vigilance", "meetmeontuesdayeveningatseven", "hmkbxebpxpmyllyrxiiqtoltfgzzv")]
        [InlineData("scones", "meetmebythetree", "egsgqwtahuiljgs")]
        public void Encode(string key, string clearText, string expectedCipherText)
        {
            Assert.Equal(this.cipher.Encode(key, clearText), expectedCipherText);
        }

        [Theory]
        [InlineData("vigilance", "hmkbxebpxpmyllyrxiiqtoltfgzzv", "meetmeontuesdayeveningatseven")]
        [InlineData("scones", "egsgqwtahuiljgs", "meetmebythetree")]
        public void Decode(string key, string cipherText, string expectedClearText)
        {
            Assert.Equal(this.cipher.Decode(key, cipherText), expectedClearText);
        }

        [Theory]
        [InlineData("opkyfipmfmwcvqoklyhxywgeecpvhelzg", "thequickbrownfoxjumpsoveralazydog", "vigilance")]
        [InlineData("hcqxqqtqljmlzhwiivgbsapaiwcenmyu", "packmyboxwithfivedozenliquorjugs", "scones")]
        [InlineData("hfnlphoontutufa", "hellofromrussia", "abcabcx")]
        public void Decipher(string cipherText, string clearText, string expectedKey)
        {
            Assert.Equal(this.cipher.Decipher(cipherText, clearText), expectedKey);
        }
    }
}
