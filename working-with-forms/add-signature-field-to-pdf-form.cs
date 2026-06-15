using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_signed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the signature field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the document (first page by default)
            SignatureField sigField = new SignatureField(doc, rect);
            sigField.Name = "Signature1";               // internal field name
            sigField.AlternateName = "Sign Here";       // tooltip shown in PDF viewers

            // Add the signature field to the form
            doc.Form.Add(sigField);

            // Optional: sign the field immediately using a certificate
            // string pfxPath = "certificate.pfx";
            // string pfxPassword = "password";
            // using (FileStream pfxStream = File.OpenRead(pfxPath))
            // {
            //     Signature signature = new Signature(pfxStream, pfxPassword);
            //     sigField.Sign(signature);
            // }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field added and saved to '{outputPath}'.");
    }
}