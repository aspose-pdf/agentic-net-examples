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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade for the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Apply changes only to page 3
                editor.ProcessPages = new int[] { 3 };

                // Set the transition type to a split effect (horizontal in)
                editor.TransitionType = PdfPageEditor.SPLITHIN;

                // Set the transition duration to 2 seconds
                editor.TransitionDuration = 2;

                // Commit the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with split transition on page 3: '{outputPath}'.");
    }
}