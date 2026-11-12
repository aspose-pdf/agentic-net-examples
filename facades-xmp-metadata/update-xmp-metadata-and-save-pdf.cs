using System;
using System.IO;
using Aspose.Pdf;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Example XMP metadata XML to be written into the PDF
        string newXmp = @"<?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>
<x:xmpmeta xmlns:x='adobe:ns:meta/'>
  <rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>
    <rdf:Description rdf:about=''
      xmlns:dc='http://purl.org/dc/elements/1.1/'>
      <dc:title>
        <rdf:Alt>
          <rdf:li xml:lang='x-default'>Sample PDF Title</rdf:li>
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

        // Apply the new XMP metadata to the document using the Metadata dictionary.
        // The Metadata property is read‑only but returns a mutable IDictionary, so we can add/replace entries directly.
        pdfDoc.Metadata["xmp"] = newXmp;

        // Save the modified PDF to a new file
        pdfDoc.Save(outputPath);

        Console.WriteLine($"PDF saved with updated XMP metadata to '{outputPath}'.");
    }
}
