using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "Images";
        const string substituteFontPath = "substitute.ttf"; // Path to the TTF file used for substitution
        const string missingFontName = "MissingFont"; // Name of the font that may be missing in the PDF

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(substituteFontPath))
        {
            Console.Error.WriteLine($"Substitute font not found: {substituteFontPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Register a substitution for the missing font. The SimpleFontSubstitution constructor can accept the path to the TTF file.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution(missingFontName, substituteFontPath));

        // Load the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Prepare a JPEG device – 300 DPI resolution, 100% quality (adjust as needed)
            var jpegDevice = new JpegDevice(new Resolution(300), 100);

            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                using (var outStream = new MemoryStream())
                {
                    // Convert the specific page to JPEG
                    jpegDevice.Process(pdfDoc.Pages[pageNum], outStream);
                    outStream.Position = 0;

                    string outPath = Path.Combine(outputDir, $"Page_{pageNum}.jpg");
                    File.WriteAllBytes(outPath, outStream.ToArray());
                    Console.WriteLine($"Saved {outPath}");
                }
            }
        }
    }
}
