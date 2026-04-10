using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "aligned_page3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the PdfPageEditor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Apply alignment only to page 3 (1‑based indexing)
            editor.ProcessPages = new int[] { 3 };

            // Center the original content vertically on the result page
            // The correct enum member is Center (not Middle)
            editor.VerticalAlignmentType = VerticalAlignment.Center;

            // (Optional) Center horizontally as well
            // editor.HorizontalAlignment = HorizontalAlignment.Center;

            // Apply the changes and save the result
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 3 content aligned to middle vertically and saved as '{outputPath}'.");
    }
}
