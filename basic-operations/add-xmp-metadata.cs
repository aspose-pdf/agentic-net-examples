using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Custom XMP metadata block (XML format)
        string xmpXml = @"<?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>
<rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>
  <rdf:Description rdf:about='' xmlns:dc='http://purl.org/dc/elements/1.1/'>
    <dc:creator>John Doe</dc:creator>
    <dc:title>Sample PDF with XMP</dc:title>
  </rdf:Description>
</rdf:RDF>
<?xpacket end='w'?>";

        byte[] xmpBytes = Encoding.UTF8.GetBytes(xmpXml);

        using (Document doc = new Document(inputPath))
        {
            using (MemoryStream ms = new MemoryStream(xmpBytes))
            {
                doc.SetXmpMetadata(ms);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with XMP metadata to '{outputPath}'.");
    }
}