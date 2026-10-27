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
        Document pdfDocument = new Document(inputPath);

        // Use PdfPageEditor to set a Cover transition on page 4 with a 1‑second duration
        using (PdfPageEditor editor = new PdfPageEditor(pdfDocument))
        {
            // Specify the page(s) to which the transition will be applied (1‑based indexing)
            editor.ProcessPages = new int[] { 4 };

            // TransitionType values are defined as integers in older Aspose.Pdf versions.
            // 4 corresponds to TransitionType.Cover.
            editor.TransitionType = 4; // Cover transition
            editor.TransitionDuration = 1; // 1 second

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Transition applied to page 4 and saved as '{outputPath}'.");
    }
}
