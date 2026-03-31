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
            PdfPageEditor pageEditor = new PdfPageEditor(document);
            int[] pagesToEdit = new int[1];
            pagesToEdit[0] = 2; // target page number (1‑based)
            pageEditor.ProcessPages = pagesToEdit;
            pageEditor.TransitionType = PdfPageEditor.OUTBOX; // outward box (zoom‑out) effect
            pageEditor.TransitionDuration = 3; // duration in seconds
            pageEditor.ApplyChanges();

            document.Save(outputPath);
        }

        Console.WriteLine("Transition applied and saved to '" + outputPath + "'.");
    }
}