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

        // Load the PDF into the page editor
        PdfPageEditor pageEditor = new PdfPageEditor();
        pageEditor.BindPdf(inputPath);

        // Specify the page(s) to edit – page numbers are 1‑based
        pageEditor.ProcessPages = new int[] { 2 };

        // Set horizontal alignment to left (justification)
        pageEditor.HorizontalAlignment = HorizontalAlignment.Left;

        // Save the modified PDF
        pageEditor.Save(outputPath);
        pageEditor.Close();

        Console.WriteLine($"Page 2 content left‑aligned and saved to '{outputPath}'.");
    }
}
