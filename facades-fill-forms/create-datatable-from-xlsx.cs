using System;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf; // Aspose.Pdf namespace is required by the overall project

class Program
{
    static void Main()
    {
        const string xlsxPath = "input.xlsx";

        if (!File.Exists(xlsxPath))
        {
            Console.Error.WriteLine($"File not found: {xlsxPath}");
            return;
        }

        // Load the worksheet data into a DataTable.
        DataTable table = LoadDataTableFromXlsx(xlsxPath);

        // Example: display the first few rows.
        Console.WriteLine("Columns:");
        foreach (DataColumn col in table.Columns)
            Console.Write($"{col.ColumnName}\t");
        Console.WriteLine();

        Console.WriteLine("Rows:");
        foreach (DataRow row in table.Rows)
        {
            foreach (var item in row.ItemArray)
                Console.Write($"{item}\t");
            Console.WriteLine();
        }

        // The DataTable can now be used for form mapping, e.g. with AutoFiller.
        // using (var filler = new Aspose.Pdf.Facades.AutoFiller())
        // {
        //     filler.BindPdf("template.pdf");
        //     filler.ImportDataTable(table);
        //     filler.Save("filled.pdf");
        // }
    }

    /// <summary>
    /// Reads the first worksheet of an XLSX file and creates a DataTable.
    /// The first row of the sheet is used as column headers.
    /// </summary>
    /// <param name="xlsxFilePath">Path to the .xlsx file.</param>
    /// <returns>DataTable populated with the worksheet data.</returns>
    private static DataTable LoadDataTableFromXlsx(string xlsxFilePath)
    {
        var dataTable = new DataTable();
        // Load shared strings (if any)
        var sharedStrings = new System.Collections.Generic.List<string>();
        using (var zip = ZipFile.OpenRead(xlsxFilePath))
        {
            // ---- 1. Read shared strings ----
            var sharedStringsEntry = zip.GetEntry("xl/sharedStrings.xml");
            if (sharedStringsEntry != null)
            {
                using (var stream = sharedStringsEntry.Open())
                {
                    XDocument sstDoc = XDocument.Load(stream);
                    XNamespace ns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
                    foreach (var si in sstDoc.Descendants(ns + "si"))
                    {
                        // A shared string can contain multiple <t> elements (rich text). Concatenate them.
                        var text = string.Concat(si.Descendants(ns + "t").Select(t => t.Value));
                        sharedStrings.Add(text);
                    }
                }
            }

            // ---- 2. Locate the first worksheet ----
            // For simplicity we assume the first sheet is "sheet1.xml". In a full implementation we would parse xl/workbook.xml.
            var sheetEntry = zip.GetEntry("xl/worksheets/sheet1.xml");
            if (sheetEntry == null)
                throw new FileNotFoundException("The first worksheet (sheet1.xml) could not be found in the XLSX package.");

            using (var stream = sheetEntry.Open())
            {
                XDocument sheetDoc = XDocument.Load(stream);
                XNamespace ns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
                var rows = sheetDoc.Root
                                   .Element(ns + "sheetData")
                                   .Elements(ns + "row");

                bool isFirstRow = true;
                foreach (var row in rows)
                {
                    // Skip rows that have no cells.
                    var cells = row.Elements(ns + "c").ToList();
                    if (!cells.Any())
                        continue;

                    if (isFirstRow)
                    {
                        // Create columns from the first row.
                        foreach (var cell in cells)
                        {
                            string columnName = GetCellStringValue(cell, sharedStrings);
                            if (string.IsNullOrWhiteSpace(columnName))
                                columnName = $"Column{GetColumnName(cell.Attribute("r")?.Value ?? "A")}";
                            if (dataTable.Columns.Contains(columnName))
                                columnName = $"{columnName}_{dataTable.Columns.Count}";
                            dataTable.Columns.Add(columnName, typeof(string));
                        }
                        isFirstRow = false;
                    }
                    else
                    {
                        DataRow dataRow = dataTable.NewRow();
                        int cellIndex = 0;
                        foreach (var cell in cells)
                        {
                            // Determine the column index for this cell.
                            string cellRef = cell.Attribute("r")?.Value ?? ""; // e.g., "C5"
                            string colName = GetColumnName(cellRef);
                            int columnIndex = GetColumnIndexFromName(colName);

                            // Fill any gaps with empty strings.
                            while (cellIndex < columnIndex && cellIndex < dataTable.Columns.Count)
                            {
                                dataRow[cellIndex] = string.Empty;
                                cellIndex++;
                            }

                            if (cellIndex < dataTable.Columns.Count)
                            {
                                dataRow[cellIndex] = GetCellStringValue(cell, sharedStrings);
                                cellIndex++;
                            }
                        }
                        // Fill trailing empty cells.
                        while (cellIndex < dataTable.Columns.Count)
                        {
                            dataRow[cellIndex] = string.Empty;
                            cellIndex++;
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }
            }
        }
        return dataTable;
    }

    /// <summary>
    /// Retrieves the string value of a cell, handling shared strings and inline strings.
    /// </summary>
    private static string GetCellStringValue(XElement cell, System.Collections.Generic.List<string> sharedStrings)
    {
        if (cell == null)
            return string.Empty;

        XNamespace ns = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";
        string cellType = cell.Attribute("t")?.Value; // "s" = shared string, "inlineStr" = inline string, etc.
        var vElement = cell.Element(ns + "v");
        var isElement = cell.Element(ns + "is");

        if (cellType == "s") // shared string
        {
            if (vElement != null && int.TryParse(vElement.Value, out int sstIndex) && sstIndex >= 0 && sstIndex < sharedStrings.Count)
                return sharedStrings[sstIndex];
            return string.Empty;
        }
        else if (cellType == "inlineStr" && isElement != null)
        {
            // Inline string may contain multiple <t> elements.
            return string.Concat(isElement.Descendants(ns + "t").Select(t => t.Value));
        }
        else if (vElement != null)
        {
            // For numbers, dates, booleans etc., just return the raw value.
            return vElement.Value;
        }
        return string.Empty;
    }

    /// <summary>
    /// Extracts the column name (e.g., "A", "BC") from a cell reference like "BC23".
    /// </summary>
    private static string GetColumnName(string cellReference)
    {
        if (string.IsNullOrEmpty(cellReference))
            return string.Empty;
        string columnName = string.Empty;
        foreach (char ch in cellReference)
        {
            if (char.IsLetter(ch))
                columnName += ch;
            else
                break;
        }
        return columnName;
    }

    /// <summary>
    /// Converts an Excel column name to a zero‑based column index.
    /// </summary>
    private static int GetColumnIndexFromName(string columnName)
    {
        if (string.IsNullOrEmpty(columnName))
            return 0;
        int index = 0;
        foreach (char ch in columnName.ToUpperInvariant())
        {
            index *= 26;
            index += (ch - 'A' + 1);
        }
        return index - 1; // zero‑based
    }
}
