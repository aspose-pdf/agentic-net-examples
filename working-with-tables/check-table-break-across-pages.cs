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

        // Load PDF within a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content of the document
            ITaggedContent tagged = doc.TaggedContent;

            // Retrieve the first TableElement in the structure tree
            TableElement table = null;
            var tables = tagged.RootElement.FindElements<TableElement>(true);
            if (tables.Count > 0)
                table = tables[0];

            if (table == null)
            {
                Console.WriteLine("No table element found in the document.");
                return;
            }

            // Determine whether the table breaks across pages
            bool isBroken = table.IsBroken;
            Console.WriteLine($"Table IsBroken: {isBroken}");
        }
    }
}