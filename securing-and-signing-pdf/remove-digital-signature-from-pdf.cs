using System;
using System.IO;
using Aspose.Pdf;

class RemoveSignatureExample
{
    static void Main()
    {
        // Input PDF path (may contain a digital signature)
        const string inputPath = "signed_input.pdf";

        // Output PDF path (signature will be removed)
        const string outputPath = "unsigned_output.pdf";

        // Optional password if the PDF is protected (empty string if not needed)
        const string password = ""; // e.g., "ownerPassword"

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (with password if required)
            using (Document doc = string.IsNullOrEmpty(password)
                                   ? new Document(inputPath)
                                   : new Document(inputPath, password))
            {
                // Enable signature sanitization – this removes signature fields
                // when the document is saved, provided the current privileges allow it.
                doc.EnableSignatureSanitization = true;

                // Save the modified document; the signature data will be stripped.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Signature removed successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}