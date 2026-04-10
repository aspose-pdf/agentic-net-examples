using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF and work with signatures using PdfFileSignature facade
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the PDF file to the facade
            pdfSign.BindPdf(inputPdf);

            // Retrieve all non‑empty signature names
            var signatureNames = pdfSign.GetSignatureNames();

            if (signatureNames == null || signatureNames.Count == 0)
            {
                Console.WriteLine("No signatures found in the document.");
                return;
            }

            // Iterate over each signature and output its reason and location
            foreach (var sigName in signatureNames)
            {
                // Get the reason and location metadata for the current signature
                string reason   = pdfSign.GetReason(sigName);
                string location = pdfSign.GetLocation(sigName);

                Console.WriteLine($"Signature: {sigName}");
                Console.WriteLine($"  Reason  : {reason}");
                Console.WriteLine($"  Location: {location}");
                Console.WriteLine();
            }

            // No explicit UnbindPdf call is required – the using block disposes the facade.
        }
    }
}
