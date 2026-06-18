using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    // DTO for a table cell
    public class CellInfo
    {
        public string? AlternativeText { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public int RowSpan { get; set; }
        public int ColSpan { get; set; }
        public string? BackgroundColor { get; set; }
        public string? Alignment { get; set; }
    }

    // DTO for a table row
    public class RowInfo
    {
        public string? AlternativeText { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? BackgroundColor { get; set; }
        // TableTRElement does not expose an Alignment property, so we omit it.
        public List<CellInfo> Cells { get; set; } = new List<CellInfo>();
    }

    // DTO for a table
    public class TableInfo
    {
        public string? AlternativeText { get; set; }
        public string? Title { get; set; }
        public string? Alignment { get; set; }
        public string? BackgroundColor { get; set; }
        public List<RowInfo> Rows { get; set; } = new List<RowInfo>();
    }

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputJsonPath = "table_structure.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: using block for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content
            ITaggedContent tagged = doc.TaggedContent;

            // Find all table structure elements in the document
            var tableElements = tagged.RootElement.FindElements<TableElement>(true);

            var tables = new List<TableInfo>();

            foreach (TableElement table in tableElements)
            {
                TableInfo tableInfo = new TableInfo
                {
                    AlternativeText = table.AlternativeText,
                    Title = table.Title,
                    // HorizontalAlignment is an enum, not nullable – call ToString() directly
                    Alignment = table.Alignment.ToString(),
                    BackgroundColor = table.BackgroundColor?.ToString()
                };

                // Find all row elements (TR) within this table
                var rowElements = table.FindElements<TableTRElement>(true);
                foreach (TableTRElement row in rowElements)
                {
                    RowInfo rowInfo = new RowInfo
                    {
                        AlternativeText = row.AlternativeText,
                        Title = row.Title,
                        Text = row.ActualText,
                        BackgroundColor = row.BackgroundColor?.ToString()
                        // No Alignment property on TableTRElement – omitted
                    };

                    // Find all cell elements (TD) within this row
                    var cellElements = row.FindElements<TableTDElement>(true);
                    foreach (TableTDElement cell in cellElements)
                    {
                        CellInfo cellInfo = new CellInfo
                        {
                            AlternativeText = cell.AlternativeText,
                            Title = cell.Title,
                            Text = cell.ActualText,
                            RowSpan = cell.RowSpan,
                            ColSpan = cell.ColSpan,
                            BackgroundColor = cell.BackgroundColor?.ToString(),
                            // HorizontalAlignment is an enum, not nullable – call ToString() directly
                            Alignment = cell.Alignment.ToString()
                        };
                        rowInfo.Cells.Add(cellInfo);
                    }

                    tableInfo.Rows.Add(rowInfo);
                }

                tables.Add(tableInfo);
            }

            // Serialize the collected structure to JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(tables, jsonOptions);

            // Write JSON to file
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Table structure exported to '{outputJsonPath}'.");
        }
    }
}
