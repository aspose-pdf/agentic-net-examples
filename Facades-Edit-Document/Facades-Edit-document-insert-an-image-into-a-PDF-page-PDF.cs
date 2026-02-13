using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF and image paths (adjust as needed)
        string pdfPath = "input.pdf";
        string imagePath = "image.png";
        string outputPath = "output.pdf";

        // Verify that the source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Error: Image file not found at '{imagePath}'.");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(pdfPath);

            // Create an Image object and attach the image stream
            Image img = new Image
            {
                // Keep the stream open until the document is saved
                ImageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read)
            };

            // Optionally set size or position (here we keep original size)
            // img.FixWidth = 200;
            // img.FixHeight = 200;

            // Insert the image into the first page (page index is 1‑based)
            pdfDocument.Pages[1].Paragraphs.Add(img);

            // Save the modified PDF (using the provided document-save rule)
            pdfDocument.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}