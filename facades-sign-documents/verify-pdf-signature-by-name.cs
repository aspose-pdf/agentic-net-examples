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

        // Load the PDF for signature operations
        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(pdfPath);

        // Verify whether a signature named 'ContractSigner' exists and is valid
        bool hasValidSignature = pdfSignature.VerifySigned("ContractSigner");

        Console.WriteLine(hasValidSignature
            ? "Signature 'ContractSigner' is present and valid."
            : "Signature 'ContractSigner' not found or invalid.");
    }
}