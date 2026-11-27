using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputDir  = "TableCells";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content (document must be tagged)
                ITaggedContent taggedContent = doc.TaggedContent;

                // Root of the logical structure tree
                StructureElement root = taggedContent.RootElement;

                // Find all tables in the document (recursive search)
                var tables = root.FindElements<TableElement>(true);

                int tableIndex = 1;
                foreach (TableElement table in tables)
                {
                    // Collect all header (TH) and data (TD) cells within the current table
                    var headerCells = table.FindElements<TableTHElement>(true);
                    var dataCells   = table.FindElements<TableTDElement>(true);

                    // Combine both collections for sequential processing
                    var allCells = new System.Collections.Generic.List<StructureElement>();
                    allCells.AddRange(headerCells);
                    allCells.AddRange(dataCells);

                    int cellIndex = 1;
                    foreach (StructureElement cell in allCells)
                    {
                        // Retrieve the visible text of the cell; ActualText holds the content
                        string cellText = cell.ActualText ?? string.Empty;

                        // Build a file name like Table1_Cell1.txt, Table1_Cell2.txt, etc.
                        string fileName = $"Table{tableIndex}_Cell{cellIndex}.txt";
                        string filePath = Path.Combine(outputDir, fileName);

                        // Write the cell text to the file (overwrites if exists)
                        File.WriteAllText(filePath, cellText);

                        cellIndex++;
                    }

                    tableIndex++;
                }

                // No need for PreSave(); simply save the (unchanged) document if desired
                // doc.Save("output.pdf");
            }

            Console.WriteLine("Cell texts have been saved to individual TXT files.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}