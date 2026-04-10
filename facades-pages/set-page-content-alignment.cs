using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "aligned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Choose desired alignment: Left, Center, or Right
        Aspose.Pdf.HorizontalAlignment alignment = Aspose.Pdf.HorizontalAlignment.Center; // change as needed

        // Use PdfPageEditor (Facade) to modify page content alignment
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF document
            editor.BindPdf(inputPath);

            // Set horizontal alignment for the original content on the result pages
            editor.HorizontalAlignment = alignment;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with {alignment} alignment to '{outputPath}'.");
    }
}
