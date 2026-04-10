using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_reset_rotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PDF page editor and bind the source PDF
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Specify the page to edit (page numbers are 1‑based)
        editor.ProcessPages = new int[] { 6 };

        // Reset rotation to 0 degrees (allowed values: 0, 90, 180, 270)
        editor.Rotation = 0;

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Page 6 rotation reset. Saved to '{outputPath}'.");
    }
}