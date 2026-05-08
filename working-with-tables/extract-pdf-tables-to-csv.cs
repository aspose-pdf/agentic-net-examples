using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "TablesCsv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (lifecycle rule: use Document constructor and using block)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Create a new TableAbsorber for the current page
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables on this page
                absorber.Visit(page);

                // Process each extracted table
                for (int tblIdx = 0; tblIdx < absorber.TableList.Count; tblIdx++)
                {
                    var absorbedTable = absorber.TableList[tblIdx];

                    // Build a CSV file name that reflects page and table numbers
                    string csvPath = Path.Combine(outputDir,
                        $"page{pageNum}_table{tblIdx + 1}.csv");

                    // Write the table content to CSV
                    using (StreamWriter writer = new StreamWriter(csvPath))
                    {
                        foreach (var row in absorbedTable.RowList)
                        {
                            List<string> cellTexts = new List<string>();

                            foreach (var cell in row.CellList)
                            {
                                // Concatenate all text fragments inside the cell
                                string cellText = string.Empty;
                                foreach (var fragment in cell.TextFragments)
                                {
                                    cellText += fragment.Text;
                                }

                                // Escape commas and double quotes according to CSV rules
                                if (cellText.Contains("\"") || cellText.Contains(","))
                                {
                                    cellText = $"\"{cellText.Replace("\"", "\"\"")}\"";
                                }

                                cellTexts.Add(cellText);
                            }

                            // Join the cell texts with commas and write the line
                            writer.WriteLine(string.Join(",", cellTexts));
                        }
                    }

                    Console.WriteLine($"Extracted table to: {csvPath}");
                }
            }
        }
    }
}