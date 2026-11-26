using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page10.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Configure a font substitution so that any missing font falls back to Times New Roman.
        // The "default" key applies the substitution globally.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("default", "Times New Roman"));

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Verify the document has at least 10 pages (1‑based indexing).
            if (pdfDoc.Pages.Count < 10)
            {
                Console.Error.WriteLine("The PDF contains fewer than 10 pages.");
                return;
            }

            // Retrieve the 10th page.
            Page page = pdfDoc.Pages[10];

            // Create a PNG device with a resolution of 300 DPI.
            PngDevice pngDevice = new PngDevice(new Resolution(300));

            // Render the page to a PNG file.
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
            {
                pngDevice.Process(page, imageStream);
            }
        }

        Console.WriteLine($"Rendered page 10 to PNG at 300 DPI: {outputPath}");
    }
}
