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

        using (Document document = new Document(inputPath))
        {
            PdfPageEditor editor = new PdfPageEditor(document);
            // Edit only page 3 (1‑based indexing)
            editor.ProcessPages = new int[] { 3 };
            // Set a horizontal split transition effect
            editor.TransitionType = PdfPageEditor.SPLITHIN;
            // Duration of the transition in seconds
            editor.TransitionDuration = 2;
            editor.ApplyChanges();

            document.Save(outputPath);
        }

        Console.WriteLine("Transition applied and saved to '" + outputPath + "'.");
    }
}
