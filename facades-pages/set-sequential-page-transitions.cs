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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the page editor with the loaded document
            PdfPageEditor editor = new PdfPageEditor(doc);

            // Page 1 – vertical blinds transition
            editor.ProcessPages = new int[] { 1 };
            editor.TransitionType = PdfPageEditor.BLINDV;
            editor.TransitionDuration = 2; // seconds
            editor.ApplyChanges();

            // Page 2 – dissolve transition
            editor.ProcessPages = new int[] { 2 };
            editor.TransitionType = PdfPageEditor.DISSOLVE;
            editor.TransitionDuration = 3;
            editor.ApplyChanges();

            // Page 3 – left‑right wipe transition
            editor.ProcessPages = new int[] { 3 };
            editor.TransitionType = PdfPageEditor.LRWIPE;
            editor.TransitionDuration = 2;
            editor.ApplyChanges();

            // Page 4 – bottom‑top wipe transition (if the document has at least 4 pages)
            if (doc.Pages.Count >= 4)
            {
                editor.ProcessPages = new int[] { 4 };
                editor.TransitionType = PdfPageEditor.BTWIPE;
                editor.TransitionDuration = 2;
                editor.ApplyChanges();
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with page transitions saved to '{outputPath}'.");
    }
}