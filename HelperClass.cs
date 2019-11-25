using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace HackerNews
{
  class HelperClass
  {
    public bool IsTitleAuthorValid(string str)
    {
      if (str == string.Empty || str.Length > 256)
      {
        return false;
      }
      return true;
    }
    public bool IsUriValid(string uri)
    {
      Uri uriResult;
      bool result = Uri.TryCreate(uri, UriKind.Absolute, out uriResult)
          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
      return result;
    }
    public bool ArePointsCommentsRankIntegers(string s)
    {
      int i;
      if (int.TryParse(s, out i) && i >= 0)
      {
        return true;
      }
      return false;
    }
    public XDocument FromHtmlToXDoc(string webAddress)
    {
      WebClient webPage = new WebClient();
      string html = webPage.DownloadString(webAddress);   
      using (TextReader sr = new StringReader(html))
      {
        Sgml.SgmlReader sgmlReader = new Sgml.SgmlReader();
        sgmlReader.DocType = "HTML";
        sgmlReader.WhitespaceHandling = WhitespaceHandling.All;
        sgmlReader.CaseFolding = Sgml.CaseFolding.ToLower;
        sgmlReader.InputStream = sr;
        return XDocument.Load(sgmlReader);
      }
    }
    public int IsInputOK(string NoOfPosts)
    {
      int temp;
      if (!int.TryParse(NoOfPosts, out temp) || temp < 0 || temp > 100)
      {
        return 0;
      }
      return temp;
    }
  }
}
