using System;
using System.IO;
using Aspose.Pdf.Facades;

class ExtractSignatureCertificate
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // PDF containing the signature field
        const string signatureName = "LegalSignature";         // Name of the signature field
        const string outputCerPath = "LegalSignature.cer";     // Destination for the extracted certificate

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // PdfFileSignature is a facade for working with digital signatures.
        // It implements IDisposable, so we wrap it in a using block for deterministic cleanup.
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            // Bind the PDF document to the facade.
            pdfSignature.BindPdf(inputPdfPath);

            // Extract the certificate stream for the specified signature field.
            // ExtractCertificate returns null if the signature or certificate is missing.
            using (Stream certStream = pdfSignature.ExtractCertificate(signatureName))
            {
                if (certStream == null)
                {
                    Console.Error.WriteLine($"Certificate not found for signature '{signatureName}'.");
                    return;
                }

                // Write the certificate stream to a .cer file.
                using (FileStream fileStream = new FileStream(outputCerPath, FileMode.Create, FileAccess.Write))
                {
                    certStream.CopyTo(fileStream);
                }

                Console.WriteLine($"Certificate extracted and saved to '{outputCerPath}'.");
            }
        }
    }
}