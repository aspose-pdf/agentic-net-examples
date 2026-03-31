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

        using (Document doc = new Document(inputPath))
        {
            // Page 1 – Fade effect (DISSOLVE)
            PdfPageEditor editor1 = new PdfPageEditor(doc);
            editor1.ProcessPages = new int[] { 1 };
            editor1.TransitionType = PdfPageEditor.DISSOLVE;
            editor1.TransitionDuration = 2; // seconds
            editor1.ApplyChanges();

            // Page 2 – Box Out effect (INBOX)
            PdfPageEditor editor2 = new PdfPageEditor(doc);
            editor2.ProcessPages = new int[] { 2 };
            editor2.TransitionType = PdfPageEditor.INBOX;
            editor2.TransitionDuration = 2;
            editor2.ApplyChanges();

            // Page 3 – Cover effect (OUTBOX)
            PdfPageEditor editor3 = new PdfPageEditor(doc);
            editor3.ProcessPages = new int[] { 3 };
            editor3.TransitionType = PdfPageEditor.OUTBOX;
            editor3.TransitionDuration = 2;
            editor3.ApplyChanges();

            doc.Save(outputPath);
        }

        Console.WriteLine("Transitions applied and saved to '" + outputPath + "'.");
    }
}