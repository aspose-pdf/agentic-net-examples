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
            // Initialize the page editor with the document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Apply changes only to page 3 (1‑based indexing)
                editor.ProcessPages = new int[] { 3 };

                // Set transition to a horizontal split (IN)
                editor.TransitionType = PdfPageEditor.SPLITHIN;

                // Set transition duration to 2 seconds
                editor.TransitionDuration = 2;

                // Apply the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}