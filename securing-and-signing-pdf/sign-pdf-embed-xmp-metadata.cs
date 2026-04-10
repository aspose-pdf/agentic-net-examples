using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string outputPdfPath  = "signed_output.pdf"; // signed PDF
        const string pfxPath        = "certificate.pfx";   // signing certificate
        const string pfxPassword    = "pfxPassword";       // certificate password

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"PFX file not found: {pfxPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 1. Add a signature field (positioned at the bottom of the first page)
            // -----------------------------------------------------------------
            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
            SignatureField sigField = new SignatureField(doc, sigRect);
            // Optional: set a name for the field
            sigField.Name = "Signature1";
            // Add the field to the document (the constructor already registers it)

            // -----------------------------------------------------------------
            // 2. Create a PKCS#7 signature object using the PFX file
            // -----------------------------------------------------------------
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason   = "Document approval",
                Location = "Head Office",
                ContactInfo = "contact@example.com",
                Date = DateTime.UtcNow
            };

            // -----------------------------------------------------------------
            // 3. Sign the document using the signature field
            // -----------------------------------------------------------------
            sigField.Sign(pkcs7);

            // -----------------------------------------------------------------
            // 4. Embed custom XMP metadata describing the signing process
            // -----------------------------------------------------------------
            string xmpXml =
@"<?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>
<rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>
  <rdf:Description rdf:about=''>
    <dc:creator xmlns:dc='http://purl.org/dc/elements/1.1/'>
      <rdf:Seq><rdf:li>MyCompany</rdf:li></rdf:Seq>
    </dc:creator>
    <pdf:Producer xmlns:pdf='http://ns.adobe.com/pdf/1.3/'>Aspose.Pdf</pdf:Producer>
    <custom:SigningInfo xmlns:custom='http://example.com/custom'>
      <custom:Method>PKCS7</custom:Method>
      <custom:Date>" + DateTime.UtcNow.ToString("o") + @"</custom:Date>
    </custom:SigningInfo>
  </rdf:Description>
</rdf:RDF>
<?xpacket end='w'?>";

            // Convert the XML string to a UTF‑8 stream and set it on the document
            using (MemoryStream xmpStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmpXml)))
            {
                doc.SetXmpMetadata(xmpStream);
            }

            // -----------------------------------------------------------------
            // 5. Save the signed PDF (PDF format, no extra SaveOptions needed)
            // -----------------------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}