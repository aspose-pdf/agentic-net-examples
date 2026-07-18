using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_center.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the PdfPageEditor facade and bind the source PDF
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(inputPath);

                // Set horizontal alignment for the page content.
                // Options: HorizontalAlignment.Left, .Center, .Right
                editor.HorizontalAlignment = HorizontalAlignment.Center;

                // Apply the alignment changes to the document.
                editor.ApplyChanges();

                // Save the modified PDF.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Alignment applied and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}