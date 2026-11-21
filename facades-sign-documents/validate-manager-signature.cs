using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string signatureFieldName = "ManagerSignature";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF document to the PdfFileSignature facade.
        using (PdfFileSignature signatureFacade = new PdfFileSignature())
        {
            signatureFacade.BindPdf(pdfPath);

            // Retrieve the list of signature names (only active signatures).
            // GetSignatureNames returns IList<SignatureName> – we must work with the
            // SignatureName objects, not raw strings.
            bool isValid = false;
            foreach (SignatureName sigName in signatureFacade.GetSignatureNames(true))
            {
                // SignatureName's string representation (ToString) holds the field name.
                if (sigName.ToString().Equals(signatureFieldName, StringComparison.OrdinalIgnoreCase))
                {
                    // Verify using the overload that accepts a SignatureName instance.
                    isValid = signatureFacade.VerifySignature(sigName);
                    break;
                }
            }

            Console.WriteLine($"Signature field '{signatureFieldName}' is valid: {isValid}");
            // No explicit UnbindPdf call is required – the using statement disposes the facade and unbinds the document.
        }
    }
}
