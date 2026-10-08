using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // required for PdfPageEditor

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_transitions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        int pageCount = pdfDocument.Pages.Count; // 1‑based collection

        // Apply a custom transition to each page based on its index using PdfPageEditor
        using (PdfPageEditor editor = new PdfPageEditor(pdfDocument))
        {
            for (int i = 1; i <= pageCount; i++)
            {
                // Select the current page
                editor.ProcessPages = new int[] { i };

                // Choose transition type and duration
                if (i % 2 == 0) // even pages → Box transition (integer value 3)
                {
                    editor.TransitionType = 3; // Box
                    editor.TransitionDuration = 2; // 2 seconds
                }
                else // odd pages → Fly transition (integer value 7)
                {
                    editor.TransitionType = 7; // Fly
                    editor.TransitionDuration = 1; // 1 second
                }

                // Apply the changes for the selected page
                editor.ApplyChanges();
            }
        }

        // Save the modified PDF with the applied transitions
        pdfDocument.Save(outputPath);

        Console.WriteLine($"PDF with page transitions saved to '{outputPath}'.");
    }
}
