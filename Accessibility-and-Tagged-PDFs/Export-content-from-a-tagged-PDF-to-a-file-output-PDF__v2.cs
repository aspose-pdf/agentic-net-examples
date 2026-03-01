using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use the PdfBookmarkEditor facade to load the PDF and save it to a new file.
        // This works for both tagged and untagged PDFs and preserves all content.
        PdfBookmarkEditor editor = new PdfBookmarkEditor();

        try
        {
            // Bind the source PDF to the facade
            editor.BindPdf(inputPath);

            // Save the bound document to the output path
            editor.Save(outputPath);
        }
        finally
        {
            // Ensure resources are released
            editor.Close();
        }

        Console.WriteLine($"Export completed. Output saved to '{outputPath}'.");
    }
}