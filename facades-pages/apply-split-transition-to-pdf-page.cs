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

        // Load the PDF document (creation rule)
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfPageEditor with the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Specify that only page 3 should be edited
            editor.ProcessPages = new int[] { 3 };

            // Set transition type to a split effect (horizontal in)
            editor.TransitionType = PdfPageEditor.SPLITHIN;

            // Set transition duration to 2 seconds
            editor.TransitionDuration = 2;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF (saving rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Transition applied and saved to '{outputPath}'.");
    }
}