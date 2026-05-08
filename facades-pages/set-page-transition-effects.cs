using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_transitions.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor with the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Page 1 – Fade effect (using DISSOLVE constant)
                editor.ProcessPages = new int[] { 1 };
                editor.TransitionType = PdfPageEditor.DISSOLVE;
                editor.TransitionDuration = 2; // duration in seconds
                editor.ApplyChanges();

                // Page 2 – BoxOut effect (using OUTBOX constant)
                editor.ProcessPages = new int[] { 2 };
                editor.TransitionType = PdfPageEditor.OUTBOX;
                editor.TransitionDuration = 3;
                editor.ApplyChanges();

                // Page 3 – Cover effect (using INBOX constant)
                editor.ProcessPages = new int[] { 3 };
                editor.TransitionType = PdfPageEditor.INBOX;
                editor.TransitionDuration = 2;
                editor.ApplyChanges();

                // Save the modified PDF
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with transitions saved to '{outputPath}'.");
    }
}