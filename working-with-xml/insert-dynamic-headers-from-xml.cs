using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xmlDataPath = "headers.xml";
        const string outputPdfPath = "output_with_headers.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data not found: {xmlDataPath}");
            return;
        }

        // Load XML that contains header text per page.
        // Expected format:
        // <Headers>
        //   <Header page="1">First page header</Header>
        //   <Header page="2">Second page header</Header>
        //   ...
        // </Headers>
        XDocument xmlDoc = XDocument.Load(xmlDataPath);
        var headerMap = new Dictionary<int, string>();
        if (xmlDoc.Root != null)
        {
            foreach (var elem in xmlDoc.Root.Elements("Header"))
            {
                if (int.TryParse(elem.Attribute("page")?.Value, out int pageNum))
                {
                    headerMap[pageNum] = elem.Value;
                }
            }
        }

        // Open the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Assign a header to each page based on the XML map.
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];
                if (headerMap.TryGetValue(page.Number, out string headerText))
                {
                    // Create a HeaderFooter object for this page.
                    HeaderFooter header = new HeaderFooter();
                    // Add the text fragment (you can style it via TextState if needed).
                    header.Paragraphs.Add(new TextFragment(headerText));
                    page.Header = header;
                }
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with dynamic headers to '{outputPdfPath}'.");
    }
}
