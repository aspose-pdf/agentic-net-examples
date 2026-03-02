using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF and image file paths
        const string inputPdfPath  = "input.pdf";
        const string imagePath     = "image.png";
        const string outputPdfPath = "output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the existing PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document has at least one page
            if (pdfDoc.Pages.Count == 0)
                pdfDoc.Pages.Add();

            // Create an Image object and set its source file
            Image img = new Image();
            img.File = imagePath;               // Correct: use parameterless constructor then set File
            img.FixWidth = 200;                 // Optional: set desired width
            img.FixHeight = 150;                // Optional: set desired height

            // Add the image to the first page (you can loop pages if needed)
            pdfDoc.Pages[1].Paragraphs.Add(img);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image added and PDF saved to '{outputPdfPath}'.");
    }
}