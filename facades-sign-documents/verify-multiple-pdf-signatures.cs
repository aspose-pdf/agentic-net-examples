using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed_document.pdf";

        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Bind the PDF to the signature facade
            PdfFileSignature signatureFacade = new PdfFileSignature();
            signatureFacade.BindPdf(pdfPath);

            // Retrieve all signature names (true = include inactive signatures)
            IList<SignatureName> signatureNames = signatureFacade.GetSignatureNames(true);

            if (signatureNames == null || signatureNames.Count == 0)
            {
                Console.WriteLine("No signatures were found in the document.");
                return;
            }

            // Verify each signature using VerifySigned and report its status
            foreach (SignatureName sigName in signatureNames)
            {
                // VerifySigned expects the signature name as a string, not the SignatureName object
                bool isValid = signatureFacade.VerifySigned(sigName.Name);
                Console.WriteLine($"Signature '{sigName.Name}': {(isValid ? "Valid" : "Invalid")}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during verification: {ex.Message}");
        }
    }
}
