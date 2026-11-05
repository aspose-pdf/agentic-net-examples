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
        // Sample data collection; can be replaced with any dynamic source
        var data = new List<(string Name, string Value)>
        {
            ("Item A", "100"),
            ("Item B", "200"),
            ("Item C", "300")
            // Add more rows as needed
        };

        const string outputPath = "dynamic_table.pdf";

        // Create the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page that will contain the visual table
            Page page = doc.Pages.Add();

            // Create a visual Table object
            Table table = new Table
            {
                ColumnWidths = "200 200", // two equal-width columns
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Header row
            Row header = table.Rows.Add();
            Cell headerCell1 = header.Cells.Add("Name");
            Cell headerCell2 = header.Cells.Add("Value");
            headerCell1.BackgroundColor = Aspose.Pdf.Color.LightGray;
            headerCell2.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Dynamically add data rows based on the collection size
            foreach (var item in data)
            {
                Row row = table.Rows.Add();
                row.Cells.Add(item.Name);
                row.Cells.Add(item.Value);
            }

            // Add the visual table to the page
            page.Paragraphs.Add(table);

            // ---------- Accessibility tagging ----------
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Dynamic Data Table");

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a tagged table element
            TableElement taggedTable = tagged.CreateTableElement();
            taggedTable.AlternativeText = "Data table with dynamic rows.";
            root.AppendChild(taggedTable);

            // Table header (thead)
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            taggedTable.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);
            TableTHElement thName = tagged.CreateTableTHElement();
            thName.SetText("Name");
            headerRow.AppendChild(thName);
            TableTHElement thValue = tagged.CreateTableTHElement();
            thValue.SetText("Value");
            headerRow.AppendChild(thValue);

            // Table body (tbody) with rows matching the data collection
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            taggedTable.AppendChild(tbody);
            foreach (var item in data)
            {
                TableTRElement dataRow = tagged.CreateTableTRElement();
                tbody.AppendChild(dataRow);
                TableTDElement tdName = tagged.CreateTableTDElement();
                tdName.SetText(item.Name);
                dataRow.AppendChild(tdName);
                TableTDElement tdValue = tagged.CreateTableTDElement();
                tdValue.SetText(item.Value);
                dataRow.AppendChild(tdValue);
            }

            // Save the PDF (no PreSave needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dynamic table saved to '{outputPath}'.");
    }
}