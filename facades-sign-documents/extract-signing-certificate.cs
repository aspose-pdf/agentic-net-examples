using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed.pdf";
        const string outputCer = "LegalSignature.cer";
        const string signatureFieldName = "LegalSignature";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdf);

            // ExtractCertificate overload accepts the signature field name as a string.
            using (Stream certStream = pdfSignature.ExtractCertificate(signatureFieldName))
            {
                if (certStream != null)
                {
                    using (FileStream fileStream = new FileStream(outputCer, FileMode.Create, FileAccess.Write))
                    {
                        certStream.CopyTo(fileStream);
                    }
                    Console.WriteLine("Certificate extracted to " + outputCer);
                }
                else
                {
                    Console.WriteLine("No certificate found in the signature field.");
                }
            }
        }
    }
}
