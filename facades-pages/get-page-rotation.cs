using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with PdfPageEditor and retrieve rotation of page 4 (1‑based index)
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);
            int rotation = editor.GetPageRotation(4);
            Console.WriteLine($"Rotation of page 4: {rotation} degrees");
        }
    }
}