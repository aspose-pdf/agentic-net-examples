using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdf = "input.pdf";

        // Ensure the source PDF exists – if not, create a simple PDF with a table for demo purposes
        if (!File.Exists(inputPdf))
        {
            CreateSamplePdfWithTable(inputPdf);
            Console.WriteLine($"Sample PDF created at '{inputPdf}'.");
        }

        // Directory where CSV files will be saved
        const string outputDir = "TablesCsv";
        Directory.CreateDirectory(outputDir);

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            int tableIndex = 1;
            // Iterate over each detected table
            foreach (var absorbedTable in absorber.TableList)
            {
                string csvPath = Path.Combine(outputDir, $"Table_{tableIndex}.csv");

                // Write the table data to a CSV file
                using (StreamWriter writer = new StreamWriter(csvPath, false, Encoding.UTF8))
                {
                    // Iterate rows
                    foreach (var row in absorbedTable.RowList)
                    {
                        var cellValues = new List<string>();

                        // Iterate cells in the current row
                        foreach (var cell in row.CellList)
                        {
                            // Concatenate all text fragments inside the cell
                            StringBuilder cellText = new StringBuilder();
                            foreach (var fragment in cell.TextFragments)
                            {
                                cellText.Append(fragment.Text);
                            }

                            // Escape the cell text for CSV format
                            cellValues.Add(EscapeForCsv(cellText.ToString()));
                        }

                        // Write the CSV line for the current row
                        writer.WriteLine(string.Join(",", cellValues));
                    }
                }

                Console.WriteLine($"Exported table {tableIndex} to '{csvPath}'.");
                tableIndex++;
            }
        }
    }

    // Helper method to escape CSV fields according to RFC 4180
    static string EscapeForCsv(string field)
    {
        if (field.Contains('"') || field.Contains(',') || field.Contains('\n') || field.Contains('\r'))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }

    // Creates a minimal PDF containing a single table – used when the input file is missing.
    static void CreateSamplePdfWithTable(string path)
    {
        // Create a new empty document
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Build a simple 3x3 table
        Table table = new Table
        {
            ColumnWidths = "100 100 100",
            Border = new BorderInfo(BorderSide.All, 0.5f)
        };

        // Header row
        Row header = table.Rows.Add();
        header.Cells.Add("Header 1");
        header.Cells.Add("Header 2");
        header.Cells.Add("Header 3");
        header.BackgroundColor = Color.LightGray;

        // Data rows
        for (int i = 1; i <= 2; i++)
        {
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add($"R{i}C1");
            dataRow.Cells.Add($"R{i}C2");
            dataRow.Cells.Add($"R{i}C3");
        }

        // Add the table to the page
        page.Paragraphs.Add(table);

        // Save the document
        doc.Save(path);
    }
}
