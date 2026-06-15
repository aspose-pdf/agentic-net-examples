using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_page4.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to rotate page 4 by 180 degrees
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(inputPath);

            // Specify that only page 4 should be edited
            editor.ProcessPages = new int[] { 4 };

            // Set the rotation (allowed values: 0, 90, 180, 270)
            editor.Rotation = 180;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 4 rotated and saved to '{outputPath}'.");
    }
}