using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Prepare a multi‑line text fragment (contains different line‑break characters)
        string multiLineText = "First line\r\nSecond line\nThird line";
        TextFragment fragment = new TextFragment(multiLineText);

        // Use a regular expression to split the text while preserving the line‑break characters.
        // Each match represents a line together with its terminating line‑break (if any).
        var lineMatches = Regex.Matches(multiLineText, ".*?(\r\n|\n|\r|$)");

        foreach (Match match in lineMatches)
        {
            // The captured value includes the line‑break characters (if present).
            string rawLine = match.Value;
            // Remove trailing CR/LF characters to obtain the visible line text.
            string lineText = rawLine.TrimEnd('\r', '\n');
            // A line‑break flag is true when the original line ended with a CR, LF or CRLF.
            bool lineBreakFlag = rawLine.EndsWith("\r\n") || rawLine.EndsWith("\n") || rawLine.EndsWith("\r");

            Console.WriteLine($"Line text: \"{lineText}\"  LineBreak: {lineBreakFlag}");
        }

        // OPTIONAL: add the fragment to a PDF page and save the document
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(fragment);
            doc.Save("output.pdf");
        }
    }
}
