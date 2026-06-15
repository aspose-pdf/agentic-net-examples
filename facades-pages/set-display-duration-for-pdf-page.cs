using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor facade to edit page properties
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Specify that only page 5 should be processed
            editor.ProcessPages = new int[] { 5 };

            // Set display duration to 5 seconds for the selected page
            editor.DisplayDuration = 5;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 5 display duration set to 5 seconds. Saved to '{outputPath}'.");
    }
}