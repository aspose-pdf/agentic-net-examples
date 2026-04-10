using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";               // source PDF containing the signature field
        const string signatureField = "LegalSignature";   // name of the signature field to extract from
        const string outputCertPath = "LegalSignature.cer"; // destination file for the extracted certificate

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfFileSignature implements IDisposable, so wrap it in a using block
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            // Bind the PDF document to the facade
            pdfSignature.BindPdf(pdfPath);

            // Extract the certificate stream from the specified signature field
            using (Stream certStream = pdfSignature.ExtractCertificate(signatureField))
            {
                if (certStream == null)
                {
                    Console.WriteLine("No certificate found in the specified signature field.");
                    return;
                }

                // Write the certificate bytes to a .cer file
                using (FileStream fileStream = new FileStream(outputCertPath, FileMode.Create, FileAccess.Write))
                {
                    certStream.CopyTo(fileStream);
                }

                Console.WriteLine($"Certificate extracted and saved to '{outputCertPath}'.");
            }
        }
    }
}