using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class TableCellInfo
{
    public string CellType { get; set; } = string.Empty;          // "Header" or "Data"
    public string Text { get; set; } = string.Empty;              // Visible text (ActualText)
    public string AlternativeText { get; set; } = string.Empty;   // /Alt text if present
}

class TableRowInfo
{
    public List<TableCellInfo> Cells { get; set; } = new List<TableCellInfo>();
}

class TableInfo
{
    public List<TableRowInfo> Rows { get; set; } = new List<TableRowInfo>();
}

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "tables.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Verify that the PDF is tagged. In newer Aspose.Pdf versions the IsTagged property does not exist.
                if (doc.TaggedContent == null)
                {
                    Console.WriteLine("Document is not a tagged PDF. No table structure to export.");
                    return;
                }

                ITaggedContent tagged = doc.TaggedContent;
                StructureElement root = tagged.RootElement;

                // Find all TableElement objects in the structure tree.
                var tableElements = root.FindElements<TableElement>(true);
                var tables = new List<TableInfo>();

                foreach (TableElement table in tableElements)
                {
                    TableInfo tableInfo = new TableInfo();

                    // Retrieve all rows (TableTRElement) belonging to this table.
                    var rows = table.FindElements<TableTRElement>(true);
                    foreach (TableTRElement row in rows)
                    {
                        TableRowInfo rowInfo = new TableRowInfo();

                        // Iterate over each cell in the row.
                        foreach (Element cell in row.ChildElements)
                        {
                            if (cell is TableTHElement th)
                            {
                                TableCellInfo cellInfo = new TableCellInfo
                                {
                                    CellType = "Header",
                                    Text = th.ActualText ?? string.Empty,
                                    AlternativeText = th.AlternativeText ?? string.Empty
                                };
                                rowInfo.Cells.Add(cellInfo);
                            }
                            else if (cell is TableTDElement td)
                            {
                                TableCellInfo cellInfo = new TableCellInfo
                                {
                                    CellType = "Data",
                                    Text = td.ActualText ?? string.Empty,
                                    AlternativeText = td.AlternativeText ?? string.Empty
                                };
                                rowInfo.Cells.Add(cellInfo);
                            }
                        }

                        tableInfo.Rows.Add(rowInfo);
                    }

                    tables.Add(tableInfo);
                }

                // Serialize the collected table information to JSON.
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(tables, jsonOptions);
                File.WriteAllText(outputJson, json);

                Console.WriteLine($"Exported {tables.Count} table(s) to '{outputJson}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
