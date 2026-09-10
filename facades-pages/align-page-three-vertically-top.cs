using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_aligned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document within a using block for proper disposal
        using (Document doc = new Document(inputPath))
        // Initialize the PdfPageEditor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the editor to the loaded document
            editor.BindPdf(doc);

            // Specify that only page 3 should be processed
            editor.ProcessPages = new int[] { 3 };

            // Set vertical alignment to Top for the selected page(s)
            editor.VerticalAlignmentType = VerticalAlignment.Top;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified document
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 3 vertically aligned to top and saved as '{outputPath}'.");
    }
}