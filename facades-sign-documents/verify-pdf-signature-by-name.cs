using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string signatureName = "ContractSigner";

        if (!System.IO.File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF to the PdfFileSignature facade
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(pdfPath);

            // Verify the signature named 'ContractSigner'
            bool isValid = pdfSign.VerifySigned(signatureName);

            Console.WriteLine($"Signature '{signatureName}' verification result: {isValid}");
        }
    }
}