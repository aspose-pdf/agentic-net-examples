using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    // DTOs for JSON serialization – string properties are nullable to satisfy non‑nullable warnings
    public class TableDto
    {
        public string? AlternativeText { get; set; }
        public string? ActualText { get; set; }
        public List<RowDto> Rows { get; set; } = new List<RowDto>();
    }

    public class RowDto
    {
        public string? AlternativeText { get; set; }
        public string? ActualText { get; set; }
        public List<CellDto> Cells { get; set; } = new List<CellDto>();
    }

    public class CellDto
    {
        public string? AlternativeText { get; set; }
        public string? ActualText { get; set; }
        public int RowSpan { get; set; }
        public int ColSpan { get; set; }
        public string? BackgroundColor { get; set; }
        // BorderInfo does not expose a Color property – we expose its string representation instead
        public string? BorderInfo { get; set; }
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "table_structure.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Access tagged content (must use ITaggedContent, not Document.IsTagged)
            ITaggedContent tagged = doc.TaggedContent;

            // Find all TableElement instances in the structure tree
            List<TableDto> tables = new List<TableDto>();
            var tableElements = doc.TaggedContent.RootElement.FindElements<TableElement>(true);
            foreach (TableElement table in tableElements)
            {
                TableDto tableDto = new TableDto
                {
                    AlternativeText = table.AlternativeText,
                    ActualText = table.ActualText
                };

                // Iterate over child elements of the table (rows are TableTRElement)
                foreach (Element rowElem in table.ChildElements)
                {
                    if (rowElem is TableTRElement row)
                    {
                        RowDto rowDto = new RowDto
                        {
                            AlternativeText = row.AlternativeText,
                            ActualText = row.ActualText
                        };

                        // Iterate over cells within the row (cells are TableTDElement)
                        foreach (Element cellElem in row.ChildElements)
                        {
                            if (cellElem is TableTDElement cell)
                            {
                                CellDto cellDto = new CellDto
                                {
                                    AlternativeText = cell.AlternativeText,
                                    ActualText = cell.ActualText,
                                    RowSpan = cell.RowSpan,
                                    ColSpan = cell.ColSpan,
                                    BackgroundColor = cell.BackgroundColor?.ToString(),
                                    // BorderInfo does not have a Color property; expose its string representation instead
                                    BorderInfo = cell.Border?.ToString()
                                };
                                rowDto.Cells.Add(cellDto);
                            }
                        }

                        tableDto.Rows.Add(rowDto);
                    }
                }

                tables.Add(tableDto);
            }

            // Serialize the collected table structures to JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(tables, jsonOptions);
            File.WriteAllText(outputJson, json);

            Console.WriteLine($"Table structure exported to '{outputJson}'.");
        }
    }
}
