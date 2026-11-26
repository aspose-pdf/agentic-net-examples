using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // Required for possible font substitution (not used here)

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPngPath = "page14.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // No explicit DefaultFontName assignment – leaving it null lets Aspose.PDF fall back to the system default font (typically Times New Roman).
        // If a specific fallback is required, you could register a substitution like:
        // FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", "Times New Roman"));

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Verify that the document has at least 14 pages
            if (pdfDoc.Pages.Count < 14)
            {
                Console.Error.WriteLine("The PDF does not contain 14 pages.");
                return;
            }

            // Create a PNG rasterizer with 300 DPI resolution (Aspose.Pdf.Devices uses Aspose.Drawing internally)
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            // Render page 14 (1‑based indexing) to a memory stream
            using (MemoryStream pngStream = new MemoryStream())
            {
                pngDevice.Process(pdfDoc.Pages[14], pngStream);
                // Write the PNG bytes directly to the output file
                File.WriteAllBytes(outputPngPath, pngStream.ToArray());
            }
        }

        Console.WriteLine($"Page 14 rendered to PNG at 300 DPI: {outputPngPath}");
    }
}
