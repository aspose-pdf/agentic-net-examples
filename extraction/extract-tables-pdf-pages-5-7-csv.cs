using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string csvPath   = "Pages5to7.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Determine the actual range (pages are 1‑based)
                int startPage = 5;
                int endPage   = Math.Min(7, doc.Pages.Count);

                // Open the CSV file for writing
                using (StreamWriter writer = new StreamWriter(csvPath, false))
                {
                    // Iterate through pages 5 to 7
                    for (int pageIndex = startPage; pageIndex <= endPage; pageIndex++)
                    {
                        Page page = doc.Pages[pageIndex];

                        // Extract tables from the current page using TableAbsorber
                        TableAbsorber absorber = new TableAbsorber();
                        absorber.Visit(page);

                        // Walk through each absorbed table
                        foreach (AbsorbedTable absorbedTable in absorber.TableList)
                        {
                            // Walk through each row of the absorbed table
                            foreach (AbsorbedRow absorbedRow in absorbedTable.RowList)
                            {
                                List<string> cellValues = new List<string>();

                                // Walk through each cell of the row
                                foreach (AbsorbedCell absorbedCell in absorbedRow.CellList)
                                {
                                    // Concatenate all text fragments inside the cell
                                    string cellText = string.Empty;
                                    foreach (TextFragment fragment in absorbedCell.TextFragments)
                                    {
                                        cellText += fragment.Text;
                                    }

                                    // Escape CSV special characters
                                    cellText = cellText?.Replace("\"", "\"\"") ?? string.Empty;
                                    if (cellText.Contains(",") || cellText.Contains("\"") || cellText.Contains("\n"))
                                        cellText = $"\"{cellText}\"";

                                    cellValues.Add(cellText);
                                }

                                // Write the CSV line for the current row
                                writer.WriteLine(string.Join(",", cellValues));
                            }
                            // Optional blank line between tables
                            writer.WriteLine();
                        }
                    }
                }

                Console.WriteLine($"Tables from pages {startPage}-{endPage} saved to '{csvPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
