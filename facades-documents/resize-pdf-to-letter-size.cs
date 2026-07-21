using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized_letter.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfPageEditor is a facade that allows page size manipulation.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file (file‑path overload).
            editor.BindPdf(inputPath);

            // Set the desired page size. PageSize is in Aspose.Pdf namespace.
            editor.PageSize = PageSize.PageLetter;

            // Apply the changes to all pages.
            editor.ApplyChanges();

            // Save the modified PDF to a new file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}