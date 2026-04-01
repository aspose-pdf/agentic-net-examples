using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string signatureFieldName = "ManagerSignature";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPath);
            // Use the overload that accepts the signature name as a string.
            bool isSignatureValid = pdfSignature.VerifySignature(signatureFieldName);
            Console.WriteLine($"Signature '{signatureFieldName}' validity: {isSignatureValid}");
        }
    }
}