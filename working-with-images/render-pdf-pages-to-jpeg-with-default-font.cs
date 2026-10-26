using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PagesAsJpeg";
        const string defaultFontName = "Arial Unicode MS"; // font used for missing glyphs

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Register a global font substitution so that any missing glyphs are rendered
        // with the specified default font. The wildcard "*" applies the substitution to
        // all source fonts that are not found.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("*", defaultFontName));

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // JpegDevice settings (resolution 150 DPI, quality 90)
            var jpegDevice = new JpegDevice(new Resolution(150), 90);

            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Build output file name for the current page
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");

                // Render the page to JPEG
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                }

                Console.WriteLine($"Rendered page {pageNumber} to {outputPath}");
            }
        }

        Console.WriteLine("All pages have been rendered to JPEG images.");
    }
}
