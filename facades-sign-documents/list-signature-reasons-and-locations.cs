using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // for SignatureName type

class SignatureAuditUtility
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create the PdfFileSignature facade and bind the PDF document
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPdf);

            // Retrieve all non‑empty signature names
            var signatureNames = pdfSign.GetSignatureNames();

            // Iterate over each signature and output its reason and location
            foreach (SignatureName sigName in signatureNames)
            {
                string reason   = pdfSign.GetReason(sigName);
                string location = pdfSign.GetLocation(sigName);

                Console.WriteLine($"Signature: {sigName}");
                Console.WriteLine($"  Reason:   {reason}");
                Console.WriteLine($"  Location: {location}");
            }
        }
    }
}