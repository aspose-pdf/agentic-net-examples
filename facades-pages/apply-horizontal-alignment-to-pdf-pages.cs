using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "aligned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the editor and bind the source PDF
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Apply left horizontal alignment to all pages
        editor.HorizontalAlignment = HorizontalAlignment.Left;

        // Commit the changes
        editor.ApplyChanges();

        // Save the result
        editor.Save(outputPath);

        // Release resources
        editor.Close();

        Console.WriteLine($"Horizontal alignment applied. Saved to '{outputPath}'.");
    }
}