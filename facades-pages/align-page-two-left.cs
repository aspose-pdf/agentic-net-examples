using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "aligned_page2.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor works with facades; wrap it in a using block for proper disposal
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Specify that only page 2 should be processed
            editor.ProcessPages = new int[] { 2 };

            // Set horizontal alignment to left (default, but set explicitly as requested)
            editor.HorizontalAlignment = HorizontalAlignment.Left;

            // Apply the alignment changes to the selected page(s)
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 2 aligned left and saved to '{outputPath}'.");
    }
}