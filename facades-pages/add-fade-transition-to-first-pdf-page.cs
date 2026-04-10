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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor with the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Apply changes only to the first page
            editor.ProcessPages = new int[] { 1 };

            // Set transition type to Fade (0 corresponds to Fade in Aspose.Pdf)
            editor.TransitionType = 0;

            // Set transition duration to 2 seconds
            editor.TransitionDuration = 2;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with slide transition to '{outputPath}'.");
    }
}