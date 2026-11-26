using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;          // PngDevice for rasterizing pages, Resolution for DPI
using Aspose.Pdf.Text;            // FontRepository, SimpleFontSubstitution

class RenderPageToPng
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPngPath = "page12.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal (document-disposal-with-using rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least 12 pages (page-indexing-one-based rule)
            if (doc.Pages.Count < 12)
            {
                Console.Error.WriteLine("The document contains fewer than 12 pages.");
                return;
            }

            // Set Impact as the default substitution font for any missing/unspecified fonts.
            // Aspose.Pdf does not expose a Document.DefaultFont property; the recommended way is to add a font substitution.
            Font impactFont = FontRepository.FindFont("Impact");
            if (impactFont != null)
            {
                // Substitute any font that cannot be resolved with Impact.
                // Using "*" as a wildcard source font name replaces all missing fonts.
                // The SimpleFontSubstitution constructor expects the target font name as a string.
                FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", impactFont.FontName));
            }
            else
            {
                Console.WriteLine("Impact font not found on the system; font substitution not applied.");
            }

            // Create a PNG device with 300 DPI resolution (default is 72 DPI).
            // The constructor expects a Resolution object, not an int.
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            // Render the 12th page (1‑based index) to a PNG file
            using (FileStream imageStream = new FileStream(outputPngPath, FileMode.Create))
            {
                // Process the specific page; Pages[12] is the 12th page.
                pngDevice.Process(doc.Pages[12], imageStream);
            }

            Console.WriteLine($"Page 12 rendered to PNG at 300 DPI: {outputPngPath}");
        }
    }
}
