using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputXml = "structure.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that the PDF is tagged – Document.IsTagged does not exist, use TaggedContent null‑check
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("The document is not a tagged PDF.");
                return;
            }

            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;
            // Obtain the root structure element of the tagged PDF
            StructTreeRootElement structRoot = tagged.StructTreeRootElement;

            // (Optional) you can work with structRoot here, e.g., inspect its children.
            Console.WriteLine($"Root element has {structRoot.ChildElements.Count} child elements.");

            // Save the logical‑structure tree as an XML file
            doc.Save(outputXml, new XmlSaveOptions());
        }

        Console.WriteLine($"Structure tree XML saved to '{outputXml}'.");
    }
}
