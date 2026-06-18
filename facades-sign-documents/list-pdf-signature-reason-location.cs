using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class SignatureAudit
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the PdfFileSignature facade
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPath);

            // Get all non‑empty signature names
            IList<SignatureName> names = pdfSign.GetSignatureNames();

            if (names == null || names.Count == 0)
            {
                Console.WriteLine("No signatures found.");
                return;
            }

            Console.WriteLine("Signature audit:");
            foreach (SignatureName sigName in names)
            {
                // Retrieve reason and location for each signature
                string reason = pdfSign.GetReason(sigName);
                string location = pdfSign.GetLocation(sigName);

                Console.WriteLine($"- Name: {sigName}");
                Console.WriteLine($"  Reason: {reason}");
                Console.WriteLine($"  Location: {location}");
            }
        }
    }
}