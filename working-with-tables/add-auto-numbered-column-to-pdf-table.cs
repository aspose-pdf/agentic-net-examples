using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Sample data for the table (first column will be auto‑numbered)
        List<string> items = new List<string>
        {
            "Apple",
            "Banana",
            "Cherry",
            "Date",
            "Elderberry"
        };

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content (creates a tagged structure if none exists)
                ITaggedContent tagged = doc.TaggedContent;
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Root element of the structure tree
                StructureElement root = tagged.RootElement;

                // Create a table element and attach it to the root
                TableElement table = tagged.CreateTableElement();
                table.AlternativeText = "Sample data table with auto‑numbered column";
                root.AppendChild(table);

                // ----- Table Header -----
                TableTHeadElement thead = tagged.CreateTableTHeadElement();
                table.AppendChild(thead);

                TableTRElement headerRow = tagged.CreateTableTRElement();
                thead.AppendChild(headerRow);

                // Header cell for the auto‑numbered column
                TableTHElement thNumber = tagged.CreateTableTHElement();
                thNumber.SetText("No.");
                headerRow.AppendChild(thNumber);

                // Header cell for the data column
                TableTHElement thItem = tagged.CreateTableTHElement();
                thItem.SetText("Item");
                headerRow.AppendChild(thItem);

                // ----- Table Body -----
                TableTBodyElement tbody = tagged.CreateTableTBodyElement();
                table.AppendChild(tbody);

                for (int i = 0; i < items.Count; i++)
                {
                    // Create a new row
                    TableTRElement dataRow = tagged.CreateTableTRElement();
                    tbody.AppendChild(dataRow);

                    // First cell: auto‑numbered value (i + 1)
                    TableTDElement tdNumber = tagged.CreateTableTDElement();
                    tdNumber.SetText((i + 1).ToString());
                    dataRow.AppendChild(tdNumber);

                    // Second cell: actual item text
                    TableTDElement tdItem = tagged.CreateTableTDElement();
                    tdItem.SetText(items[i]);
                    dataRow.AppendChild(tdItem);
                }

                // Save the modified PDF (no PreSave call required)
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with auto‑numbered table saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}