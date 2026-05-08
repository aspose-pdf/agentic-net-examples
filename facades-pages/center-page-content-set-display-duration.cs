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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Edit only page 5
                editor.ProcessPages = new int[] { 5 };

                // Center the original content horizontally on the result page
                editor.HorizontalAlignment = HorizontalAlignment.Center;

                // Set the display duration for the edited page to 4 seconds
                editor.DisplayDuration = 4;

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}