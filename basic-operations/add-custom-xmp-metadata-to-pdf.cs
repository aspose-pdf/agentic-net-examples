using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_xmp.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Prepare a custom XMP metadata packet (XML format)
            string xmpPacket = @"<?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>
<x:xmpmeta xmlns:x='adobe:ns:meta/'>
  <rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>
    <rdf:Description rdf:about='' xmlns:dc='http://purl.org/dc/elements/1.1/'>
      <dc:title>
        <rdf:Alt>
          <rdf:li xml:lang='x-default'>Custom Title</rdf:li>
        </rdf:Alt>
      </dc:title>
    </rdf:Description>
  </rdf:RDF>
</x:xmpmeta>
<?xpacket end='w'?>";

            // Convert the XML string to a memory stream
            using (MemoryStream xmpStream = new MemoryStream(Encoding.UTF8.GetBytes(xmpPacket)))
            {
                // Set the XMP metadata on the document
                doc.SetXmpMetadata(xmpStream);
            }

            // Save the document, preserving the newly added metadata
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with custom XMP metadata to '{outputPdf}'.");
    }
}