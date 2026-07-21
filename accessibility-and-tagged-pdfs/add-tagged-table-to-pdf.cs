using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;    // StructureElement, TableElement, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language for the tagged PDF
            tagged.SetLanguage("en-US");

            // Get the root of the structure tree (no cast needed)
            StructureElement root = tagged.RootElement;

            // Create a Table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            root.AppendChild(table);

            // Create a Table Body element and attach it to the table
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Create three rows, each with four cells
            for (int rowIndex = 0; rowIndex < 3; rowIndex++)
            {
                // Create a table row and attach it to the body
                TableTRElement row = tagged.CreateTableTRElement();
                tbody.AppendChild(row);

                // Create four cells for the current row
                for (int colIndex = 0; colIndex < 4; colIndex++)
                {
                    TableTDElement cell = tagged.CreateTableTDElement();
                    // Set the visible text of the cell
                    cell.SetText($"R{rowIndex + 1}C{colIndex + 1}");
                    // Attach the cell to the current row
                    row.AppendChild(cell);
                }
            }

            // Save the modified PDF (no PreSave needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}