using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";

        // Ensure the PDF file exists before attempting to bind it.
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileSignature facade and bind the PDF file
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // Retrieve the names of all non‑empty signatures in the document
            var signatureInfos = pdfSignature.GetSignatureNames();

            // Iterate through each signature name and verify its validity
            foreach (var sigInfo in signatureInfos)
            {
                // SignatureInfo objects expose the actual name via the "Name" property
                string sigName = sigInfo.Name;
                // Use the non‑obsolete VerifySignature method instead of VerifySigned
                bool isValid = pdfSignature.VerifySignature(sigName);
                Console.WriteLine($"Signature '{sigName}': Valid = {isValid}");
            }
        }
    }
}