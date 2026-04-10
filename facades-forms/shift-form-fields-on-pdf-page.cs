using System;
using System.IO;
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

        // Use PdfPageEditor to shift page content (including form fields) on page 2
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);               // Load the PDF
            editor.ProcessPages = new int[] { 2 };   // Target only page 2 (1‑based indexing)
            editor.MovePosition(5f, 0f);             // Move 5 points to the right
            editor.Save(outputPath);                 // Save the modified PDF
            editor.Close();                         // Release resources
        }

        Console.WriteLine($"Fields on page 2 moved 5 points right. Saved to '{outputPath}'.");
    }
}