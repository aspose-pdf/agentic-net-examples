using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_centered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor with the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Apply changes only to page 5
            editor.ProcessPages = new int[] { 5 };

            // Center the original PDF content on the result page
            editor.HorizontalAlignment = HorizontalAlignment.Center;

            // Set the display duration of the edited page to 4 seconds
            editor.DisplayDuration = 4;

            // Apply the modifications
            editor.ApplyChanges();

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}