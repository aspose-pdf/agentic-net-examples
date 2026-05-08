using System;
using System.IO;
using System.Text;
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
            // Build a simple XMP packet as a UTF‑8 string.
            // This example adds creator and title metadata.
            string xmpPacket =
                @"<?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>"
              + "<x:xmpmeta xmlns:x='adobe:ns:meta/'>"
              + "<rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>"
              + "<rdf:Description rdf:about='' xmlns:dc='http://purl.org/dc/elements/1.1/'>"
              + "<dc:creator><rdf:Seq><rdf:li>John Doe</rdf:li></rdf:Seq></dc:creator>"
              + "<dc:title><rdf:Alt><rdf:li xml:lang='x-default'>Sample PDF</rdf:li></rdf:Alt></dc:title>"
              + "</rdf:Description>"
              + "</rdf:RDF>"
              + "</x:xmpmeta>"
              + "<?xpacket end='w'?>";

            // Convert the XMP string to a memory stream
            byte[] xmpBytes = Encoding.UTF8.GetBytes(xmpPacket);
            using (MemoryStream xmpStream = new MemoryStream(xmpBytes))
            {
                // Attach the XMP metadata to the document
                doc.SetXmpMetadata(xmpStream);
            }

            // Save the document, preserving the newly added XMP metadata
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with XMP metadata to '{outputPath}'.");
    }
}