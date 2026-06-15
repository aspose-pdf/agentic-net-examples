using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Initialize the page editor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Define the new page size (e.g., A4: 595 x 842 points)
                // You can also use any custom dimensions.
                editor.PageSize = new PageSize(595, 842);

                // Apply the changes – this resizes pages while preserving
                // all existing annotations and interactive elements.
                editor.ApplyChanges();
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}