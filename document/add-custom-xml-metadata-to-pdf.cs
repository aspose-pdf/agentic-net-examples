using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Add custom entries to the DocumentInfo dictionary.
            //    This dictionary stores standard metadata (Title, Author, …) and
            //    any user‑defined key/value pairs.
            // -----------------------------------------------------------------
            string customXml = "<metadata><item key=\"example\">value</item></metadata>";
            doc.Info.Add("CustomXmlMetadata", customXml);   // or doc.Info["CustomXmlMetadata"] = customXml;

            // -----------------------------------------------------------------
            // 2. (Optional) Add custom XMP metadata.
            //    XMP metadata is stored in a separate XML packet.  Here we
            //    create a minimal XMP packet that contains a custom namespace
            //    and a single element.  The packet is written to a memory stream
            //    and passed to Document.SetXmpMetadata.
            // -----------------------------------------------------------------
            string xmpXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<x:xmpmeta xmlns:x=""adobe:ns:meta/"" x:xmptk=""Aspose.Pdf"">
  <rdf:RDF xmlns:rdf=""http://www.w3.org/1999/02/22-rdf-syntax-ns#"">
    <rdf:Description rdf:about="""" xmlns:custom=""http://example.com/custom#"">
      <custom:Data>Sample custom XML data</custom:Data>
    </rdf:Description>
  </rdf:RDF>
</x:xmpmeta>";

            using (MemoryStream xmpStream = new MemoryStream(Encoding.UTF8.GetBytes(xmpXml)))
            {
                doc.SetXmpMetadata(xmpStream);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom metadata to '{outputPath}'.");
    }
}