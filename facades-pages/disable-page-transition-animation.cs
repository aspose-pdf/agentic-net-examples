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
            // Create a PdfPageEditor facade and bind it to the document
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Specify that only page 6 should be edited
            editor.ProcessPages = new int[] { 6 };

            // Set transition duration to zero seconds (disable animation)
            editor.TransitionDuration = 0;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Transition duration set to zero on page 6. Saved to '{outputPath}'.");
    }
}