using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string jsonPath = "table_structure.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Find all table structure elements in the document
            var tables = root.FindElements<TableElement>(true);

            var tableInfos = new List<TableInfo>();

            foreach (TableElement table in tables)
            {
                TableInfo tableInfo = new TableInfo {
                    AlternativeText = table.AlternativeText,
                    Title = table.Title,
                    Language = table.Language,
                    Rows = new List<RowInfo>()
                };

                // Iterate over child elements of the table (rows)
                foreach (Element child in table.ChildElements)
                {
                    if (child is TableTRElement row)
                    {
                        RowInfo rowInfo = new RowInfo {
                            AlternativeText = row.AlternativeText,
                            Title = row.Title,
                            Language = row.Language,
                            Cells = new List<CellInfo>()
                        };

                        // Iterate over cells within the row
                        foreach (Element cellElem in row.ChildElements)
                        {
                            if (cellElem is TableTDElement cell)
                            {
                                CellInfo cellInfo = new CellInfo {
                                    AlternativeText = cell.AlternativeText,
                                    Title = cell.Title,
                                    Language = cell.Language,
                                    ActualText = cell.ActualText,
                                    RowSpan = cell.RowSpan,
                                    ColSpan = cell.ColSpan,
                                    BackgroundColor = cell.BackgroundColor?.ToString(),
                                    Border = cell.Border?.ToString()
                                };
                                rowInfo.Cells.Add(cellInfo);
                            }
                        }

                        tableInfo.Rows.Add(rowInfo);
                    }
                }

                tableInfos.Add(tableInfo);
            }

            // Serialize the collected structure to JSON with indentation
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(tableInfos, jsonOptions);
            File.WriteAllText(jsonPath, json);
        }

        Console.WriteLine($"Table structure exported to {jsonPath}");
    }

    // DTO classes used for JSON serialization
    class TableInfo
    {
        public string AlternativeText { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public List<RowInfo> Rows { get; set; }
    }

    class RowInfo
    {
        public string AlternativeText { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public List<CellInfo> Cells { get; set; }
    }

    class CellInfo
    {
        public string AlternativeText { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string ActualText { get; set; }
        public int RowSpan { get; set; }
        public int ColSpan { get; set; }
        public string BackgroundColor { get; set; }
        public string Border { get; set; }
    }
}