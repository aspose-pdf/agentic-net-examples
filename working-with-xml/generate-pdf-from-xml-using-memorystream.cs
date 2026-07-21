using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Sample XML content – replace with your actual XML data
        string xmlContent = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<root>
    <title>Sample PDF</title>
    <body>This PDF was generated from XML in a memory stream.</body>
</root>";

        // Convert the XML string to a UTF‑8 byte array and wrap it in a MemoryStream
        byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlContent);
        using (MemoryStream xmlStream = new MemoryStream(xmlBytes))
        {
            // Parse the XML from the memory stream
            XDocument xDoc = XDocument.Load(xmlStream);
            string title = xDoc.Root.Element("title")?.Value ?? "Untitled";
            string body = xDoc.Root.Element("body")?.Value ?? string.Empty;

            // Create a new PDF document with default settings
            Document pdfDocument = new Document();

            // Add a page
            Page page = pdfDocument.Pages.Add();

            // Add the title
            TextFragment titleFragment = new TextFragment(title)
            {
                TextState = { FontSize = 20, FontStyle = FontStyles.Bold, ForegroundColor = Color.Black },
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = { Top = 20 }
            };
            page.Paragraphs.Add(titleFragment);

            // Add the body text
            TextFragment bodyFragment = new TextFragment(body)
            {
                TextState = { FontSize = 12, FontStyle = FontStyles.Regular, ForegroundColor = Color.Black },
                Margin = { Top = 30 }
            };
            page.Paragraphs.Add(bodyFragment);

            // Save the resulting PDF with default settings
            pdfDocument.Save("output.pdf");
        }
    }
}
