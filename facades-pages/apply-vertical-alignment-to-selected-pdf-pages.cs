using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "aligned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the page editor and bind the source PDF
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Specify which pages to modify (example: pages 1, 2, and 3)
            editor.ProcessPages = new int[] { 1, 2, 3 };

            // Align the original content to the top of the result pages
            // The VerticalAlignment enum resides in the Aspose.Pdf namespace, not Facades.
            editor.VerticalAlignmentType = Aspose.Pdf.VerticalAlignment.Top;

            // Apply the alignment changes
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Vertical alignment applied and saved to '{outputPath}'.");
    }
}
