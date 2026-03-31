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

        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(inputPath);
            // Specify the page(s) to edit using ProcessPages (1‑based indexing)
            pageEditor.ProcessPages = new int[] { 3 };
            // Align the content of the selected page(s) to the top
            pageEditor.VerticalAlignmentType = Aspose.Pdf.VerticalAlignment.Top;
            pageEditor.Save(outputPath);
        }

        Console.WriteLine($"Page 3 content aligned to top and saved as '{outputPath}'.");
    }
}
