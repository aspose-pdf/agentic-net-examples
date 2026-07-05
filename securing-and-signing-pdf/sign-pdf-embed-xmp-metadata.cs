using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle for the signature field (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(sigField, 1); // Add the field to the form collection

            // Prepare a PKCS#7 signature using the certificate
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
            {
                Reason = "Document approved",
                Location = "Company HQ",
                ContactInfo = "contact@example.com",
                Date = DateTime.UtcNow
            };

            // Sign the field
            sigField.Sign(pkcs7);

            // Build custom XMP metadata describing the signing process
            string xmp = $@"<?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>
<x:xmpmeta xmlns:x='adobe:ns:meta/'>
  <rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>
    <rdf:Description rdf:about=''
      xmlns:pdf='http://ns.adobe.com/pdf/1.3/'>
      <pdf:Producer>Aspose.Pdf for .NET</pdf:Producer>
    </rdf:Description>
    <rdf:Description rdf:about=''
      xmlns:dc='http://purl.org/dc/elements/1.1/'>
      <dc:format>application/pdf</dc:format>
    </rdf:Description>
    <rdf:Description rdf:about=''
      xmlns:pdfsig='http://www.adobe.com/pdf/signature/'>
      <pdfsig:Signature>
        <pdfsig:Reason>{pkcs7.Reason}</pdfsig:Reason>
        <pdfsig:Location>{pkcs7.Location}</pdfsig:Location>
        <pdfsig:ContactInfo>{pkcs7.ContactInfo}</pdfsig:ContactInfo>
        <pdfsig:Date>{pkcs7.Date:O}</pdfsig:Date>
      </pdfsig:Signature>
    </rdf:Description>
  </rdf:RDF>
</x:xmpmeta>
<?xpacket end='w'?>";

            // Embed the XMP metadata into the PDF
            using (MemoryStream xmpStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmp)))
            {
                doc.SetXmpMetadata(xmpStream);
            }

            // Save the signed PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}