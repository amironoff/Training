using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextStatistics.Core;

namespace TextStatistics.Tests.Unit
{
    [TestClass]
    public class SorencenDiceCoefficientTests
    {
        [TestMethod]
        public void GivenTwoSimilarStrings_WhenICalculateDiceCoeffiecient_ThenIGetTheSimilarityIndex()
        {
            //Arrange
            var text1 = "The quickest way to double your money is to fold it over and put it back in your pocket";
            var text2 = "The fastest way to double your money is to fold it over and put it back in your pocket";
            //Act
            var result = text1.ToCharacterShingles().GetSorensenDiceCoefficientFor(text2.ToCharacterShingles());
            //Assert
            Assert.AreEqual(0.93, result, 0.015);
        }

        [TestMethod]
        public void GivenTwoDistinctStrings_WhenICalculateDiceCoeffiecient_ThenIGetTheSimilarityIndex()
        {
            //Arrange
            var text1 = "The quickest way to double your money is to fold it over and put it back in your pocket";
            var text2 = "Even if you’re on the right track, you’ll get run over if you just sit there";
            //Act
            var result = text1.ToCharacterShingles().GetSorensenDiceCoefficientFor(text2.ToCharacterShingles());
            //Assert
            Assert.AreEqual(0.35, result, 0.015);
        }

    }
}
