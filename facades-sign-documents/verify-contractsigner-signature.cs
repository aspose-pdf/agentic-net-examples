using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF to the PdfFileSignature facade
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(pdfPath);

            // Verify that the document contains at least one digital signature
            if (!pdfSign.ContainsSignature())
            {
                Console.WriteLine("No digital signatures found in the document.");
                return;
            }

            // Retrieve all signature names present in the PDF
            var signatureNames = pdfSign.GetSignatureNames(); // returns SignatureName[]

            bool hasContractSigner = false;
            bool isSignatureValid = false;

            foreach (SignatureName sig in signatureNames)
            {
                // SignatureName exposes the actual name via the Name property
                if (string.Equals(sig.Name, "ContractSigner", StringComparison.OrdinalIgnoreCase))
                {
                    hasContractSigner = true;
                    // Use the non‑obsolete VerifySignature method instead of VerifySigned
                    isSignatureValid = pdfSign.VerifySignature(sig.Name);
                    break;
                }
            }

            if (hasContractSigner)
            {
                Console.WriteLine($"Signature 'ContractSigner' found. Valid: {isSignatureValid}");
            }
            else
            {
                Console.WriteLine("Signature 'ContractSigner' not found.");
            }
        }
    }
}
