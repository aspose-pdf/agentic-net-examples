using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // Add standard document information (optional but useful)
            // ------------------------------------------------------------
            doc.Info.Title    = "Sample PDF/A Document";
            doc.Info.Creator  = "MyApp 1.0";
            doc.Info.Author   = "John Doe";
            doc.Info.ModDate  = DateTime.Now;
            doc.Info.CreationDate = DateTime.Now;

            // ------------------------------------------------------------
            // Prepare XMP metadata required for PDF/A compliance.
            // We add the DocumentID and CreatorTool entries.
            // ------------------------------------------------------------
            string xmpXml =
@"<?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>
<x:xmpmeta xmlns:x='adobe:ns:meta/'>
  <rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>
    <rdf:Description rdf:about=''
                     xmlns:xmp='http://ns.adobe.com/xap/1.0/'>
      <xmp:DocumentID>uuid:12345678-1234-1234-1234-123456789abc</xmp:DocumentID>
      <xmp:CreatorTool>MyApp 1.0</xmp:CreatorTool>
    </rdf:Description>
  </rdf:RDF>
</x:xmpmeta>
<?xpacket end='w'?>";

            // Convert the XML string to a memory stream
            using (MemoryStream xmpStream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(xmpStream, System.Text.Encoding.UTF8, 1024, true))
                {
                    writer.Write(xmpXml);
                }
                xmpStream.Position = 0; // Reset position before passing to the API

                // Attach the XMP metadata to the document
                doc.SetXmpMetadata(xmpStream);
            }

            // ------------------------------------------------------------
            // PDF/A specific flag – allow XRef gaps (required for PDF/A‑1b)
            // ------------------------------------------------------------
            doc.IsXrefGapsAllowed = true;

            // Save the resulting PDF (PDF/A compliant) to the output file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A document saved to '{outputPath}'.");
    }
}