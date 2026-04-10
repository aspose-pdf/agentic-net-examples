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

        // Use PdfPageEditor (Facade) to edit page layout
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF document
            editor.BindPdf(inputPath);

            // Specify that only page 3 should be processed
            editor.ProcessPages = new int[] { 3 };

            // Align the original content of page 3 to the top of the result page
            editor.VerticalAlignmentType = VerticalAlignment.Top;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 3 vertically aligned to top and saved as '{outputPath}'.");
    }
}