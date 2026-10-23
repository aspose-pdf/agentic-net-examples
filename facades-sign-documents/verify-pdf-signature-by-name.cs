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

        try
        {
            // Use PdfFileSignature (the correct class for signature operations in Aspose.Pdf.Facades)
            using (PdfFileSignature pdfSignature = new PdfFileSignature())
            {
                // Load the PDF into the signature handler
                pdfSignature.BindPdf(pdfPath);

                // Verify whether a signature named 'ContractSigner' exists and is valid
                bool isSigned = pdfSignature.VerifySigned("ContractSigner");

                Console.WriteLine($"Signature 'ContractSigner' present and valid: {isSigned}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Verification error: {ex.Message}");
        }
    }
}
