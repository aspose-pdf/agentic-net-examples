using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for SignatureName type

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileSignature implements IDisposable, so use a using block
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Load the PDF document for signature inspection
            pdfSign.BindPdf(inputPath);

            // Get all non‑empty signature names
            var signatureNames = pdfSign.GetSignatureNames();

            // Iterate through each signature and output its reason and location
            for (int i = 0; i < signatureNames.Count; i++)
            {
                SignatureName sigName = signatureNames[i];

                // Retrieve metadata for the current signature
                string reason   = pdfSign.GetReason(sigName);
                string location = pdfSign.GetLocation(sigName);

                // Output the information
                Console.WriteLine($"Signature: {sigName}");
                Console.WriteLine($"  Reason  : {reason}");
                Console.WriteLine($"  Location: {location}");
            }
        }
    }
}