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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Verify that the PDF contains tagged content (the replacement for the removed IsTagged property)
            if (doc.TaggedContent == null)
            {
                Console.WriteLine("Document is not tagged; no table structure to inspect.");
                return;
            }

            ITaggedContent tagged = doc.TaggedContent;

            // Locate the first TableElement in the structure tree
            TableElement? table = FindFirstTable(tagged.RootElement);
            if (table == null)
            {
                Console.WriteLine("No table element found in the document.");
                return;
            }

            // Determine whether the table is broken across pages
            bool isBroken = table.IsBroken;
            Console.WriteLine($"Table.IsBroken = {isBroken}");
        }
    }

    // Recursively searches for a TableElement starting from the given element
    static TableElement? FindFirstTable(StructureElement element)
    {
        if (element is TableElement tbl)
            return tbl;

        foreach (Element child in element.ChildElements)
        {
            if (child is StructureElement se)
            {
                TableElement? result = FindFirstTable(se);
                if (result != null)
                    return result;
            }
        }
        return null;
    }
}