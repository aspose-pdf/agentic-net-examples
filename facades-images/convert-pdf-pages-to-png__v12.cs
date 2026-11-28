using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputImage42 = "page_42.png";
        const string outputImage43 = "page_43.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Convert page 42 to PNG
            ConvertPageToPng(inputPdf, 42, outputImage42);

            // Convert page 43 to PNG
            ConvertPageToPng(inputPdf, 43, outputImage43);

            Console.WriteLine($"Pages saved as images: {outputImage42}, {outputImage43}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void ConvertPageToPng(string pdfPath, int pageNumber, string outputPath)
    {
        // Load the PDF document.
        Document pdfDocument = new Document(pdfPath);

        // Ensure the requested page exists.
        if (pageNumber < 1 || pageNumber > pdfDocument.Pages.Count)
            throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number is out of range.");

        // Set the desired resolution (e.g., 300 DPI).
        Resolution resolution = new Resolution(300);

        // Create a concrete PNG device (devices are NOT disposable, so no using block).
        PngDevice pngDevice = new PngDevice(resolution)
        {
            // Optional: make background transparent.
            TransparentBackground = true
        };

        // Save the selected page as a PNG file.
        using (FileStream imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
        }
    }
}
