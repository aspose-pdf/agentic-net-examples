using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // for SignatureName type

class SignatureAuditUtility
{
    static void Main()
    {
        // Path to the PDF file to be audited
        const string inputPdf = "input.pdf";

        // Ensure the file exists before proceeding
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileSignature facade
        PdfFileSignature pdfSign = new PdfFileSignature();

        // Bind the PDF document for reading signatures
        pdfSign.BindPdf(inputPdf);

        // Retrieve all non‑empty signature names
        IList<SignatureName> signatureNames = pdfSign.GetSignatureNames();

        // Iterate over each signature and output its reason and location
        foreach (SignatureName sigName in signatureNames)
        {
            // Get the reason and location metadata for the current signature
            string reason   = pdfSign.GetReason(sigName);
            string location = pdfSign.GetLocation(sigName);

            // Output the information (SignatureName.ToString() provides the name)
            Console.WriteLine($"Signature: {sigName} | Reason: {reason} | Location: {location}");
        }

        // Release resources held by the facade
        pdfSign.Close();
    }
}