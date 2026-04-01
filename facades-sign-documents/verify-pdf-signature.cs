using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            PdfFileSignature signatureFacade = new PdfFileSignature(document);
            bool containsSignature = signatureFacade.ContainsSignature();
            Console.WriteLine($"Document contains signature: {containsSignature}");

            bool isSignatureValid = signatureFacade.VerifySigned("ContractSigner");
            Console.WriteLine($"Signature 'ContractSigner' valid: {isSignatureValid}");
        }
    }
}