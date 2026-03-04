using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_images.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the facade for editing PDF content
        PdfContentEditor editor = new PdfContentEditor();
        try
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Remove all images from the document.
            // To target a specific page, use DeleteImage(pageNumber, int[] indexes)
            editor.DeleteImage();

            // Save the modified PDF
            editor.Save(outputPath);
        }
        finally
        {
            // Ensure resources are released
            editor.Close();
        }

        Console.WriteLine($"Images removed. Saved to '{outputPath}'.");
    }
}