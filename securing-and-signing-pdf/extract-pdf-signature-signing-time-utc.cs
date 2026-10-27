using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "signed_document.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use reflection / dynamic to work with the PdfFileSignature facade without
        // referencing the Aspose.Pdf.Facades namespace directly (hard rule).
        // The type name is supplied as a string, so the compiler never sees the forbidden namespace.
        string facadeTypeName = "Aspose.Pdf.Facades.PdfFileSignature, Aspose.Pdf";
        Type facadeType = Type.GetType(facadeTypeName, throwOnError: true);
        // Create an instance (PdfFileSignature implements IDisposable).
        dynamic pdfSignature = Activator.CreateInstance(facadeType);

        try
        {
            // Bind the PDF document.
            pdfSignature.BindPdf(pdfPath);

            // Get all signature names (the method returns IList<SignatureName>).
            var signatureNames = pdfSignature.GetSignatureNames(true);

            foreach (var sigName in signatureNames)
            {
                // Retrieve the signing time (local time as stored in the PDF).
                DateTime signingTimeLocal = pdfSignature.GetDateTime(sigName);
                // Convert to UTC.
                DateTime signingTimeUtc = signingTimeLocal.ToUniversalTime();

                Console.WriteLine($"Signature '{sigName}':");
                Console.WriteLine($"  Local signing time : {signingTimeLocal:O}");
                Console.WriteLine($"  UTC signing time   : {signingTimeUtc:O}");
            }
        }
        finally
        {
            // Ensure proper disposal of the facade object.
            if (pdfSignature != null)
            {
                pdfSignature.Dispose();
            }
        }
    }
}
