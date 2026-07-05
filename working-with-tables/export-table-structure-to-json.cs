using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

namespace TableStructureExport
{
    // Simple POCO classes that will be serialized to JSON
    public class TableInfo
    {
        public string AlternativeText { get; set; }
        public string Title { get; set; }
        public List<RowInfo> Rows { get; set; }
    }

    public class RowInfo
    {
        public string AlternativeText { get; set; }
        public string Title { get; set; }
        public List<CellInfo> Cells { get; set; }
    }

    public class CellInfo
    {
        public string AlternativeText { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int RowSpan { get; set; }
        public int ColSpan { get; set; }
        public string BackgroundColor { get; set; }
        public string Border { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string inputPath = "input.pdf";
            const string outputPath = "table_structure.json";

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Load the PDF document inside a using block (lifecycle rule)
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content (required for logical structure)
                ITaggedContent tagged = doc.TaggedContent;

                // Collect all tables in the document (recursive search)
                var tables = tagged.RootElement.FindElements<TableElement>(true);
                var export = new List<TableInfo>();

                foreach (TableElement table in tables)
                {
                    TableInfo tableInfo = new TableInfo {
                        AlternativeText = table.AlternativeText,
                        Title = table.Title,
                        Rows = new List<RowInfo>()
                    };

                    // Find all rows (TR elements) belonging to this table
                    var rows = table.FindElements<TableTRElement>(true);
                    foreach (TableTRElement row in rows)
                    {
                        RowInfo rowInfo = new RowInfo {
                            AlternativeText = row.AlternativeText,
                            Title = row.Title,
                            Cells = new List<CellInfo>()
                        };

                        // Find all cells (TD elements) belonging to this row
                        var cells = row.FindElements<TableTDElement>(true);
                        foreach (TableTDElement cell in cells)
                        {
                            CellInfo cellInfo = new CellInfo {
                                AlternativeText = cell.AlternativeText,
                                Title = cell.Title,
                                Text = cell.ActualText,
                                RowSpan = cell.RowSpan,
                                ColSpan = cell.ColSpan,
                                BackgroundColor = cell.BackgroundColor?.ToString(),
                                Border = cell.Border?.ToString()
                            };
                            rowInfo.Cells.Add(cellInfo);
                        }

                        tableInfo.Rows.Add(rowInfo);
                    }

                    export.Add(tableInfo);
                }

                // Serialize the collected structure to JSON with indentation
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                    JsonSerializer.Serialize(fs, export, jsonOptions);
                }

                Console.WriteLine($"Table structure exported to '{outputPath}'.");
            }
        }
    }
}