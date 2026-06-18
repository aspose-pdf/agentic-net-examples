using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "unsigned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF file to the PdfFileSignature facade.
        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPath);

        // Verify a signature that does not exist.
        // When the document has no signatures, VerifySigned returns false.
        bool isSignatureValid = pdfSignature.VerifySigned("Signature1");

        Console.WriteLine($"VerifySigned returned: {isSignatureValid}");
    }
}