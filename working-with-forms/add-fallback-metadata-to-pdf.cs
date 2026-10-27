using System;
using System.IO;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath   = "source.xml";
        const string pdfPath   = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML document
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Helper to get node text with fallback default
        string GetValue(string xpath, string defaultValue)
        {
            XmlNode node = xmlDoc.SelectSingleNode(xpath);
            return node?.InnerText ?? defaultValue;
        }

        // Define defaults for expected fields
        string title  = GetValue("/Document/Title",  "Untitled Document");
        string author = GetValue("/Document/Author", "Unknown Author");
        string date   = GetValue("/Document/Date",   DateTime.Now.ToString("yyyy-MM-dd"));

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a blank page
            Page page = pdfDoc.Pages.Add();

            // Prepare text fragments for each field
            TextFragment titleFragment  = new TextFragment($"Title:  {title}");
            TextFragment authorFragment = new TextFragment($"Author: {author}");
            TextFragment dateFragment   = new TextFragment($"Date:   {date}");

            // Set a simple text state (font size and color)
            titleFragment.TextState.FontSize  = 14;
            authorFragment.TextState.FontSize = 12;
            dateFragment.TextState.FontSize   = 12;

            titleFragment.TextState.ForegroundColor  = Aspose.Pdf.Color.Black;
            authorFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            dateFragment.TextState.ForegroundColor   = Aspose.Pdf.Color.Black;

            // Position fragments on the page
            titleFragment.Position  = new Position(50, 750);
            authorFragment.Position = new Position(50, 720);
            dateFragment.Position   = new Position(50, 690);

            // Add fragments to the page
            page.Paragraphs.Add(titleFragment);
            page.Paragraphs.Add(authorFragment);
            page.Paragraphs.Add(dateFragment);

            // Save the PDF (no SaveOptions needed for PDF format)
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated with fallback values: {pdfPath}");
    }
}