using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF that may contain signatures
        const string inputPath  = "signed_input.pdf";
        // Output PDF after attempted removal
        const string outputPath = "signed_output.pdf";
        // Name of the signature we want to remove
        const string signatureToRemove = "Signature1";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create the PdfFileSignature facade and bind the PDF
            PdfFileSignature pdfSign = new PdfFileSignature();
            pdfSign.BindPdf(inputPath);

            // Retrieve all existing (non‑empty) signature names
            IList<SignatureName> existingNames = pdfSign.GetSignatureNames();

            // Flag to indicate whether the requested signature exists
            bool signatureFound = false;

            // Search for the signature by name (SignatureName.ToString() returns the name)
            foreach (SignatureName name in existingNames)
            {
                if (string.Equals(name.ToString(), signatureToRemove, StringComparison.OrdinalIgnoreCase))
                {
                    // Signature exists – remove it (only the signature, keep the field)
                    pdfSign.RemoveSignature(name, false);
                    signatureFound = true;
                    Console.WriteLine($"Signature '{signatureToRemove}' removed.");
                    break;
                }
            }

            if (!signatureFound)
            {
                // Handle the case where the signature field is missing
                Console.WriteLine($"Signature '{signatureToRemove}' does not exist in the document.");
            }

            // Save the resulting PDF
            pdfSign.Save(outputPath);
            Console.WriteLine($"Result saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // General error handling – any unexpected issues are reported here
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}