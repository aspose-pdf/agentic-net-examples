using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "aligned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Choose the desired alignment: Left, Center, or Right
        HorizontalAlignment alignment = HorizontalAlignment.Center; // change as needed

        // Use PdfPageEditor (a Facades class) to modify page content alignment
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Set horizontal alignment for the original content on the result pages
            editor.HorizontalAlignment = alignment;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Aligned PDF saved to '{outputPath}'.");
    }
}