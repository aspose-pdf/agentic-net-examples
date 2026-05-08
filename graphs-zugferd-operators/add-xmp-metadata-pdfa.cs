using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Set basic document information
            doc.Info.Title = "Sample PDF/A Document";
            doc.Info.Author = "John Doe";
            doc.Info.Creator = "MyApp";
            doc.Info.Producer = "Aspose.Pdf";

            // Build XMP metadata XML containing creation date, creator tool, producer and document ID
            string xmpXml = $@"<?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>
<x:xmpmeta xmlns:x='adobe:ns:meta/'>
  <rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>
    <rdf:Description rdf:about=''
      xmlns:xmp='http://ns.adobe.com/xap/1.0/'
      xmlns:pdf='http://ns.adobe.com/pdf/1.3/'>
      <xmp:CreateDate>{DateTime.UtcNow:yyyy-MM-ddTHH:mm:ssZ}</xmp:CreateDate>
      <xmp:CreatorTool>MyApp PDF/A Generator</xmp:CreatorTool>
      <pdf:Producer>Aspose.Pdf</pdf:Producer>
      <pdf:DocumentID>urn:uuid:{Guid.NewGuid()}</pdf:DocumentID>
    </rdf:Description>
  </rdf:RDF>
</x:xmpmeta>
<?xpacket end='w'?>";

            // Apply the XMP metadata to the document
            using (MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmpXml)))
            {
                doc.SetXmpMetadata(ms);
            }

            // Convert the document to PDF/A-1b compliance
            doc.Convert("conversion_log.xml", PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the PDF/A compliant file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A file saved to '{outputPath}'.");
    }
}