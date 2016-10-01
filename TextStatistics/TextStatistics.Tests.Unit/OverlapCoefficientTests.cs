using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextStatistics.Core;

namespace TextStatistics.Tests.Unit
{
    [TestClass]
    public class OverlapCoefficientWithTests
    {

        [TestMethod]
        public void GivenTwoSimilarStrings_WhenICalculateOverlapCoeffiecient_ThenIGetTheSimilarityIndex()
        {
            //Arrange
            var text1 = "The quickest way to double your money is to fold it over and put it back in your pocket";
            var text2 = "The fastest way to double your money is to fold it over and put it back in your pocket";
            //Act
            var result = text1.ToCharacterShingles().GetOverlapCoefficientWith(text2.ToCharacterShingles());
            //Assert
            Assert.AreEqual(0.95, result, 0.01);
        }

        [TestMethod]
        public void GivenTwoSimilarStringsWhereOneIsASubsetOfTheOther_WhenICalculateOverlapCoeffiecient_ThenIGetTheSimilarityIndex()
        {
            //Arrange
            var text1 = "The quickest way to double your money is to fold it over and put it back in your pocket";
            var text2 = "The quickest way to double your money is to put them back in your pocket";
            //Act
            var result = text1.ToCharacterShingles().GetOverlapCoefficientWith(text2.ToCharacterShingles());
            //Assert
            Assert.AreEqual(0.96, result, 0.01);
        }

        [TestMethod]
        public void GivenTwoDistinctStrings_WhenICalculateOverlapCoeffiecient_ThenIGetTheSimilarityIndex()
        {
            //Arrange
            var text1 = "The quickest way to double your money is to fold it over and put it back in your pocket";
            var text2 = "Even if you’re on the right track, you’ll get run over if you just sit there";
            //Act
            var result = text1.ToCharacterShingles().GetOverlapCoefficientWith(text2.ToCharacterShingles());
            //Assert
            Assert.AreEqual(0.4, result, 0.01);
        }
    }
}
