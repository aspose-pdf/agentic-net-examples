using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        const string outputPath = "modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the signed PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document contains at least one digital signature
            using (PdfFileSignature sigChecker = new PdfFileSignature())
            {
                sigChecker.BindPdf(inputPath);
                bool hasSignature = sigChecker.ContainsSignature();
                Console.WriteLine($"Document contains signature: {hasSignature}");
            }

            // Edit pages with PdfPageEditor while preserving existing signatures
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the already loaded Document instance
                editor.BindPdf(doc);

                // Example modifications:
                // - Rotate page 2 by 90 degrees
                // - Set a custom page size for page 1 (A4)
                // - Process only the first three pages
                // - Keep zoom at 100 % (integer percentage required)
                editor.PageRotations = new Dictionary<int, int>
                {
                    { 2, 90 } // page number (1‑based) => rotation angle
                };
                editor.PageSize = new PageSize(595f, 842f); // width, height in points (A4) – float literals required
                editor.ProcessPages = new int[] { 1, 2, 3 };
                editor.Zoom = 100; // 100 % zoom – integer required

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save using the facade's Save method.
                // This performs an incremental update, preserving existing signatures.
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
