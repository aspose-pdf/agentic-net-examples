using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class ExportTableStructureToJson
{
    // DTO for a table cell
    private class CellDto
    {
        public string? ActualText { get; set; }
        public string? AlternativeText { get; set; }
        public string? Title { get; set; }
        public string? Alignment { get; set; }
        public string? BackgroundColor { get; set; }
        public string? Border { get; set; }
        public int? RowSpan { get; set; }
        public int? ColSpan { get; set; }
    }

    // DTO for a table row
    private class RowDto
    {
        public string? AlternativeText { get; set; }
        public string? Title { get; set; }
        public string? BackgroundColor { get; set; }
        public string? Border { get; set; }
        public List<CellDto> Cells { get; set; } = new List<CellDto>();
    }

    // DTO for a table
    private class TableDto
    {
        public string? AlternativeText { get; set; }
        public string? Title { get; set; }
        public string? Alignment { get; set; }
        public string? BackgroundColor { get; set; }
        public string? Border { get; set; }
        public List<RowDto> Rows { get; set; } = new List<RowDto>();
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF containing a tagged table
        const string outputJsonPath = "table_structure.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Access tagged content (must use ITaggedContent, not cast Document)
            ITaggedContent taggedContent = doc.TaggedContent;

            // Find all TableElement objects in the structure tree (recursive search)
            var tableElements = taggedContent.RootElement.FindElements<TableElement>(true);

            var tables = new List<TableDto>();

            foreach (TableElement table in tableElements)
            {
                TableDto tableDto = new TableDto {
                    AlternativeText = table.AlternativeText,
                    Title = table.Title,
                    // HorizontalAlignment is a non‑nullable enum; use direct ToString()
                    Alignment = table.Alignment.ToString(),
                    BackgroundColor = table.BackgroundColor?.ToString(),
                    Border = table.Border?.ToString()
                };

                // Iterate over child elements of the table (rows)
                foreach (Element rowElem in table.ChildElements)
                {
                    if (rowElem is TableTRElement row)
                    {
                        RowDto rowDto = new RowDto {
                            AlternativeText = row.AlternativeText,
                            Title = row.Title,
                            BackgroundColor = row.BackgroundColor?.ToString(),
                            Border = row.Border?.ToString()
                        };

                        // Iterate over cells within the row
                        foreach (Element cellElem in row.ChildElements)
                        {
                            if (cellElem is TableTDElement cell)
                            {
                                CellDto cellDto = new CellDto {
                                    ActualText = cell.ActualText,
                                    AlternativeText = cell.AlternativeText,
                                    Title = cell.Title,
                                    // HorizontalAlignment is a non‑nullable enum; use direct ToString()
                                    Alignment = cell.Alignment.ToString(),
                                    BackgroundColor = cell.BackgroundColor?.ToString(),
                                    Border = cell.Border?.ToString(),
                                    RowSpan = cell.RowSpan,
                                    ColSpan = cell.ColSpan
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

            // Write JSON to file
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Table structure exported to '{outputJsonPath}'.");
        }
    }
}
