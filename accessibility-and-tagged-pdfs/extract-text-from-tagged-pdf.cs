using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (throws if PDF is not tagged)
            ITaggedContent tagged = doc.TaggedContent;

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Collect all textual content
            StringBuilder allText = new StringBuilder();
            TraverseStructure(root, allText);

            // Output the extracted text
            Console.WriteLine("Extracted Text:");
            Console.WriteLine(allText.ToString());
        }
    }

    // Recursively walk the structure tree and append ActualText of each element
    static void TraverseStructure(StructureElement element, StringBuilder sb)
    {
        // ActualText holds the visible text for most structure elements
        if (!string.IsNullOrWhiteSpace(element.ActualText))
        {
            sb.AppendLine(element.ActualText);
        }

        // Iterate over child elements (ElementList) – use ChildElements property
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStruct)
            {
                TraverseStructure(childStruct, sb);
            }
        }
    }
}