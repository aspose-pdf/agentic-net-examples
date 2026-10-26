using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output_with_headers.pdf";
        const string xmlDataPath    = "headers.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // Load XML that defines a header per page, e.g.:
        // <Headers>
        //   <Header page="1">Report Title – Page 1</Header>
        //   <Header page="2">Report Title – Page 2</Header>
        //   ...
        // </Headers>
        XDocument xmlDoc = XDocument.Load(xmlDataPath);

        // Open the PDF inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement).
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                // Find the header text for the current page.
                XElement headerElement = xmlDoc.Root?
                    .Element("Headers")?
                    .Elements("Header")
                    .FirstOrDefault(e => (string)e.Attribute("page") == pageIndex.ToString());

                if (headerElement == null)
                    continue; // No header defined for this page.

                string headerText = headerElement.Value.Trim();
                if (string.IsNullOrEmpty(headerText))
                    continue;

                // Create a TextFragment for the header.
                TextFragment headerFragment = new TextFragment(headerText)
                {
                    // Set visual appearance.
                    TextState =
                    {
                        FontSize = 12,
                        FontStyle = FontStyles.Bold,
                        ForegroundColor = Aspose.Pdf.Color.DarkBlue
                    },
                    // Position the header near the top of the page.
                    // (x, y) coordinates are in points; (0,0) is bottom‑left.
                    Position = new Position(50, pdfDoc.Pages[pageIndex].PageInfo.Height - 30)
                };

                // Add the header to the page's paragraph collection.
                pdfDoc.Pages[pageIndex].Paragraphs.Add(headerFragment);
            }

            // Save the modified PDF. No SaveOptions needed because the output is PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with dynamic headers saved to '{outputPdfPath}'.");
    }
}