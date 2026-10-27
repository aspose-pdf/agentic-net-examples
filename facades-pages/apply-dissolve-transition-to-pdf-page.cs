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
        Document doc = new Document(inputPath);

        // Use PdfPageEditor to set a Dissolve transition (value 5) with a 3‑second duration on page 5
        using (PdfPageEditor editor = new PdfPageEditor(doc))
        {
            editor.ProcessPages = new int[] { 5 }; // 1‑based page index
            editor.TransitionType = 5;            // 5 corresponds to Dissolve transition
            editor.TransitionDuration = 3;        // duration in seconds
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Transition applied and saved to '{outputPath}'.");
    }
}
