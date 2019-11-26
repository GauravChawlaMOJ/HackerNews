# HackerNews
Hacker News in Json

How to run:

Please open the command prompt window (Press windows button + R. Type cmd in the box and press enter or click ok).

 1. Change the path to where the program is installed and HackerNews.exe file is located. 
   Alternatively, you can directly open the installation folder and enter cmd in the address bar. 

 2. Type "HackerNews" and space then enter the number of posts (between 0 and 100) need to be displayed. For example type "HackerNews 2" (without quotes) in the command prompt.

Libraries used:

  SgmlReader: To read html from the webpage. It is used as it parses the html really well. 
  
  System.XML.Linq: To convert html into xml. HTML is converted into xml so that linq could be used to read the data.

 
