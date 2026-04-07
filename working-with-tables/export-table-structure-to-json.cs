using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class TableStructureExporter
{
    // DTO for JSON serialization
    private class CellDto
    {
        public string ActualText { get; set; }
        public string AlternativeText { get; set; }
    }

    private class RowDto
    {
        public List<CellDto> Cells { get; set; } = new List<CellDto>();
    }

    private class TableDto
    {
        public List<RowDto> Rows { get; set; } = new List<RowDto>();
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "table_structure.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Access tagged content
            ITaggedContent tagged = doc.TaggedContent;

            // Find the first TableElement in the structure tree
            TableElement table = null;
            foreach (Element elem in tagged.RootElement.ChildElements)
            {
                if (elem is TableElement te)
                {
                    table = te;
                    break;
                }
            }

            if (table == null)
            {
                Console.Error.WriteLine("No table structure element found in the document.");
                return;
            }

            // Prepare DTO for JSON
            TableDto tableDto = new TableDto();

            // Iterate over rows (TableTRElement)
            foreach (Element rowElem in table.ChildElements)
            {
                if (rowElem is TableTRElement row)
                {
                    RowDto rowDto = new RowDto();

                    // Iterate over cells (TableTDElement)
                    foreach (Element cellElem in row.ChildElements)
                    {
                        if (cellElem is TableTDElement cell)
                        {
                            CellDto cellDto = new CellDto
                            {
                                ActualText = cell.ActualText,
                                AlternativeText = cell.AlternativeText
                            };
                            rowDto.Cells.Add(cellDto);
                        }
                    }

                    tableDto.Rows.Add(rowDto);
                }
            }

            // Serialize to JSON (indented for readability)
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(tableDto, jsonOptions);

            // Write JSON to file
            File.WriteAllText(outputJsonPath, json);
            Console.WriteLine($"Table structure exported to '{outputJsonPath}'.");
        }
    }
}