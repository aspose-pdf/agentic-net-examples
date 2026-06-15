using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // Not required here but kept for completeness

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";      // Source PDF
        const string xmlConfigPath  = "backgrounds.xml"; // XML defining page backgrounds
        const string outputPdfPath  = "output_branded.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlConfigPath))
        {
            Console.Error.WriteLine($"XML config not found: {xmlConfigPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Load XML configuration
                XDocument xmlDoc = XDocument.Load(xmlConfigPath);

                // Expected XML format:
                // <Backgrounds>
                //   <Page number="1" image="bg1.png" />
                //   <Page number="2" image="bg2.jpg" />
                // </Backgrounds>
                foreach (XElement pageElem in xmlDoc.Root.Elements("Page"))
                {
                    // Parse page number
                    if (!int.TryParse(pageElem.Attribute("number")?.Value, out int pageNumber) ||
                        pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                    {
                        Console.Error.WriteLine($"Invalid or out‑of‑range page number: {pageElem.Attribute("number")?.Value}");
                        continue;
                    }

                    // Resolve image path
                    string imagePath = pageElem.Attribute("image")?.Value;
                    if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
                    {
                        Console.Error.WriteLine($"Image not found for page {pageNumber}: {imagePath}");
                        continue;
                    }

                    // Create a background artifact and attach it to the target page
                    BackgroundArtifact bgArtifact = new BackgroundArtifact();
                    bgArtifact.SetImage(imagePath);          // Load image from file
                    bgArtifact.IsBackground = true;         // Place behind page content
                    pdfDoc.Pages[pageNumber].Artifacts.Add(bgArtifact);
                }

                // Save the modified PDF (lifecycle rule: save inside using block)
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Branded PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}