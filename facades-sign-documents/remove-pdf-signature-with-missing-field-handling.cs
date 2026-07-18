using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, name of the signature to remove, and output PDF paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_removed.pdf";
        const string signatureName = "Signature1";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the PdfFileSignature facade
            using (PdfFileSignature pdfSign = new PdfFileSignature())
            {
                // Bind the existing PDF document
                pdfSign.BindPdf(inputPdf);

                // Retrieve the list of existing signature names
                var existingNames = pdfSign.GetSignatureNames();

                // Check whether the requested signature exists (case‑insensitive)
                bool found = false;
                foreach (var nameObj in existingNames)
                {
                    // Get the string representation safely (may be null)
                    string? name = nameObj?.ToString();
                    if (string.Equals(name, signatureName, StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    // Signature exists – remove it (remove only the signature, keep the field)
                    // Use the overload that accepts a string name (non‑obsolete)
                    pdfSign.RemoveSignature(signatureName);
                    Console.WriteLine($"Signature '{signatureName}' removed.");
                }
                else
                {
                    // Signature not found – handle gracefully
                    Console.WriteLine($"Signature '{signatureName}' does not exist in the document.");
                }

                // Save the resulting PDF
                pdfSign.Save(outputPdf);
                Console.WriteLine($"Result saved to '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            // General error handling (e.g., corrupted PDF, I/O issues)
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
