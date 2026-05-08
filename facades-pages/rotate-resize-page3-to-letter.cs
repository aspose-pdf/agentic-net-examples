using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document within a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Use PdfPageEditor inside a using block (it implements IDisposable)
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);                     // Bind the loaded document

                // Specify that only page 3 should be processed
                editor.ProcessPages = new int[] { 3 };

                // Set rotation to 90 degrees – the Rotation enum must be cast to int
                editor.Rotation = (int)Rotation.on90;

                // Change page size to Letter – use explicit dimensions (Letter = 8.5" x 11" = 612 x 792 points)
                editor.PageSize = new PageSize(612, 792);

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the modified document
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Page 3 rotated 90° and resized to Letter saved as '{outputPath}'.");
    }
}
