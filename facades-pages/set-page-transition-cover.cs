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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Load the document
        Document doc = new Document(inputPath);

        // Edit page 4 and set a Cover transition with 1‑second duration
        using (PdfPageEditor editor = new PdfPageEditor(doc))
        {
            editor.ProcessPages = new int[] { 4 };          // page numbers are 1‑based
            editor.TransitionType = 4;                     // 4 = Cover transition (use integer value)
            editor.TransitionDuration = 1;                // duration in seconds
            editor.ApplyChanges();
            editor.Save(outputPath);                       // persist the changes
        }

        Console.WriteLine("Transition applied and saved to " + outputPath);
    }
}
