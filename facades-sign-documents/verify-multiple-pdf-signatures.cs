using System;
using System.Collections.Generic;
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
            Console.WriteLine($"File '{inputPdf}' not found.");
            return;
        }

        // Load the PDF and work with its signatures
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // Retrieve all non‑empty signature names
            IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames();

            // Verify each signature and output the result
            foreach (SignatureName sigName in signatureNames)
            {
                // SignatureName exposes the actual name via the Name property
                string name = sigName.Name;
                // Use the non‑obsolete VerifySignature method
                bool isValid = pdfSignature.VerifySignature(name);
                Console.WriteLine($"Signature '{name}': Valid = {isValid}");
            }
        }
    }
}