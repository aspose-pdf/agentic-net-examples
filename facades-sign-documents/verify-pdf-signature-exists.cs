using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string signatureName = "ContractSigner";

        // Ensure the PDF file exists before attempting to bind it
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"File '{inputPath}' not found.");
            return;
        }

        // Load the PDF and bind it to the signature facade
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPath);

            // Get all existing signature names
            IList<SignatureName> signatureNames = pdfSignature.GetSignatureNames();

            bool exists = false;
            foreach (SignatureName name in signatureNames)
            {
                if (name.ToString() == signatureName)
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                Console.WriteLine($"Signature '{signatureName}' not found.");
                return;
            }

            // Verify the specified signature using the non‑obsolete API
            bool isValid = pdfSignature.VerifySignature(signatureName);
            Console.WriteLine($"Signature '{signatureName}' verification result: {isValid}");
        }
    }
}