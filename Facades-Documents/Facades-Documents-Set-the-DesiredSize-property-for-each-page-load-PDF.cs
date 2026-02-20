using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for PageSize

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade for page editing
            PdfPageEditor pageEditor = new PdfPageEditor();

            // Load the PDF document
            pageEditor.BindPdf(inputPath);

            // Total number of pages in the document
            int pageCount = pageEditor.Document.Pages.Count;

            // Set the desired size for each page (example: A4 size)
            for (int i = 1; i <= pageCount; i++)
            {
                // Retrieve A4 dimensions (width & height) and apply them
                var a4Size = PageSize.A4; // SizeF with Width and Height
                pageEditor.Document.Pages[i].SetPageSize(a4Size.Width, a4Size.Height);
            }

            // Save the modified PDF
            pageEditor.Save(outputPath);
            Console.WriteLine($"PDF saved with desired page sizes to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
