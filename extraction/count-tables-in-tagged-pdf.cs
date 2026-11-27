using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "table_count.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            int tableCount = 0;

            // Check if the PDF is tagged by verifying TaggedContent is not null
            if (doc.TaggedContent != null)
            {
                ITaggedContent tagged = doc.TaggedContent;
                // Root element of the structure tree
                StructureElement root = tagged.RootElement;

                // Find all TableElement instances recursively
                var tables = root.FindElements<TableElement>(true);
                tableCount = tables.Count;
            }
            else
            {
                // No tagging => no structural tables detected
                tableCount = 0;
            }

            // Write the count to a text file
            File.WriteAllText(outputTxtPath, tableCount.ToString());
            Console.WriteLine($"Detected tables: {tableCount}");
            Console.WriteLine($"Count written to: {outputTxtPath}");
        }
    }
}
