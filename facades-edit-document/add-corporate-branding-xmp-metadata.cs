using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_branded.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Build an XMP packet that includes custom corporate branding properties.
            // A custom namespace (http://example.com/corporate/branding/) is used for the
            // LogoURL and BrandColor entries.
            string xmp = @"<?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>
<x:xmpmeta xmlns:x='adobe:ns:meta/'>
  <rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>
    <rdf:Description rdf:about=''
        xmlns:corp='http://example.com/corporate/branding/'>
      <corp:LogoURL>https://example.com/logo.png</corp:LogoURL>
      <corp:BrandColor>#FF5733</corp:BrandColor>
    </rdf:Description>
  </rdf:RDF>
</x:xmpmeta>
<?xpacket end='w'?>";

            // Aspose.Pdf does not expose an XmpMetadata property.  Use the Metadata
            // dictionary (indexer) to store the raw XMP packet.  The special key "xmp"
            // is interpreted by the library as the full XMP metadata block.
            doc.Metadata["xmp"] = xmp;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Branded PDF saved to '{outputPath}'.");
    }
}
