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
        const string outputPath = "TotalTables.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content (required for structural elements like tables)
                ITaggedContent tagged = doc.TaggedContent;

                // Recursively find all TableElement objects in the structure tree
                var tables = tagged.RootElement.FindElements<TableElement>(true);
                int totalTables = tables.Count;

                // Write the total count to a text file
                File.WriteAllText(outputPath, totalTables.ToString());

                Console.WriteLine($"Total tables: {totalTables} saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}