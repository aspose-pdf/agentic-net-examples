using System;
using System.IO;
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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add custom key/value pairs to the DocumentInfo dictionary
            doc.Info.Add("ProcessingId", "12345");
            doc.Info.Add("SourceSystem", "MyApp");

            // Create custom XMP metadata as an XML string
            string xmpXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<x:xmpmeta xmlns:x=""adobe:ns:meta/"" x:xmptk=""Aspose.Pdf"">
  <rdf:RDF xmlns:rdf=""http://www.w3.org/1999/02/22-rdf-syntax-ns#"">
    <rdf:Description rdf:about="""" xmlns:my=""http://example.com/myNamespace#"">
      <my:ProcessingId>12345</my:ProcessingId>
      <my:SourceSystem>MyApp</my:SourceSystem>
    </rdf:Description>
  </rdf:RDF>
</x:xmpmeta>";

            // Write the XML to a memory stream and set it as XMP metadata
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(ms))
                {
                    writer.Write(xmpXml);
                    writer.Flush();
                    ms.Position = 0; // Reset stream position for reading
                    doc.SetXmpMetadata(ms);
                }
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}