using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextStatistics.Core;

namespace TextStatistics.Tests.Unit
{
    [TestClass]
    public class JaccardIndexTests
    {
        [TestMethod]
        public void GivenTwoSimilarStrings_WhenIComputeJaccardIndex_ThenCanConputeSimilarityIndex()
        {
            // arrange
            var text1 = "The quickest way to double your money is to fold it over and put it back in your pocket";
            var text2 = "The fastest way to double your money is to fold it over and put it back in your pocket";

            // act
            var result = text1.ToCharacterShingles().GetJaccardIndexFor(text2.ToCharacterShingles());

            // assert
            Assert.AreEqual(0.89, result, 0.01);

        }

        [TestMethod]
        public void GivenTwoSimilarStrings_WhenICalculateJaccardDistance_ThenIGetTheSimilarityIndex()
        {
            //Arrange
            var text1 = "The quickest way to double your money is to fold it over and put it back in your pocket";
            var text2 = "The fastest way to double your money is to fold it over and put it back in your pocket";
            //Act
            var result = text1.ToCharacterShingles().GetJaccardDistanceFrom(text2.ToCharacterShingles());
            //Assert
            Assert.AreEqual(0.11, result, 0.01);
        }


        [TestMethod]
        public void GivenTwoDistinctStrings_WhenICalculateJaccardIndex_ThenIGetTheSimilarityIndex()
        {
            //Arrange
            var text1 = "The quickest way to double your money is to fold it over and put it back in your pocket";
            var text2 = "Even if you’re on the right track, you’ll get run over if you just sit there";
            //Act
            var result = text1.ToCharacterShingles().GetJaccardIndexFor(text2.ToCharacterShingles());
            //Assert
            Assert.AreEqual(0.21, result, 0.01);
        }

        [TestMethod]
        public void GivenTwoDistinctStrings_WhenICalculateJaccardDistance_ThenIGetTheSimilarityIndex()
        {
            //Arrange
            var text1 = "The quickest way to double your money is to fold it over and put it back in your pocket";
            var text2 = "Even if you’re on the right track, you’ll get run over if you just sit there";
            //Act
            var result = text1.ToCharacterShingles().GetJaccardDistanceFrom(text2.ToCharacterShingles());
            //Assert
            Assert.AreEqual(0.78, result, 0.01);
        }


    }
}
