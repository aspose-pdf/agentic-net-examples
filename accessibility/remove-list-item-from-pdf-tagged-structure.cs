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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content (creates it if missing)
            ITaggedContent tagged = doc.TaggedContent;

            // Ensure the document has a structure tree
            StructureElement root = tagged.RootElement;

            // Find the first List element in the structure (if any)
            var lists = root.FindElements<ListElement>(true);
            if (lists.Count > 0)
            {
                ListElement list = lists[0];

                // Ensure the list has at least one child (e.g., a ListItem)
                if (list.ChildElements.Count > 0)
                {
                    // Remove the first child from the list using the index overload
                    list.RemoveChild(0);
                    Console.WriteLine("Removed a child element from the list.");
                }
                else
                {
                    Console.WriteLine("The List element has no children to remove.");
                }
            }
            else
            {
                Console.WriteLine("No List element found in the document structure.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
