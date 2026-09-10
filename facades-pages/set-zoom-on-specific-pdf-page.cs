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

        // Initialize the page editor and bind the source PDF
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Specify that only page 7 should be edited
        editor.ProcessPages = new int[] { 7 };

        // Set zoom factor to 2.0 (200% magnification)
        editor.Zoom = 2.0f;

        // Apply the changes and save the result
        editor.ApplyChanges();
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Page 7 zoom set to 2.0 and saved to '{outputPath}'.");
    }
}