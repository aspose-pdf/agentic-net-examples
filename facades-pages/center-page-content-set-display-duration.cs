using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document and edit it with PdfPageEditor
        using (Document doc = new Document(inputPath))
        using (PdfPageEditor editor = new PdfPageEditor(doc))
        {
            // Apply changes only to page 5
            editor.ProcessPages = new int[] { 5 };

            // Center the original content on the result page
            editor.HorizontalAlignment = HorizontalAlignment.Center;

            // Set the display duration (in seconds) for the page
            editor.DisplayDuration = 4;

            // Apply the modifications
            editor.ApplyChanges();

            // Save the edited PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}