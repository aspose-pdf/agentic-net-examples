using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedTables";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];

                    // TableAbsorber extracts tables from the current page
                    TableAbsorber absorber = new TableAbsorber();
                    absorber.Visit(page); // Correct method – Visit, not Accept

                    // Process each extracted table
                    for (int tblIdx = 0; tblIdx < absorber.TableList.Count; tblIdx++)
                    {
                        var absorbedTable = absorber.TableList[tblIdx];

                        // Build CSV file name: Table_page{page}_index{tblIdx}.csv
                        string csvPath = Path.Combine(
                            outputFolder,
                            $"Table_page{pageNum}_index{tblIdx + 1}.csv");

                        using (StreamWriter writer = new StreamWriter(csvPath))
                        {
                            // Iterate rows
                            foreach (var row in absorbedTable.RowList)
                            {
                                List<string> cellTexts = new List<string>();

                                // Iterate cells in the current row
                                foreach (var cell in row.CellList)
                                {
                                    // Concatenate all text fragments belonging to the cell
                                    string cellText = string.Empty;
                                    foreach (var fragment in cell.TextFragments)
                                    {
                                        cellText += fragment.Text;
                                    }

                                    // CSV escaping (double quotes, commas, new lines)
                                    string escaped = cellText?.Replace("\"", "\"\"") ?? string.Empty;
                                    if (escaped.Contains(",") || escaped.Contains("\"") || escaped.Contains("\n"))
                                        escaped = $"\"{escaped}\"";

                                    cellTexts.Add(escaped);
                                }

                                // Write the CSV line for the current row
                                writer.WriteLine(string.Join(",", cellTexts));
                            }
                        }

                        Console.WriteLine($"Extracted table saved to: {csvPath}");
                    }
                }
            }

            Console.WriteLine("Table extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
