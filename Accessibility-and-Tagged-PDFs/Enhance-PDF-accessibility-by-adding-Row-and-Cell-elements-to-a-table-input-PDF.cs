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
        const string outputPath = "accessible_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the document (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // ------------------------------------------------------------
            // Create a new Table structure element and attach it to the root
            // ------------------------------------------------------------
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table added for accessibility";
            root.AppendChild(table); // AppendChild with a single argument

            // ------------------------------------------------------------
            // Create a Table Body (TBody) and attach it to the table
            // ------------------------------------------------------------
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // ------------------------------------------------------------
            // Add a new Row (TableTR) to the body
            // ------------------------------------------------------------
            TableTRElement row = tagged.CreateTableTRElement();
            tbody.AppendChild(row);

            // ------------------------------------------------------------
            // Add two Cells (TableTD) to the row and set their text
            // ------------------------------------------------------------
            TableTDElement cell1 = tagged.CreateTableTDElement();
            cell1.SetText("First cell content");
            row.AppendChild(cell1);

            TableTDElement cell2 = tagged.CreateTableTDElement();
            cell2.SetText("Second cell content");
            row.AppendChild(cell2);

            // ------------------------------------------------------------
            // Save the modified PDF (no PreSave required)
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF with table saved to '{outputPath}'.");
    }
}