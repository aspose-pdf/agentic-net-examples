using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        const string outputPath = "signed_removed.pdf";
        const string targetSignature = "Signature1";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF into the PdfFileSignature facade
        PdfFileSignature pdfSign = new PdfFileSignature();
        pdfSign.BindPdf(inputPath);

        // Get the list of existing (non‑empty) signature names
        IList<SignatureName> existingNames = pdfSign.GetSignatureNames();

        // Check whether the requested signature exists
        bool signatureExists = false;
        foreach (SignatureName name in existingNames)
        {
            if (name.Name.Equals(targetSignature, StringComparison.OrdinalIgnoreCase))
            {
                signatureExists = true;
                break;
            }
        }

        if (signatureExists)
        {
            // Remove the signature; keep the signature field (removeField = false)
            // Use the overload that accepts a string name directly (SignatureName has no public ctor).
            pdfSign.RemoveSignature(targetSignature, false);
            Console.WriteLine($"Signature '{targetSignature}' removed successfully.");
        }
        else
        {
            // Handle missing signature gracefully
            Console.WriteLine($"Signature '{targetSignature}' does not exist. No removal performed.");
        }

        // Save the modified PDF
        pdfSign.Save(outputPath);
        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
