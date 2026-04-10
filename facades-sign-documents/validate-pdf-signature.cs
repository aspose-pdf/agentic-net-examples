using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf"; // PDF containing the signature field "ManagerSignature"

        // Ensure the file exists before proceeding
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfFileSignature facade to work with digital signatures
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            // Bind the PDF file to the facade
            pdfSignature.BindPdf(inputPdf);

            // Verify the integrity of the signature named "ManagerSignature"
            bool isSignatureValid = pdfSignature.VerifySignature("ManagerSignature");

            // Output the verification result
            Console.WriteLine($"Signature 'ManagerSignature' valid: {isSignatureValid}");
        }
    }
}