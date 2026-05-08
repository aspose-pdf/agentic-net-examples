using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // PDF containing the 'LegalSignature' field
        const string outputCer = "LegalSignature.cer"; // Destination for the extracted certificate

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (Document implements IDisposable)
            using (Document doc = new Document(inputPdf))
            {
                // Initialize the PdfFileSignature facade
                using (PdfFileSignature pdfSign = new PdfFileSignature())
                {
                    // Bind the already loaded Document instance
                    pdfSign.BindPdf(doc);

                    // Extract the certificate stream from the specified signature field
                    Stream certStream = pdfSign.ExtractCertificate("LegalSignature");

                    if (certStream == null)
                    {
                        Console.Error.WriteLine("Certificate not found in the 'LegalSignature' field.");
                        return;
                    }

                    // Ensure the stream is positioned at the beginning
                    if (certStream.CanSeek)
                        certStream.Position = 0;

                    // Write the certificate to a .cer file
                    using (FileStream fileOut = new FileStream(outputCer, FileMode.Create, FileAccess.Write))
                    {
                        certStream.CopyTo(fileOut);
                    }

                    Console.WriteLine($"Certificate extracted and saved to '{outputCer}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}