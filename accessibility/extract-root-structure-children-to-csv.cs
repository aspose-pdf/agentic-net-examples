using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string csvPath   = "structure_children.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Access tagged content via the TaggedContent property.
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged. No structure tree available.");
                return;
            }

            ITaggedContent tagged = doc.TaggedContent;

            // Use dynamic to avoid compile‑time dependency on StructureElement types that may be missing in older SDK versions.
            dynamic root = tagged.RootElement; // logical structure tree root

            // Collect the type names of immediate child elements.
            List<string> childTypes = new List<string>();
            foreach (var child in root.ChildElements)
            {
                // The runtime type will be a concrete structure element (e.g., SectionElement, DivElement, etc.).
                childTypes.Add(child.GetType().Name);
            }

            // Write the list to a CSV file (one column: ChildElementType).
            using (StreamWriter writer = new StreamWriter(csvPath, false))
            {
                writer.WriteLine("ChildElementType");
                foreach (string typeName in childTypes)
                {
                    writer.WriteLine(typeName);
                }
            }

            Console.WriteLine($"Child element types saved to '{csvPath}'.");
        }
    }
}
