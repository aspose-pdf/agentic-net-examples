using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PNG images
        const string imagesFolder = "Images";
        // Output PDF file path
        const string outputPdf = "output.pdf";

        if (!Directory.Exists(imagesFolder))
        {
            Console.Error.WriteLine($"Folder not found: {imagesFolder}");
            return;
        }

        // Get all PNG files in the folder, sorted alphabetically to define page order
        string[] pngFiles = Directory.GetFiles(imagesFolder, "*.png");
        if (pngFiles.Length == 0)
        {
            Console.Error.WriteLine("No PNG files found in the specified folder.");
            return;
        }
        Array.Sort(pngFiles);

        // Create a new PDF document
        var pdfDocument = new Document();

        foreach (var pngPath in pngFiles)
        {
            // Add a new page for each image
            var page = pdfDocument.Pages.Add();

            // Create an Image object and set its source file
            var image = new Image
            {
                File = pngPath
            };

            // Optionally, fit the image to the page while respecting default margins
            // Here we simply add the image; Aspose.PDF will place it at the origin.
            page.Paragraphs.Add(image);
        }

        // Save the combined PDF
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"PDF successfully created at '{outputPdf}'.");
    }
}
