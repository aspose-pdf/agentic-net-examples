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

        // Adjust zoom of page 3 to 150% using PdfPageEditor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);               // Load the PDF
            editor.ProcessPages = new int[] { 3 };   // Target only page 3
            editor.Zoom = 1.5f;                      // 1.0 = 100%, so 1.5 = 150%
            editor.ApplyChanges();                   // Apply the modifications
            editor.Save(outputPath);                 // Save the result
        }

        Console.WriteLine($"Page 3 zoom set to 150% and saved as '{outputPath}'.");
    }
}