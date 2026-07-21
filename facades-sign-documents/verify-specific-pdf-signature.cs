using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfFileSignature facade to work with signatures
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the PDF file
            pdfSign.BindPdf(inputPath);

            // Retrieve all non‑empty signature names
            var signatureNames = pdfSign.GetSignatureNames();

            bool signatureFound = false;
            bool verificationResult = false;

            // Look for the specific signature name
            foreach (var sigName in signatureNames)
            {
                // SignatureName can be converted to string via ToString()
                if (sigName.ToString().Equals("ContractSigner", StringComparison.Ordinal))
                {
                    signatureFound = true;
                    // Verify the signature using VerifySigned method
                    verificationResult = pdfSign.VerifySigned(sigName.ToString());
                    break;
                }
            }

            if (!signatureFound)
            {
                Console.WriteLine("Signature 'ContractSigner' not found in the document.");
            }
            else
            {
                Console.WriteLine($"Signature 'ContractSigner' verification result: {verificationResult}");
            }
        }
    }
}