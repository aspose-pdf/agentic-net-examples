using System;
using System.IO;
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
            editor.BindPdf(inputPath);

            int pageCount = editor.GetPages();

            for (int i = 1; i <= pageCount; i++)
            {
                // Example: start at 0.5x zoom and increase by 0.1x for each subsequent page
                float zoomFactor = 0.5f + (float)(i - 1) * 0.1f;
                editor.ProcessPages = new int[] { i };
                editor.Zoom = zoomFactor;
                editor.ApplyChanges();
            }

            editor.Save(outputPath);
        }

        Console.WriteLine($"Zoom factors applied and saved to '{outputPath}'.");
    }
}
