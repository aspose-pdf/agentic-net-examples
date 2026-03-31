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
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Page 1 – assumed text content – apply Horizontal Blinds transition
                editor.ProcessPages = new int[] { 1 };
                editor.TransitionType = PdfPageEditor.BLINDH;
                editor.TransitionDuration = 2; // seconds
                editor.ApplyChanges();

                // Page 2 – assumed image content – apply Dissolve transition
                editor.ProcessPages = new int[] { 2 };
                editor.TransitionType = PdfPageEditor.DISSOLVE;
                editor.TransitionDuration = 2;
                editor.ApplyChanges();

                // Page 3 – assumed table content – apply Out Vertical Split transition
                editor.ProcessPages = new int[] { 3 };
                editor.TransitionType = PdfPageEditor.SPLITVOUT;
                editor.TransitionDuration = 2;
                editor.ApplyChanges();

                // Save the modified PDF
                doc.Save(outputPath);
            }
        }

        Console.WriteLine("Page transitions applied and saved to " + outputPath);
    }
}