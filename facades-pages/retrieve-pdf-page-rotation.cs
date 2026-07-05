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

        // Bind the PDF with PdfPageEditor and retrieve rotation of page 4
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            int rotation = editor.GetPageRotation(4); // Pages are 1‑based
            Console.WriteLine($"Rotation of page 4: {rotation} degrees");
        }
    }
}