using System;
using System.IO;
using Aspose.Pdf;               // HorizontalAlignment enum
using Aspose.Pdf.Facades;      // PdfPageEditor

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "aligned_page2.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Edit the PDF using PdfPageEditor (facade API)
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Process only page 2 (Aspose.Pdf uses 1‑based indexing)
            editor.ProcessPages = new int[] { 2 };

            // Set horizontal alignment to left (explicitly)
            editor.HorizontalAlignment = HorizontalAlignment.Left;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 2 left‑aligned PDF saved to '{outputPath}'.");
    }
}