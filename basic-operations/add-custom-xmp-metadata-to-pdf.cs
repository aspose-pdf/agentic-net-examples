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

        // Load the existing PDF document inside a using block for proper disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Build a simple XMP metadata packet (XML format).
            // This example adds a custom namespace and a custom property.
            string xmpXml = @"<?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>
<x:xmpmeta xmlns:x='adobe:ns:meta/'>
  <rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>
    <rdf:Description rdf:about=''
        xmlns:my='http://example.com/myNamespace/'>
      <my:CustomProperty>CustomValue</my:CustomProperty>
    </rdf:Description>
  </rdf:RDF>
</x:xmpmeta>
<?xpacket end='w'?>";

            // Convert the XML string to a stream.
            using (MemoryStream xmpStream = new MemoryStream(Encoding.UTF8.GetBytes(xmpXml)))
            {
                // Set the XMP metadata on the document.
                doc.SetXmpMetadata(xmpStream);
            }

            // Save the document, preserving the newly added XMP metadata.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with custom XMP metadata to '{outputPdf}'.");
    }
}