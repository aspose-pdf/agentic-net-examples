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

        try
        {
            // Bind the PDF and specify the page(s) to edit.
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(inputPath);

                // Align the content of page 3 to the centre both vertically and horizontally.
                editor.VerticalAlignmentType = VerticalAlignment.Center;
                editor.HorizontalAlignment = HorizontalAlignment.Center;
                editor.ProcessPages = new int[] { 3 }; // 1‑based page numbers

                editor.Save(outputPath);
                editor.Close();
            }

            Console.WriteLine($"Page 3 content aligned to middle vertically and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
