using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        // Verify the PDF file exists before processing
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (a SaveableFacade) to bind the PDF and retrieve rotation
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);          // Load the PDF into the facade
            int rotation = editor.GetPageRotation(4); // Pages are 1‑based; get page 4 rotation
            Console.WriteLine($"Rotation of page 4: {rotation} degrees");
        }
    }
}