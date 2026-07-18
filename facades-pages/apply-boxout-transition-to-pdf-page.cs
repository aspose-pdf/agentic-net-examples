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
            // Initialize the page editor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the document to the editor
                editor.BindPdf(doc);

                // Apply the transition only to page 2
                editor.ProcessPages = new int[] { 2 };

                // Set transition type to BoxOut (outward box)
                editor.TransitionType = PdfPageEditor.OUTBOX;

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