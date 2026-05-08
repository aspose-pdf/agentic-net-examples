using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Ensure there is a source PDF. If "input.pdf" does not exist, create a simple one.
        const string sourcePath = "input.pdf";
        const string destinationPath = "output.pdf";

        if (!File.Exists(sourcePath))
        {
            // Create a minimal PDF with a single blank page.
            using (Document doc = new Document())
            {
                // Add a page of default size (A4).
                doc.Pages.Add();
                doc.Save(sourcePath);
            }
        }

        // Create resize parameters with uniform 15 % margins on all sides.
        // The static factory method MarginsPercent expects margins expressed as percentages of the original page size.
        PdfFileEditor.ContentsResizeParameters parameters = PdfFileEditor.ContentsResizeParameters.MarginsPercent(
            left:   15,   // left margin = 15 % of page width
            right:  15,   // right margin = 15 % of page width
            top:    15,   // top margin = 15 % of page height
            bottom: 15    // bottom margin = 15 % of page height
        );

        // Apply the parameters to resize the contents of the PDF file.
        PdfFileEditor editor = new PdfFileEditor();
        // Resize all pages (pages array = null) using the uniform 15 % margins.
        editor.ResizeContents(sourcePath, destinationPath, pages: null, parameters);

        Console.WriteLine($"Resized PDF saved to '{destinationPath}'.");
    }
}
