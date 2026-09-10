using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // for FontRepository and SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // ------------------------------------------------------------
        // Enable font substitution for any missing fonts.
        // Using FontRepository.Substitutions we can map a missing font
        // name to a fallback font (e.g., Arial). This substitution is
        // applied automatically during rendering, so it must be set
        // before any conversion or rendering operation.
        // ------------------------------------------------------------
        // Substitute any font that cannot be resolved with Arial.
        // The "*" wildcard is not supported; instead we add a generic
        // substitution that covers the most common missing fonts. You can
        // add additional entries as needed.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times New Roman", "Arial"));
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Courier", "Arial"));
        // If you need a catch‑all fallback, you can also add a substitution
        // for a font name that is unlikely to exist in the document.
        // FontRepository.Substitutions.Add(new SimpleFontSubstitution("MissingFont", "Arial"));

        // Define the resolution (DPI) for the PNG images
        const int resolution = 150; // 150 DPI – you can change this as needed

        // Iterate through each page and save it as a PNG image
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
            using (FileStream imageStream = new FileStream(outPath, FileMode.Create))
            {
                // PngDevice renders a page to a PNG image with the specified resolution
                PngDevice pngDevice = new PngDevice(resolution, resolution);
                pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
            }
        }

        Console.WriteLine("PDF has been converted to PNG images successfully.");
    }
}
