using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "aligned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the page editor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor
                editor.BindPdf(doc);

                // Specify that only page 3 should be edited (pages are 1‑based)
                editor.ProcessPages = new int[] { 3 };

                // Align the original content vertically to the middle of the page
                // Correct enum member is Center (not Middle)
                editor.VerticalAlignmentType = VerticalAlignment.Center;

                // Apply the changes to the document
                editor.ApplyChanges();
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 3 content vertically centered and saved to '{outputPath}'.");
    }
}
