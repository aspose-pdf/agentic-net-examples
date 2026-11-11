using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF that is expected to have no digital signatures
        const string pdfPath = "unsigned.pdf";

        // Verify the file exists before attempting verification
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        bool isSigned;
        using (PdfFileSignature signature = new PdfFileSignature())
        {
            // Bind the PDF file to the signature object
            signature.BindPdf(pdfPath);

            // Retrieve the list of signature names (if any)
            var signatureNames = signature.GetSignatureNames(true);

            // If there is at least one signature, verify the first one; otherwise the PDF is unsigned
            if (signatureNames != null && signatureNames.Count > 0)
            {
                // VerifySignature requires a signature name argument
                isSigned = signature.VerifySignature(signatureNames[0]);
            }
            else
            {
                isSigned = false;
            }
        }

        // For a PDF with no signatures the expected result is false
        Console.WriteLine($"Is the PDF signed? {isSigned}");
    }
}
