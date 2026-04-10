using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_page2.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the facade and bind the source PDF
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Rotate page 2 by 90 degrees using the PageRotations dictionary
            editor.PageRotations[2] = 90;

            // Apply the rotation changes
            editor.ApplyChanges();

            // Save the resulting PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 2 rotated and saved to '{outputPath}'.");
    }
}