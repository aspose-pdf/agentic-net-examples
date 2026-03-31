using System;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int durationSeconds = 5;

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(inputPath);
            pageEditor.DisplayDuration = durationSeconds;
            pageEditor.ApplyChanges();
            pageEditor.Save(outputPath);
        }

        Console.WriteLine("Display duration set to " + durationSeconds + " seconds per slide. Saved to '" + outputPath + "'.");
    }
}
