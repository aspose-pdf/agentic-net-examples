using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string targetSignature = "ContractSigner";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF to the PdfFileSignature facade
        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(pdfPath);

        // Ensure the document has at least one digital signature
        if (!pdfSignature.ContainsSignature())
        {
            Console.WriteLine("The PDF does not contain any digital signatures.");
            return;
        }

        // Check whether a signature with the specified name exists
        bool nameExists = false;
        foreach (var sigName in pdfSignature.GetSignatureNames())
        {
            if (string.Equals(sigName.ToString(), targetSignature, StringComparison.Ordinal))
            {
                nameExists = true;
                break;
            }
        }

        if (!nameExists)
        {
            Console.WriteLine($"Signature '{targetSignature}' was not found in the PDF.");
            return;
        }

        // Verify the validity of the named signature
        bool isValid = pdfSignature.VerifySigned(targetSignature);
        Console.WriteLine($"Signature '{targetSignature}' validity: {isValid}");
    }
}