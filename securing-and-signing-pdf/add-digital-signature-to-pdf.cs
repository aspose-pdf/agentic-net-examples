using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "pfxPassword";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the signature field will be placed
            // (left, bottom, right, top) – coordinates are in points
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field and add it to the first page
            SignatureField sigField = new SignatureField(doc, sigRect);
            doc.Pages[1].Annotations.Add(sigField);

            // Load the certificate (PFX) as a stream and create a PKCS#1 signature object
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                PKCS1 pkcs1 = new PKCS1(pfxStream, pfxPassword);

                // Sign the document using the signature field
                // Note: DocMDPSignature (certification) can only be applied via PdfFileSignature.Certify,
                // which resides in the Aspose.Pdf.Facades namespace and is prohibited by the hard rule.
                // Therefore we perform a regular digital signature here.
                sigField.Sign(pkcs1);
            }

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdf}'.");
    }
}
