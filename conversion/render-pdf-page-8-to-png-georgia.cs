using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class RenderPdfPageToPng
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string pngPath = "page8.png";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Substitute any missing font with Georgia (acts as the default font)
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Georgia"));

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Ensure the document has at least 8 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 8)
            {
                Console.Error.WriteLine("The PDF does not contain 8 pages.");
                return;
            }

            // Get the 8th page
            Page page = doc.Pages[8];

            // Create a resolution of 300 DPI
            Resolution resolution = new Resolution(300);

            // Render the page directly to a PNG file using PngDevice
            using (FileStream outStream = File.Create(pngPath))
            {
                PngDevice pngDevice = new PngDevice(resolution);
                pngDevice.Process(page, outStream);
            }
        }

        Console.WriteLine($"Page 8 rendered to PNG at 300 DPI: {pngPath}");
    }
}
