using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_document.pdf";

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the PdfFileSignature facade and bind the PDF file
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPdf);

            // Retrieve all non‑empty signature names
            IList<SignatureName> signatureNames = pdfSign.GetSignatureNames();

            // Iterate through each signature and verify it
            foreach (SignatureName sigName in signatureNames)
            {
                // VerifySigned expects a string (the signature name), not a SignatureName object.
                // The SignatureName class exposes the actual name via the "Name" property.
                bool isValid = pdfSign.VerifySigned(sigName.Name);
                Console.WriteLine($"Signature '{sigName.Name}': {(isValid ? "Valid" : "Invalid")}");
            }
        }
    }
}
