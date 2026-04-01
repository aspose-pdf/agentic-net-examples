using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        const string outputPath = "LegalSignature.cer";
        const string signatureFieldName = "LegalSignature";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPath);

            // ExtractCertificate overload accepts the signature field name as a string.
            using (Stream certificateStream = pdfSignature.ExtractCertificate(signatureFieldName))
            {
                if (certificateStream == null)
                {
                    Console.WriteLine("No certificate found in the specified signature field.");
                    return;
                }

                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    certificateStream.CopyTo(fileStream);
                }
            }
        }

        Console.WriteLine($"Certificate extracted and saved to '{outputPath}'.");
    }
}
