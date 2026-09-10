using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf"; // PDF containing the 'ManagerSignature' field

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF and bind it to the PdfFileSignature facade
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // Verify the integrity of the signature named 'ManagerSignature'
            bool isSignatureValid = pdfSignature.VerifySignature("ManagerSignature");

            Console.WriteLine($"Signature 'ManagerSignature' valid: {isSignatureValid}");
        }
    }
}