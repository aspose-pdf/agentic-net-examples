using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string xmlDataPath   = "headers.xml";

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

        // Load XML containing header texts per page.
        // Expected format:
        // <Headers>
        //   <Page number="1">First page header</Page>
        //   <Page number="2">Second page header</Page>
        //   ...
        // </Headers>
        XDocument xmlDoc = XDocument.Load(xmlDataPath);

        // Open the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement).
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Retrieve header text for the current page from XML.
                string headerText = xmlDoc.Root
                    .Elements("Page")
                    .FirstOrDefault(p => (int?)p.Attribute("number") == i)?
                    .Value ?? $"Page {i}";

                // Create a text fragment for the header.
                TextFragment headerFragment = new TextFragment(headerText)
                {
                    // Position near the top of the page (50 points from left, 50 points from top).
                    Position = new Position(50, page.PageInfo.Height - 50)
                };

                // Style the header (optional).
                headerFragment.TextState.FontSize = 12;
                headerFragment.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;
                headerFragment.TextState.Font = FontRepository.FindFont("Helvetica");

                // Add the header to the page.
                page.Paragraphs.Add(headerFragment);
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Dynamic headers added. Output saved to '{outputPdfPath}'.");
    }
}