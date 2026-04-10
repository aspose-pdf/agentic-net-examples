using System;
using System.IO;
using Aspose.Pdf;

namespace AsposePdfApi
{
    class Program
    {
        static void Main()
        {
            const string inputPath = "input.pdf";
            const string outputPath = "output_with_xmp.pdf";

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Define a custom XMP metadata block (XML format)
            string xmpXml = @"<?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>
<x:xmpmeta xmlns:x='adobe:ns:meta/'>
  <rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>
    <rdf:Description rdf:about=''
      xmlns:dc='http://purl.org/dc/elements/1.1/'>
      <dc:title>
        <rdf:Alt>
          <rdf:li xml:lang='x-default'>Custom Title</rdf:li>
        </rdf:Alt>
      </dc:title>
      <dc:creator>
        <rdf:Seq>
          <rdf:li>John Doe</rdf:li>
        </rdf:Seq>
      </dc:creator>
    </rdf:Description>
  </rdf:RDF>
</x:xmpmeta>
<?xpacket end='w'?>";

            // Convert the XML string to a memory stream (UTF‑8 encoded)
            using (MemoryStream xmpStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmpXml)))
            {
                // Load the PDF, set the XMP metadata, and save the document
                using (Document doc = new Document(inputPath))
                {
                    // Apply the custom XMP metadata
                    doc.SetXmpMetadata(xmpStream);

                    // Save the PDF while preserving the new metadata
                    doc.Save(outputPath);
                }
            }

            Console.WriteLine($"PDF saved with custom XMP metadata to '{outputPath}'.");
        }
    }
}
