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
        const string outputCsv = "structure_ids.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Verify that the document contains tagged content
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("The PDF does not contain tagged content.");
                return;
            }

            // Obtain the root structure element
            StructureElement root = doc.TaggedContent.RootElement;

            // Get the list of direct child elements of the root
            ElementList childElements = root.ChildElements;

            // Write the IDs of the child elements to a CSV file
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                // CSV header
                writer.WriteLine("ElementId");

                foreach (Element element in childElements)
                {
                    // The ID property is read‑only and named "ID" on StructureElement
                    string id = (element as StructureElement)?.ID ?? string.Empty;
                    writer.WriteLine(id);
                }
            }

            Console.WriteLine($"Exported {childElements.Count} element IDs to '{outputCsv}'.");
        }
    }
}
