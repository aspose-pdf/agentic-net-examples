using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_custom.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add custom key‑value pairs to the DocumentInfo dictionary
            doc.Info.Add("CustomKey1", "CustomValue1");
            doc.Info.Add("CustomKey2", "CustomValue2");

            // Embed custom XML as XMP metadata (optional but useful for downstream processing)
            string xmpXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<x:xmpmeta xmlns:x=""adobe:ns:meta/"" x:xmptk=""Aspose.Pdf"">
  <rdf:RDF xmlns:rdf=""http://www.w3.org/1999/02/22-rdf-syntax-ns#"">
    <rdf:Description rdf:about="""" xmlns:custom=""http://example.com/custom#"">
      <custom:ProcessingInfo>Downstream data</custom:ProcessingInfo>
    </rdf:Description>
  </rdf:RDF>
</x:xmpmeta>";

            using (MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmpXml)))
            {
                doc.SetXmpMetadata(ms);
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with custom metadata to '{outputPath}'.");
    }
}