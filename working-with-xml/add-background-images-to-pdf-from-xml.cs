using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // For any facade usage if needed (not used here)

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";          // Source PDF
        const string xmlPath   = "backgrounds.xml";    // XML defining backgrounds
        const string outputPdf = "output_branded.pdf"; // Result PDF

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

        // Load the XML. Expected format:
        // <Backgrounds>
        //   <Page number="1" image="brand1.png" />
        //   <Page number="2" image="brand2.png" />
        //   ...
        // </Backgrounds>
        XDocument xmlDoc = XDocument.Load(xmlPath);

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over each <Page> element in the XML.
            foreach (XElement pageElem in xmlDoc.Root.Elements("Page"))
            {
                // Parse page number (1‑based indexing as required by Aspose.Pdf).
                if (!int.TryParse(pageElem.Attribute("number")?.Value, out int pageNumber) ||
                    pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.WriteLine($"Skipping invalid or out‑of‑range page number: {pageElem.Attribute("number")?.Value}");
                    continue;
                }

                // Resolve image path (relative to the XML file location).
                string imagePath = pageElem.Attribute("image")?.Value;
                if (string.IsNullOrWhiteSpace(imagePath))
                {
                    Console.WriteLine($"No image specified for page {pageNumber}, skipping.");
                    continue;
                }

                // Make the image path absolute if it is relative.
                if (!Path.IsPathRooted(imagePath))
                {
                    string xmlDir = Path.GetDirectoryName(Path.GetFullPath(xmlPath));
                    imagePath = Path.Combine(xmlDir, imagePath);
                }

                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"Image file not found for page {pageNumber}: {imagePath}");
                    continue;
                }

                // Retrieve the target page.
                Page page = pdfDoc.Pages[pageNumber];

                // Create an Aspose.Pdf.Image, set its file, and assign it as the page background.
                Image bgImage = new Image();
                bgImage.File = imagePath;
                page.BackgroundImage = bgImage; // Generator‑only property; works for newly created PDFs.

                // Optional: ensure the background image covers the whole page.
                // This can be done by scaling the image via a BackgroundArtifact if finer control is needed.
                // For simplicity, we rely on the generator to place the image as provided.
            }

            // Save the modified PDF. No SaveOptions needed for PDF output.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Branded PDF saved to '{outputPdf}'.");
    }
}