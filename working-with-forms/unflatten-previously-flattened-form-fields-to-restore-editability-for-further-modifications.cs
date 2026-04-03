using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string flattenedPath = "flattened.pdf";   // PDF that was previously flattened
        const string originalPath  = "original.pdf";    // Original PDF with editable form fields
        const string outputPath    = "restored.pdf";    // Destination for the restored PDF

        if (!File.Exists(flattenedPath))
        {
            Console.Error.WriteLine($"File not found: {flattenedPath}");
            return;
        }

        // Load the possibly flattened document
        using (Document doc = new Document(flattenedPath))
        {
            // After flattening, the Form collection is empty.
            // There is no API to reverse flattening, so we must reload the original PDF.
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("Document is flattened – form fields have been removed.");

                if (!File.Exists(originalPath))
                {
                    Console.Error.WriteLine($"Original PDF not found: {originalPath}");
                    return;
                }

                // Load the original PDF that still contains the form fields
                using (Document original = new Document(originalPath))
                {
                    // Save the original (editable) version to the desired output location
                    original.Save(outputPath);
                    Console.WriteLine($"Restored editable PDF saved to '{outputPath}'.");
                }
            }
            else
            {
                // The document still contains editable fields; simply save it.
                doc.Save(outputPath);
                Console.WriteLine($"Document already contains editable fields. Saved to '{outputPath}'.");
            }
        }
    }
}