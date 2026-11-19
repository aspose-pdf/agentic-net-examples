using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    // DTOs for JSON serialization
    public class TableInfo
    {
        public string AlternativeText { get; set; }
        public List<RowInfo> Rows { get; set; } = new List<RowInfo>();
    }

    public class RowInfo
    {
        public int Index { get; set; }
        public List<CellInfo> Cells { get; set; } = new List<CellInfo>();
    }

    public class CellInfo
    {
        public string CellType { get; set; }   // "Header" or "Data"
        public string Text { get; set; }
        public string AlternativeText { get; set; }
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "tables.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Access tagged content
            ITaggedContent tagged = doc.TaggedContent;

            // Find all table elements in the structure tree (recursive search)
            List<TableElement> tables = tagged.RootElement.FindElements<TableElement>(true);

            var tablesInfo = new List<TableInfo>();

            foreach (TableElement table in tables)
            {
                TableInfo tableInfo = new TableInfo {
                    AlternativeText = table.AlternativeText
                };

                int rowIndex = 0;

                // Helper local function to process a row element
                void ProcessRow(TableTRElement row)
                {
                    RowInfo rowInfo = new RowInfo { Index = ++rowIndex };

                    foreach (Element cell in row.ChildElements)
                    {
                        CellInfo cellInfo = new CellInfo();

                        if (cell is TableTHElement th)
                        {
                            cellInfo.CellType = "Header";
                            cellInfo.Text = th.ActualText;
                            cellInfo.AlternativeText = th.AlternativeText;
                        }
                        else if (cell is TableTDElement td)
                        {
                            cellInfo.CellType = "Data";
                            cellInfo.Text = td.ActualText;
                            cellInfo.AlternativeText = td.AlternativeText;
                        }
                        else
                        {
                            // Skip unknown elements
                            continue;
                        }

                        rowInfo.Cells.Add(cellInfo);
                    }

                    tableInfo.Rows.Add(rowInfo);
                }

                // Iterate over possible sections: THead, TBody, TFoot
                foreach (Element section in table.ChildElements)
                {
                    if (section is TableTHeadElement thead)
                    {
                        foreach (Element row in thead.ChildElements)
                        {
                            if (row is TableTRElement tr) ProcessRow(tr);
                        }
                    }
                    else if (section is TableTBodyElement tbody)
                    {
                        foreach (Element row in tbody.ChildElements)
                        {
                            if (row is TableTRElement tr) ProcessRow(tr);
                        }
                    }
                    else if (section is TableTFootElement tfoot)
                    {
                        foreach (Element row in tfoot.ChildElements)
                        {
                            if (row is TableTRElement tr) ProcessRow(tr);
                        }
                    }
                }

                tablesInfo.Add(tableInfo);
            }

            // Serialize the collected table information to JSON
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(tablesInfo, jsonOptions);
            File.WriteAllText(outputJson, json);

            Console.WriteLine($"Exported {tablesInfo.Count} table(s) to '{outputJson}'.");
        }
    }
}