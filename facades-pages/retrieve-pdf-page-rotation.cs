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

        // Bind the PDF to the facade and retrieve the rotation of page 4
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Pages are 1‑based; page 4 corresponds to the fourth page in the document
            int rotation = editor.GetPageRotation(4);

            Console.WriteLine($"Rotation of page 4: {rotation} degrees");
        }
    }
}