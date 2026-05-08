using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string pfxPath    = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure signatures are appended only (incremental updates)
            doc.Form.SignaturesAppendOnly = true;

            // Define a tiny rectangle – the signature will be invisible
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 1, 1);

            // Create the signature field on the first page
            SignatureField sigField = new SignatureField(doc, rect);

            // Prepare the PKCS#7 signature object
            PKCS7 pkcs7 = new PKCS7
            {
                ShowProperties = false,          // hide visual appearance
                Date = DateTime.Now              // record signing time
            };

            // Sign the document using the certificate
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                sigField.Sign(pkcs7, pfxStream, pfxPassword);
            }

            // Save the PDF – default Save() performs an incremental update,
            // preserving the original layout.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}