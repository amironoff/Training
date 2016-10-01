using System;
using System.Collections.Generic;
using System.Linq;
using TextStatistics.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TextStatistics.Tests.Unit
{
    [TestClass]
    public class ShinglerTests
    {
        [TestMethod]
        public void ShouldReturnCharacterShingles()
        {
            // arrange
            string testString = "abcde";
            int shingleSize = 3;
            int shingleOverlap = 2;
            HashSet<string> expectedResult = new HashSet<string>()
                                             {
                                                 "abc",
                                                 "bcd",
                                                 "cde"
                                             };

            // act
            var testResult = testString.ToCharacterShingles(shingleSize, shingleOverlap);

            // assert
            AssertCollectionsEqual(expectedResult, testResult);
        }

        [TestMethod]
        public void ShouldReturnTermShingles()
        {
            // arrange
            string testString = "Polkovniku nikto ne pishet. Polkovnika nikto ne zhdet.";
            int shingleSize = 3;
            int shingleOverlap = 1;
            HashSet<string> expectedResult = new HashSet<string>()
                                             {
                                                 "Polkovniku nikto ne",
                                                 "ne pishet Polkovnika",
                                                 "Polkovnika nikto ne",
                                             };

            // act
            var testResult = testString.ToTermShingles(shingleSize, shingleOverlap);

            // assert
            AssertCollectionsEqual(expectedResult, testResult);
        }


        private static void AssertCollectionsEqual<T>
            (IEnumerable<T> collectionOne, IEnumerable<T> collectionTwo)
        {
            if (collectionOne == null || collectionTwo == null)
                Assert.Fail();
            if(collectionOne.Count() != collectionTwo.Count())
                Assert.Fail();
            for (int i = 0; i < collectionOne.Count(); i++)
            {
                if(!collectionOne.ElementAt(i).Equals(collectionTwo.ElementAt(i)))
                    Assert.Fail();
            }
        }
    }
}
