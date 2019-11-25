using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace HackerNews
{
  class Program
  {
    private static void Main(string[] args)
    {
      string count = String.Empty;
      if (args.Count() == 1)
      {
        count = args[0];
      }
      HelperClass helperClass = new HelperClass();
      int PostsCount = helperClass.IsInputOK(count);
      if (PostsCount == 0)
      {
        Console.WriteLine("Number of posts must be an integer between 0 and 100.");
        Console.ReadLine();
      }
      else
      {
        XDocument xdoc = helperClass.FromHtmlToXDoc("https://news.ycombinator.com/");
        displayResult(PostsCount, xdoc, helperClass);
      }
    }


    private static void displayResult(int NoOfPosts, XDocument x, HelperClass h)
    {


      List<XElement> itemsList = x.Descendants().Where(y => y.HasAttributes && y.Attributes().FirstOrDefault(ya => ya.Name.LocalName == "class" && ya.Value == "athing") != null).Take(NoOfPosts).ToList();
      string id = string.Empty;
      string title;
      string rank;
      string uri;
      string points;
      string author;
      string comments = string.Empty;
      XElement pt;
      XElement ti;
      XElement ra;
      XElement co;
      string s = string.Empty; ;
      XElement nextThree;
      Console.WriteLine("[");
      foreach (XElement element in itemsList)
      {
        nextThree = element.ElementsAfterSelf().First();
        id = element.Attributes().FirstOrDefault(a => a.Name.LocalName == "id").Value;
        ti = element.Descendants().FirstOrDefault(e => e.Name.LocalName == "a" && e.HasAttributes && e.Attributes().FirstOrDefault(a => a.Name.LocalName == "class" && a.Value == "storylink") != null);
        ra = element.Descendants().Attributes().FirstOrDefault(e => e.Value == "rank").Parent;
        title = ti.Value;

        uri = ti.Attributes().FirstOrDefault(a => a.Name.LocalName == "href").Value;
        rank = ra.Value.Replace(".", "").Trim();
        co = nextThree.Descendants().FirstOrDefault(a => a.Value.Contains("comments") && a.HasAttributes && a.Attributes().FirstOrDefault(at => at.Value.Contains(id)) != null);
        author = nextThree.Descendants().Attributes().FirstOrDefault(au => au.Value == "hnuser").Parent.Value;
        pt = nextThree.Descendants().FirstOrDefault(p => p.HasAttributes && p.Attributes().FirstOrDefault(a => a.Name.LocalName == "id" && a.Value == "score_" + id.Trim()) != null);

        s = "   {" + "\n";

        if (h.IsTitleAuthorValid(title))
          s = s + "      \"title\": " + title + " ,\n";

        if (h.IsUriValid(uri))
        {
          s = s + "      \"uri\": " + uri + " ,\n";
        }
        if (h.IsTitleAuthorValid(author))
        {
          s = s + "      \"author\": " + author + " ,\n";
        }

        if (pt != null)
        {
          points = pt.Value.Replace("points", "").Trim();
        }
        else
        {
          points = "0";
        }
        if (h.ArePointsCommentsRankIntegers(points))
          s = s + "      \"points\": " + points;
        if (co != null)
        {
          comments = co.Value.Replace("comments", "").Trim();
        }
        else
        {
          comments = "0";
        }
        if (h.ArePointsCommentsRankIntegers(comments))
          s = s + " ,\n" + "      \"comments\": " + comments;


        if (h.ArePointsCommentsRankIntegers(rank))
          s = s + " ,\n" + "      \"rank\": " + rank + "\n   }";

        if (element != itemsList.Last())
        {
          Console.WriteLine(s + ",");
        }
        else
        {
          Console.WriteLine(s);
        }
      }
      Console.WriteLine("]");
      Console.ReadLine();

    }

  }

}
