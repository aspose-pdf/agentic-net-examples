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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Obtain the tagged content interface (creates structure if missing)
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Auto‑Numbered Table");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with auto‑numbered first column";
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

            // Header cell for a data column
            TableTHElement thData = tagged.CreateTableTHElement();
            thData.SetText("Data");
            headerRow.AppendChild(thData);

            // ----- Table Body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Insert rows with sequential numbers in the first cell
            for (int i = 1; i <= 10; i++)
            {
                TableTRElement row = tagged.CreateTableTRElement();
                tbody.AppendChild(row);

                // First cell: auto‑numbered value
                TableTDElement tdNumber = tagged.CreateTableTDElement();
                tdNumber.SetText(i.ToString());
                row.AppendChild(tdNumber);

                // Second cell: placeholder data
                TableTDElement tdData = tagged.CreateTableTDElement();
                tdData.SetText($"Row {i} data");
                row.AppendChild(tdData);
            }

            // Save the modified PDF (no PreSave call required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with auto‑numbered table: {outputPath}");
    }
}