using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string pfxPath       = "certificate.pfx";
        const string pfxPassword   = "pfxPassword";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 1. Add a signature field on the first page
            // -----------------------------------------------------------------
            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField signatureField = new SignatureField(doc.Pages[1], sigRect);
            // Optional: set a name for the field
            signatureField.PartialName = "Signature1";
            // Add the field to the page annotations collection
            doc.Pages[1].Annotations.Add(signatureField);

            // -----------------------------------------------------------------
            // 2. Create a PKCS#7 signature object using the PFX certificate
            // -----------------------------------------------------------------
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                PKCS7 pkcs7Signature = new PKCS7(pfxStream, pfxPassword)
                {
                    Reason      = "Document approved",
                    Location    = "Head Office",
                    ContactInfo = "contact@example.com",
                    Date        = DateTime.UtcNow,
                    ShowProperties = true   // show default appearance with details
                };

                // Sign the document using the signature field
                signatureField.Sign(pkcs7Signature);
            }

            // -----------------------------------------------------------------
            // 3. Embed custom XMP metadata describing the signing process
            // -----------------------------------------------------------------
            // Simple XMP packet (XML) – adjust as needed for your metadata schema
            string xmpXml = @"<?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>
<x:xmpmeta xmlns:x='adobe:ns:meta/'>
  <rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>
    <rdf:Description rdf:about=''
      xmlns:pdf='http://ns.adobe.com/pdf/1.3/'
      xmlns:dc='http://purl.org/dc/elements/1.1/'>
      <pdf:Producer>Aspose.Pdf for .NET</pdf:Producer>
      <dc:format>application/pdf</dc:format>
      <pdf:Signature>
        <pdf:Signer>certificate.pfx</pdf:Signer>
        <pdf:Reason>Document approved</pdf:Reason>
        <pdf:Location>Head Office</pdf:Location>
        <pdf:ContactInfo>contact@example.com</pdf:ContactInfo>
        <pdf:SignDate>" + DateTime.UtcNow.ToString("o") + @"</pdf:SignDate>
      </pdf:Signature>
    </rdf:Description>
  </rdf:RDF>
</x:xmpmeta>
<?xpacket end='w'?>";

            // Convert the XML string to a stream and set it as XMP metadata
            using (MemoryStream xmpStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmpXml)))
            {
                doc.SetXmpMetadata(xmpStream);
            }

            // -----------------------------------------------------------------
            // 4. Save the signed PDF
            // -----------------------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}