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

        // PdfPageEditor works with facades and implements IDisposable
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Specify that only page 2 should be processed
            editor.ProcessPages = new int[] { 2 };

            // Move the origin 5 points to the right (X axis), Y unchanged
            editor.MovePosition(5f, 0f);

            // Save the modified document
            editor.Save(outputPath);
        }

        Console.WriteLine($"Fields on page 2 shifted right by 5 points. Saved to '{outputPath}'.");
    }
}