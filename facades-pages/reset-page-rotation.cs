using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_reset_rotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Reset rotation of page 6 to 0 degrees
                editor.Rotation = 0;                     // default rotation
                editor.ProcessPages = new int[] { 6 };   // apply only to page 6

                // Apply the changes
                editor.ApplyChanges();

                // Save the modified document
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Page 6 rotation reset and saved to '{outputPath}'.");
    }
}