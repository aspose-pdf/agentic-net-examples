using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF with updated XMP metadata
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_branded.pdf";

        // Corporate branding information to embed
        const string logoUrl      = "https://example.com/logo.png";
        const string brandColorHex = "#1A2B3C";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Build a minimal XMP packet containing custom branding properties.
        // A custom namespace (http://example.com/branding/) is used for the brand data.
        string xmpXml = $@"<?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>
<x:xmpmeta xmlns:x='adobe:ns:meta/'>
  <rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>
    <rdf:Description rdf:about=''
        xmlns:br='http://example.com/branding/'>
      <br:LogoUrl>{logoUrl}</br:LogoUrl>
      <br:BrandColor>{brandColorHex}</br:BrandColor>
    </rdf:Description>
  </rdf:RDF>
</x:xmpmeta>
<?xpacket end='w'?>";

        // Load the PDF, assign the XMP metadata via the Metadata dictionary, and save the result.
        Document pdfDoc = new Document(inputPdfPath);
        // Store the raw XMP packet under a custom key (Aspose.PDF will embed it when saved).
        pdfDoc.Metadata["xmp"] = xmpXml;
        pdfDoc.Save(outputPdfPath);

        Console.WriteLine($"XMP metadata added. Output saved to '{outputPdfPath}'.");
    }
}
