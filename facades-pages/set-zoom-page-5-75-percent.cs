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

        // Initialize the PdfPageEditor facade and bind the source PDF
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Edit only page 5
        editor.ProcessPages = new int[] { 5 };

        // Set zoom to 75% (0.75 corresponds to 75%)
        editor.Zoom = 0.75f;

        // Apply the changes to the document
        editor.ApplyChanges();

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"Page 5 zoom set to 75% and saved to '{outputPath}'.");
    }
}