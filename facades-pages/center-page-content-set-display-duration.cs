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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor facade and bind it to the loaded document
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Specify that only page 5 should be edited
            editor.ProcessPages = new int[] { 5 };

            // Center the original content on the result page
            editor.HorizontalAlignment = HorizontalAlignment.Center;

            // Set the display duration for the edited page to 4 seconds
            editor.DisplayDuration = 4;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF to the output file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page alignment centered and display duration set on page 5. Saved to '{outputPath}'.");
    }
}