using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // PDF containing the 'LegalSignature' field
        const string outputCer = "LegalSignature.cer"; // Destination for the extracted certificate

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind to the PDF and extract the certificate from the specified signature field
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // ExtractCertificate returns a Stream containing the DER‑encoded X.509 certificate
            using (Stream certStream = pdfSignature.ExtractCertificate("LegalSignature"))
            {
                if (certStream == null)
                {
                    Console.Error.WriteLine("Certificate not found in the 'LegalSignature' field.");
                    return;
                }

                // Save the certificate stream to a .cer file
                using (FileStream fileStream = new FileStream(outputCer, FileMode.Create, FileAccess.Write))
                {
                    certStream.CopyTo(fileStream);
                }
            }
        }

        Console.WriteLine($"Certificate extracted and saved to '{outputCer}'.");
    }
}