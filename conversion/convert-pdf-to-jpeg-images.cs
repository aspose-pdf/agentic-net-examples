using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // Required for JpegDevice
using Aspose.Pdf.Text;   // needed for TextFragment

class PdfToJpegConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDir = "JpegImages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // If the input PDF does not exist, create a minimal sample PDF so the program can run without error
        if (!File.Exists(inputPdfPath))
        {
            CreateSamplePdf(inputPdfPath);
            Console.WriteLine($"Sample PDF created at '{inputPdfPath}'.");
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // JpegDevice uses a resolution (DPI). The default quality is 100, which matches the requirement.
            // Here we use the default constructor (300 DPI, 100% quality).
            var jpegDevice = new JpegDevice();

            // Iterate through all pages (1‑based indexing used for user‑friendly output)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for the current page
                string outputFile = Path.Combine(outputDir, $"page_{pageNumber}.jpeg");

                // Save the selected page as a JPEG image using JpegDevice
                using (FileStream imageStream = new FileStream(outputFile, FileMode.Create))
                {
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as {outputFile}");
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }

    // Helper method to generate a simple PDF when the expected input file is missing
    private static void CreateSamplePdf(string path)
    {
        using (var doc = new Document())
        {
            var page = doc.Pages.Add();
            var fragment = new TextFragment("Sample PDF generated because 'input.pdf' was not found.");
            page.Paragraphs.Add(fragment);
            doc.Save(path);
        }
    }
}
