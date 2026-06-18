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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content of the document
            ITaggedContent tagged = doc.TaggedContent;

            // Retrieve all TableElement objects from the structure tree (recursive search)
            var tables = tagged.RootElement.FindElements<TableElement>(true);

            if (tables.Count == 0)
            {
                Console.WriteLine("No table elements found in the document.");
                return;
            }

            // Iterate through each table and check its IsBroken property
            foreach (TableElement table in tables)
            {
                bool isBroken = table.IsBroken; // true if the table will be truncated to the next page
                Console.WriteLine($"Table ID = {table.ID}, IsBroken = {isBroken}");
            }
        }
    }
}