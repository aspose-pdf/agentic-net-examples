using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "TablesList.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Extract tables from all pages using TableAbsorber (core API)
                TableAbsorber absorber = new TableAbsorber();
                foreach (Page page in doc.Pages)
                {
                    absorber.Visit(page); // collect tables from the current page
                }

                // Serialize extracted tables to CSV
                using (StreamWriter writer = new StreamWriter(outputCsv, false, Encoding.UTF8))
                {
                    int tableNumber = 1;
                    foreach (AbsorbedTable table in absorber.TableList)
                    {
                        // Optional header to identify each table in the CSV
                        writer.WriteLine($"# Table {tableNumber}");

                        foreach (AbsorbedRow row in table.RowList)
                        {
                            List<string> cells = new List<string>();
                            foreach (AbsorbedCell cell in row.CellList)
                            {
                                // Concatenate all text fragments inside the cell
                                string cellText = string.Empty;
                                if (cell.TextFragments != null && cell.TextFragments.Count > 0)
                                {
                                    StringBuilder sb = new StringBuilder();
                                    foreach (TextFragment fragment in cell.TextFragments)
                                    {
                                        sb.Append(fragment.Text);
                                    }
                                    cellText = sb.ToString();
                                }

                                // Escape double quotes and wrap each cell value in quotes for CSV compliance
                                cellText = cellText.Replace("\"", "\"\"");
                                cells.Add($"\"{cellText}\"");
                            }
                            writer.WriteLine(string.Join(",", cells));
                        }

                        // Blank line between tables
                        writer.WriteLine();
                        tableNumber++;
                    }
                }

                Console.WriteLine($"Tables extracted and saved to '{outputCsv}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
