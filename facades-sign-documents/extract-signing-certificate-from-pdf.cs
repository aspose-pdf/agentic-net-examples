using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCer = "LegalSignature.cer";
        const string signatureName = "LegalSignature";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF and extract the certificate from the specified signature field
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // ExtractCertificate returns a Stream containing the DER‑encoded X.509 certificate
            using (Stream certStream = pdfSignature.ExtractCertificate(signatureName))
            {
                if (certStream == null)
                {
                    Console.Error.WriteLine($"No certificate found in signature '{signatureName}'.");
                    return;
                }

                // Write the certificate stream to a .cer file
                using (FileStream file = new FileStream(outputCer, FileMode.Create, FileAccess.Write))
                {
                    certStream.CopyTo(file);
                }
            }
        }

        Console.WriteLine($"Certificate extracted to '{outputCer}'.");
    }
}