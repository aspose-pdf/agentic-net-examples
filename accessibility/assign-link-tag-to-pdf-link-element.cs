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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (document must be tagged)
            ITaggedContent tagged = doc.TaggedContent;
            if (tagged == null)
            {
                Console.Error.WriteLine("Document does not support tagging.");
                return;
            }

            // Root of the structure tree
            StructureElement root = tagged.RootElement;

            // Find all link elements in the tree
            var linkElements = root.FindElements<LinkElement>(true);
            if (linkElements.Count == 0)
            {
                Console.WriteLine("No link elements found in the document.");
            }
            else
            {
                // Assign the PDF tag "Link" to the first link element
                LinkElement link = linkElements[0];
                // Use the SetTag method because Tag is a method group, not a property
                link.SetTag("Link");
                Console.WriteLine("Tag assigned to a link element.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
