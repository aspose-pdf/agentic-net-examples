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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            if (tagged == null)
            {
                Console.WriteLine("The document is not tagged.");
                return;
            }

            // Root of the structure tree (no cast needed)
            StructureElement root = tagged.RootElement;

            // Collect text from all structure elements
            StringBuilder sb = new StringBuilder();
            TraverseStructure(root, sb);

            // Output the extracted text
            Console.WriteLine("Extracted text from the structure tree:");
            Console.WriteLine(sb.ToString());
        }
    }

    // Recursively walk the structure tree and gather ActualText
    static void TraverseStructure(StructureElement element, StringBuilder sb)
    {
        // Append the element's text if present
        if (!string.IsNullOrEmpty(element.ActualText))
        {
            sb.AppendLine(element.ActualText);
        }

        // Iterate over child elements (ElementList) using ChildElements
        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement childStructure)
            {
                TraverseStructure(childStructure, sb);
            }
        }
    }
}