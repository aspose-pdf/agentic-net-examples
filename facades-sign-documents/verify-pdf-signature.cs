using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string signatureName = "ContractSigner";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        using (Document document = new Document(inputPath))
        {
            using (PdfFileSignature pdfSignature = new PdfFileSignature())
            {
                pdfSignature.BindPdf(document);

                bool hasAnySignature = pdfSignature.ContainsSignature();
                Console.WriteLine("Document contains any signature: " + hasAnySignature);

                bool isSignatureValid = pdfSignature.VerifySigned(signatureName);
                Console.WriteLine("Signature '" + signatureName + "' verification result: " + isSignatureValid);
            }
        }
    }
}