﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearch.Services
{
    public class GoogleServicesKeys
    {
        //Setup a custom search with Image enabled with a website of *: https://cse.google.com/cse/
        public const string APIKey = "__ENTER YOUR KEY__";
        public const string CX = "__ENTER YOUR CX__";
    }

    public class CognitiveServicesKeys
    {
        //Setup a Emotion API key for Cognitive Services at: https://www.microsoft.com/cognitive-services/
        public const string Emotion = "872dac857e1a47459896f833bad2325b";

		//Setup a Bing Search API key from: http://www.microsoft.com/cognitive-services
		public const string BingSearch = "995ef1807a094cedb581f9ccc4c61f9d";

	} 
}
