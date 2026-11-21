using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // for SignatureName

class Program
{
    static void Main()
    {
        // Path to the PDF file to be audited
        const string inputPath = "input.pdf";

        // Verify that the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileSignature provides access to digital signature information.
        // It implements IDisposable, so we use a using block.
        using (PdfFileSignature signatureHandler = new PdfFileSignature())
        {
            // Bind the PDF document to the signature handler.
            signatureHandler.BindPdf(inputPath);

            // Retrieve the list of active signatures.
            // GetSignatureNames(bool onlyActive) returns IList<SignatureName>.
            var signatureNames = signatureHandler.GetSignatureNames(true);
            int signatureCount = signatureNames.Count;

            Console.WriteLine($"Found {signatureCount} signature(s) in '{Path.GetFileName(inputPath)}'.");

            // Iterate over each signature and output its Reason and Location metadata.
            foreach (SignatureName sigName in signatureNames)
            {
                // Reason for signing (e.g., "Approved", "Reviewed")
                string reason = signatureHandler.GetReason(sigName);

                // Physical location description (e.g., "Page 2, top‑right")
                string location = signatureHandler.GetLocation(sigName);

                Console.WriteLine($"Signature '{sigName}':");
                Console.WriteLine($"  Reason  : {reason}");
                Console.WriteLine($"  Location: {location}");
            }
        }
    }
}
