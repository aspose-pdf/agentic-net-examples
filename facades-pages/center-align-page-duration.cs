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

        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Apply changes only to page 5
            editor.ProcessPages = new int[] { 5 };

            // Center‑align the content on the selected page
            editor.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;

            // Set the display duration of the selected page to 4 seconds
            editor.DisplayDuration = 4;

            // Commit the modifications
            editor.ApplyChanges();

            // Save the edited PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page alignment and duration set. Saved to '{outputPath}'.");
    }
}