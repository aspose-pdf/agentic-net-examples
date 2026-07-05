using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "signed_input.pdf";
        const string outputPath = "signed_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the signed PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the page editor and bind the document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Example modification: rotate page 2 by 90 degrees
                editor.Rotation = 90;                 // rotation angle (0, 90, 180, 270)
                editor.ProcessPages = new int[] { 2 }; // apply only to page 2

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save using incremental update to preserve existing signatures
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}