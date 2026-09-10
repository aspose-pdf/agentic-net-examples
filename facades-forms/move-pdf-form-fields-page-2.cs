using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to shift the content (including form fields) on page 2
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Specify that only page 2 should be processed
            editor.ProcessPages = new int[] { 2 };

            // Move the origin 5 points to the right (X axis), Y stays unchanged
            editor.MovePosition(5f, 0f);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Fields on page 2 moved 5 points right. Saved to '{outputPath}'.");
    }
}