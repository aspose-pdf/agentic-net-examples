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
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PdfPagesAsJpeg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // ------------------------------------------------------------
        // Font substitution – replace missing fonts with available ones
        // ------------------------------------------------------------
        // Example: replace any missing "MyMissingFont" with "Arial"
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("MyMissingFont", "Arial"));
        // Add more substitutions as needed, e.g.:
        // FontRepository.Substitutions.Add(new SimpleFontSubstitution("AnotherMissingFont", "Times New Roman"));

        // Set up JPEG conversion device (cross‑platform, no System.Drawing)
        var jpegDevice = new JpegDevice(
            new Aspose.Pdf.Devices.Resolution(150), // resolution (dpi)
            100); // image quality (0‑100)

        // Convert each page to JPEG
        for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
        {
            string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpeg");
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
            {
                jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
            }
            Console.WriteLine($"Saved page {pageNumber} as JPEG: {outputPath}");
        }
    }
}
