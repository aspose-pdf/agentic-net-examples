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

        // Verify input files exist
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

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(inputPdfPath))
            {
                // -----------------------------------------------------------------
                // 1. Add a signature field to the first page
                // -----------------------------------------------------------------
                // Define the rectangle where the signature will appear
                // (left, bottom, width, height) – values are in points
                Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 200, 100);

                // Create the signature field and add it to the page annotations
                SignatureField sigField = new SignatureField(doc, sigRect);
                doc.Pages[1].Annotations.Add(sigField);

                // -----------------------------------------------------------------
                // 2. Prepare the PKCS#7 signature object
                // -----------------------------------------------------------------
                // Use the PKCS7 constructor that accepts a PFX file path and password
                PKCS7 pkcs7Signature = new PKCS7(pfxPath, pfxPassword)
                {
                    Reason   = "Document approved",
                    Location = "Head Office",
                    ContactInfo = "contact@example.com",
                    Date = DateTime.UtcNow
                };

                // -----------------------------------------------------------------
                // 3. Sign the document using the signature field
                // -----------------------------------------------------------------
                sigField.Sign(pkcs7Signature);

                // -----------------------------------------------------------------
                // 4. Embed custom XMP metadata describing the signing process
                // -----------------------------------------------------------------
                // Simple XMP packet (XML) – adjust as needed for your metadata schema
                string xmpXml = @"<?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>
<rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'
         xmlns:pdf='http://ns.adobe.com/pdf/1.3/'>
  <rdf:Description rdf:about=''
    pdf:Producer='Aspose.Pdf for .NET'
    pdf:SignatureDate='" + pkcs7Signature.Date.ToString("o") + @"'
    pdf:SignatureReason='" + pkcs7Signature.Reason + @"'
    pdf:SignatureLocation='" + pkcs7Signature.Location + @"'>
  </rdf:Description>
</rdf:RDF>
<?xpacket end='w'?>";

                // Convert the XML string to a stream (required by SetXmpMetadata)
                using (MemoryStream xmpStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmpXml)))
                {
                    doc.SetXmpMetadata(xmpStream);
                }

                // -----------------------------------------------------------------
                // 5. Save the signed PDF (lifecycle rule: save inside using block)
                // -----------------------------------------------------------------
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}