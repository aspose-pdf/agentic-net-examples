using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "aligned_page3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade and bind the loaded document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Apply vertical middle alignment only to page 3
                editor.ProcessPages = new int[] { 3 };
                // Correct enum member – use Center instead of non‑existent Middle
                editor.VerticalAlignmentType = VerticalAlignment.Center;

                // Apply the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with vertically centered content on page 3: '{outputPath}'.");
    }
}
