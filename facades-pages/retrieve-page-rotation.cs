using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        // Ensure the file exists before processing
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to access page information
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(inputPath);

            // Pages are 1‑based; retrieve rotation of page 4
            int rotation = editor.GetPageRotation(4);

            Console.WriteLine($"Rotation of page 4: {rotation} degrees");
        }
    }
}