using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "aligned_output.pdf";

        // Ensure the source PDF exists – create a minimal one if it does not.
        if (!System.IO.File.Exists(inputPath))
        {
            using (var seed = new Document())
            {
                var page = seed.Pages.Add();
                page.Paragraphs.Add(new TextFragment("Sample content for alignment demo."));
                seed.Save(inputPath);
            }
        }

        // Choose the desired horizontal alignment for the page content.
        // Options: HorizontalAlignment.Left, HorizontalAlignment.Center, HorizontalAlignment.Right
        HorizontalAlignment alignment = HorizontalAlignment.Center; // example: center alignment

        // PdfPageEditor implements IDisposable, so wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF document.
            editor.BindPdf(inputPath);

            // Set the horizontal alignment of the original content on the result page.
            editor.HorizontalAlignment = alignment;

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with {alignment} alignment to '{outputPath}'.");
    }
}
