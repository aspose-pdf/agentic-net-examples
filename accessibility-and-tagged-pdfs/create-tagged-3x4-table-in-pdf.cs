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
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Table");

            // Root of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample 3x4 table";
            root.AppendChild(table);

            // Create a table body (no header/footer needed for this example)
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Add three rows
            for (int r = 0; r < 3; r++)
            {
                // Create a table row element
                TableTRElement row = tagged.CreateTableTRElement();
                tbody.AppendChild(row);

                // Add four cells to the row
                for (int c = 0; c < 4; c++)
                {
                    TableTDElement cell = tagged.CreateTableTDElement();
                    cell.SetText($"R{r + 1}C{c + 1}"); // Set cell text
                    row.AppendChild(cell);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and saved to '{outputPath}'.");
    }
}