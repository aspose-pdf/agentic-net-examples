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
        // Sample source records (could be read from a database, CSV, etc.)
        List<string[]> sourceRecords = new List<string[]>
        {
            new[] { "ID", "Name", "Quantity", "Price" }, // header row
            new[] { "1", "Apple", "10", "$1.20" },
            new[] { "2", "Banana", "5", "$0.80" },
            new[] { "3", "Cherry", "20", "$2.50" }
        };

        // Determine the number of rows (including header) before importing
        int rowCount = sourceRecords.Count;

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Enable tagged PDF and set basic metadata
            ITaggedContent taggedContent = doc.TaggedContent;
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle("Dynamic Table Example");

            // Root element of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a table element and attach it to the root
            TableElement table = taggedContent.CreateTableElement();
            table.AlternativeText = $"Table with {rowCount} rows";
            root.AppendChild(table);

            // ----- Create table header -----
            TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
            table.AppendChild(thead);

            TableTRElement headerRow = taggedContent.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // Assuming the first record contains column titles
            string[] headerCells = sourceRecords[0];
            foreach (string header in headerCells)
            {
                TableTHElement th = taggedContent.CreateTableTHElement();
                th.SetText(header);
                headerRow.AppendChild(th);
            }

            // ----- Create table body -----
            TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Add data rows (skip the first record which was used for header)
            for (int i = 1; i < sourceRecords.Count; i++)
            {
                TableTRElement dataRow = taggedContent.CreateTableTRElement();
                tbody.AppendChild(dataRow);

                string[] cells = sourceRecords[i];
                foreach (string cell in cells)
                {
                    TableTDElement td = taggedContent.CreateTableTDElement();
                    td.SetText(cell);
                    dataRow.AppendChild(td);
                }
            }

            // Save the PDF
            doc.Save("DynamicTable.pdf");
        }

        Console.WriteLine("PDF with dynamic table created successfully.");
    }
}