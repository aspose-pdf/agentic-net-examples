using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed.pdf";          // path to the signed PDF
        const string signatureName = "Signature1";     // name of the signature field to inspect

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the signature field by its name.
            // The Form indexer returns a generic Field; cast it to SignatureField.
            SignatureField sigField = doc.Form[signatureName] as SignatureField;

            if (sigField == null)
            {
                Console.WriteLine($"Signature field '{signatureName}' not found.");
                return;
            }

            // The Signature object holds the signing date in local time.
            DateTime localSigningTime = sigField.Signature.Date;

            // Convert the local signing time to UTC.
            DateTime utcSigningTime = localSigningTime.ToUniversalTime();

            // Output the result in ISO 8601 format.
            Console.WriteLine($"Signature '{signatureName}' signing time (UTC): {utcSigningTime:O}");
        }
    }
}