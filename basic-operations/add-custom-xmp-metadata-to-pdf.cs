using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_xmp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Prepare a simple XMP metadata packet (XML format)
            string xmpXml = @"<?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>
<x:xmpmeta xmlns:x='adobe:ns:meta/'>
  <rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>
    <rdf:Description rdf:about=''
        xmlns:dc='http://purl.org/dc/elements/1.1/'>
      <dc:title>
        <rdf:Alt>
          <rdf:li xml:lang='x-default'>Custom XMP Title</rdf:li>
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

            // Convert the XML string to a stream
            using (MemoryStream xmpStream = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(xmpStream))
            {
                writer.Write(xmpXml);
                writer.Flush();
                xmpStream.Position = 0; // Reset stream position before reading

                // Set the XMP metadata on the document
                doc.SetXmpMetadata(xmpStream);
            }

            // Save the document, preserving the newly added XMP metadata
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom XMP metadata to '{outputPath}'.");
    }
}