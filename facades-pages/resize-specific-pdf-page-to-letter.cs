using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_letter.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfPageEditor is a disposable facade – wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Specify that only page 6 should be edited.
            editor.ProcessPages = new int[] { 6 };

            // Set the desired output page size to Letter.
            editor.PageSize = Aspose.Pdf.PageSize.PageLetter;

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 6 resized to Letter size and saved as '{outputPath}'.");
    }
}