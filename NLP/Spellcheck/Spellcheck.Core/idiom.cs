using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

interface ICorpus
{
    int Rank(string word);
    bool Contains(string word);
    IEnumerable<string> Known(IEnumerable<string> words);
}

static class StringExtensions
{
    public static string From(this string str, int n)
    {
        if(str == null) return null;

        var len = str.Length;

        if (n >= len) return "";
        if (n == 0 || -n >= len) return str;

        return str.Substring((len + n) % len, (len - n) % len);
    }

    public static string To(this string str, int n)
    {
        if (str == null) return null;

        var len = str.Length;

        if(n == 0 || -n >= len) return "";
        if (n >= len) return str;

        return str.Substring(0, (len + n) % len);
    }
}

class Corpus : ICorpus
{
    private readonly Dictionary<string, int> rankings;

    private static IEnumerable<string> ExtractWords(string str)
    {
        return Regex.Matches(str, "[a-z]+", RegexOptions.IgnoreCase)
                    .Cast<Match>()
                    .Select(m => m.Value);
    }

    public Corpus(string sample) : this(ExtractWords(sample)) {}

    public Corpus(IEnumerable<string> sample)
    {
        rankings = sample.Select(w => w.ToLower())
                         .GroupBy(w => w)
                         .ToDictionary(w => w.Key, w => w.Count());
    }

    public int Rank(string word)
    {
        int ret;
        return rankings.TryGetValue(word, out ret) ? ret : 1;
    }

    public bool Contains(string word)
    {
        return rankings.ContainsKey(word);
    }

    public IEnumerable<string> Known(IEnumerable<string> words)
    {
        return words.Where(Contains);
    }
}

class SpellCorrect
{
    private readonly ICorpus corpus;

    public SpellCorrect(ICorpus corpus)
    {
        this.corpus = corpus;
    }

    private const string Alphabet = "abcdefghijklmnopqrstuvwxyz";

    private IEnumerable<string> Edits(string word)
    {
        var splits     = from i in Enumerable.Range(0, word.Length)
                         select new {a = word.To(i), b = word.From(i)};
        var deletes    = from s in splits
                         where s.b != "" // Guaranteed not null
                         select s.a + s.b.From(1);
        var transposes = from s in splits
                         where s.b.Length > 1
                         select s.a + s.b[1] + s.b[0] + s.b.From(2);
        var replaces   = from s in splits
                         from c in Alphabet
                         select s.a + c + s.b.From(1);
        var inserts    = from s in splits
                         from c in Alphabet
                         select s.a + c + s.b;

        return deletes
        .Union(transposes)
        .Union(replaces)
        .Union(inserts);
    }

    private IEnumerable<string> Corrections(string word)
    {
        if (corpus.Contains(word)) return new[] {word};

        var edits = Edits(word);
        
        var knownEdits = corpus.Known(edits);
        if (knownEdits.Any()) return knownEdits;

        var secondPass = from e1 in edits
                         from e2 in Edits(e1)
                         where corpus.Contains(e2)
                         select e2;

        return secondPass.Any() ? secondPass : new[] { word };
    }

    public string Correct(string word)
    {
        var corrections = Corrections(word).OrderByDescending(corpus.Rank);
        
        return corrections.First();
    }
}

public static class Idiom
{
    private static SpellCorrect corrector;

    private static void ReadFromStdIn()
    {
        string word;
        while (!string.IsNullOrEmpty(word = (Console.ReadLine() ?? "").Trim()))
        {
            Console.WriteLine(corrector.Correct(word));
        }
    }

    public static void Main(string[] args)
    {
        if (!File.Exists("big.txt"))
        {
            Console.Error.WriteLine("Cannot find big.txt.");
            return;
        }

        var sample = File.ReadAllText("big.txt");
        var corpus = new Corpus(sample);
        corrector = new SpellCorrect(corpus);

        ReadFromStdIn();
    }
}