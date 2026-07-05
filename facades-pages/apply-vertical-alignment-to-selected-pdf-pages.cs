using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path
        const string outputPath = "aligned_output.pdf";

        // Pages to which the vertical alignment will be applied (1‑based indexing)
        int[] selectedPages = new int[] { 1, 2, 3 }; // adjust as needed

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (a SaveableFacade) within a using block for deterministic disposal
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF document
            editor.BindPdf(inputPath);

            // Specify which pages to edit
            editor.ProcessPages = selectedPages;

            // Set vertical alignment to Top for consistent top positioning
            // Use the new VerticalAlignment property that expects VerticalAlignmentType
            editor.VerticalAlignment = VerticalAlignmentType.Top;

            // Apply the changes to the selected pages
            editor.ApplyChanges();

            // Save the modified PDF to the output path
            editor.Save(outputPath);
        }

        Console.WriteLine($"Vertical alignment applied. Output saved to '{outputPath}'.");
    }
}
