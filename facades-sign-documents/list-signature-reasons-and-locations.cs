using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class SignatureAuditUtility
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF file with the PdfFileSignature facade
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPath);

            // Get all active (non‑empty) signature names
            IList<SignatureName> signatureNames = pdfSign.GetSignatureNames();

            Console.WriteLine($"Found {signatureNames.Count} signature(s):");

            foreach (SignatureName sigName in signatureNames)
            {
                // Retrieve reason and location for each signature
                string reason   = pdfSign.GetReason(sigName);
                string location = pdfSign.GetLocation(sigName);

                Console.WriteLine($"Signature: {sigName}");
                Console.WriteLine($"  Reason  : {reason}");
                Console.WriteLine($"  Location: {location}");
            }
        }
    }
}