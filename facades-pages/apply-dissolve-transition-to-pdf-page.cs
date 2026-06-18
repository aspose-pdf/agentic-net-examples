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
            // Initialize the page editor and bind the document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);
                // Target only page 5 (1‑based indexing)
                editor.ProcessPages = new int[] { 5 };
                // Set transition to Dissolve
                editor.TransitionType = PdfPageEditor.DISSOLVE;
                // Set transition duration to 3 seconds
                editor.TransitionDuration = 3;
                // Apply the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Transition applied and saved to '{outputPath}'.");
    }
}