using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string flattenedPdfPath = "flattened.pdf";   // PDF that was previously flattened
        const string originalPdfPath   = "original.pdf";   // Backup copy with editable form fields
        const string restoredPdfPath   = "restored.pdf";   // Output path

        // Verify the flattened file exists
        if (!File.Exists(flattenedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {flattenedPdfPath}");
            return;
        }

        // Load the possibly flattened document
        using (Document doc = new Document(flattenedPdfPath))
        {
            // If the document has no form fields, it was flattened.
            // The only way to "unflatten" is to reload the original PDF that still contains the fields.
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("Document appears flattened (no form fields). Reloading original PDF.");

                // Load the original (unflattened) PDF and save it as the restored version.
                using (Document original = new Document(originalPdfPath))
                {
                    original.Save(restoredPdfPath);
                }
            }
            else
            {
                // The document still contains editable fields; simply save it.
                doc.Save(restoredPdfPath);
            }
        }

        Console.WriteLine($"Restored PDF saved to '{restoredPdfPath}'.");
    }
}