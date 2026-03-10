using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf  = "input.pdf";
        const string newImage  = "newImage.jpg";
        const string outputPdf = "output.pdf";

        // Validate files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(newImage))
        {
            Console.Error.WriteLine($"Replacement image not found: {newImage}");
            return;
        }

        // Create the facade, bind the PDF, replace the image, and save.
        // PdfContentEditor does NOT implement IDisposable, so we do NOT wrap it in a using block.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Page numbers and image indexes are 1‑based in Aspose.Pdf.
        // Here we replace the first image (index = 1) on the first page (pageNumber = 1).
        int pageNumber = 1;
        int imageIndex = 1;
        editor.ReplaceImage(pageNumber, imageIndex, newImage);

        // Save the modified document.
        editor.Save(outputPdf);

        Console.WriteLine($"Image replaced and saved to '{outputPdf}'.");
    }
}