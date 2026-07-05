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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to set the display duration of page 5 to 5 seconds
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Specify that only page 5 should be processed
            editor.ProcessPages = new int[] { 5 };

            // Set the display duration (in seconds)
            editor.DisplayDuration = 5;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);

            // Close the facade (optional, as using will dispose)
            editor.Close();
        }

        Console.WriteLine($"Display duration set for page 5. Saved to '{outputPath}'.");
    }
}