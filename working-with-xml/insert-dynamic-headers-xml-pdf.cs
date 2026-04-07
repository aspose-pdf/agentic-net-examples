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
        const string inputPdfPath   = "input.pdf";      // source PDF
        const string xmlDataPath    = "headers.xml";   // XML with header text per page
        const string outputPdfPath  = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlDataPath}");
            return;
        }

        // Load the XML file that contains header text for each page.
        // Expected format:
        // <Headers>
        //   <Header page="1">First page header</Header>
        //   <Header page="2">Second page header</Header>
        //   ...
        // </Headers>
        XDocument xmlDoc = XDocument.Load(xmlDataPath);
        Dictionary<int, string> headerByPage = new Dictionary<int, string>();
        foreach (var elem in xmlDoc.Root.Elements("Header"))
        {
            if (int.TryParse(elem.Attribute("page")?.Value, out int pageNum))
            {
                headerByPage[pageNum] = elem.Value;
            }
        }

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Attach a handler to each page's OnBeforePageGenerate event.
            // The handler will insert a header just before the page is rendered.
            foreach (Page page in pdfDoc.Pages)
            {
                page.OnBeforePageGenerate += (Page pg) =>
                {
                    // Determine header text for the current page.
                    string headerText = headerByPage.TryGetValue(pg.Number, out string txt) ? txt : $"Page {pg.Number}";

                    // Create a text fragment for the header.
                    TextFragment tf = new TextFragment(headerText)
                    {
                        // Position the header near the top of the page.
                        // (x = 50 points from left, y = page height - 30 points)
                        Position = new Position(50, pg.PageInfo.Height - 30)
                    };

                    // Styling (modify the existing TextState – it is read‑only).
                    tf.TextState.FontSize = 12;
                    tf.TextState.Font = FontRepository.FindFont("Helvetica");
                    tf.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGray;

                    // Add the header to the page.
                    pg.Paragraphs.Add(tf);
                };
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Dynamic headers inserted. Output saved to '{outputPdfPath}'.");
    }
}
