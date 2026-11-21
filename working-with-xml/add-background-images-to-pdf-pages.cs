using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF, XML definition and output PDF paths
        const string pdfPath = "input.pdf";
        const string xmlPath = "backgrounds.xml";
        const string outputPdfPath = "output.pdf";

        // Verify files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load the XML that defines background images per page.
        // Expected format:
        // <Backgrounds>
        //   <Page number="1" image="bg1.png" />
        //   <Page number="2" image="bg2.png" />
        //   ...
        // </Backgrounds>
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over each <Page> element in the XML.
            foreach (XElement pageElem in xmlDoc.Root.Elements("Page"))
            {
                // Parse page number (1‑based) and image file path.
                if (!int.TryParse(pageElem.Attribute("number")?.Value, out int pageNumber))
                {
                    Console.Error.WriteLine("Invalid or missing page number attribute.");
                    continue;
                }
                string imagePath = pageElem.Attribute("image")?.Value;
                if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
                {
                    Console.Error.WriteLine($"Image not found for page {pageNumber}: {imagePath}");
                    continue;
                }

                // Ensure the requested page exists in the PDF.
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page number {pageNumber} is out of range (1‑{pdfDoc.Pages.Count}).");
                    continue;
                }

                // Create an ImageStamp that will be placed behind existing content.
                ImageStamp bgStamp = new ImageStamp(imagePath)
                {
                    // Mark as background so it does not obscure page content.
                    Background = true,
                    // Size the stamp to fill the entire page.
                    Width = pdfDoc.Pages[pageNumber].PageInfo.Width,
                    Height = pdfDoc.Pages[pageNumber].PageInfo.Height,
                    // Position at the origin of the page.
                    XIndent = 0,
                    YIndent = 0
                };

                // Add the stamp to the specified page.
                pdfDoc.Pages[pageNumber].AddStamp(bgStamp);
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Background images applied. Output saved to '{outputPdfPath}'.");
    }
}
