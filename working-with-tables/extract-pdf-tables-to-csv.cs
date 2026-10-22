using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedTables";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // TableAbsorber extracts tables from the document
                TableAbsorber tableAbsorber = new TableAbsorber();

                // Apply the absorber to all pages using Visit (not Accept)
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    tableAbsorber.Visit(pdfDoc.Pages[pageNum]);
                }

                // Iterate over each detected table
                int tableIndex = 1;
                foreach (var table in tableAbsorber.TableList)
                {
                    string csvPath = Path.Combine(outputFolder, $"Table_{tableIndex}.csv");

                    using (StreamWriter writer = new StreamWriter(csvPath))
                    {
                        // Write each row
                        foreach (var row in table.RowList)
                        {
                            var cellTexts = new List<string>();
                            foreach (var cell in row.CellList)
                            {
                                // Concatenate all text fragments inside the cell
                                string cellText = string.Empty;
                                foreach (var fragment in cell.TextFragments)
                                {
                                    cellText += fragment.Text;
                                }
                                cellText = cellText.Trim();

                                // Escape CSV special characters
                                if (cellText.Contains('"') || cellText.Contains(','))
                                {
                                    cellText = $"\"{cellText.Replace("\"", "\"\"")}\"";
                                }
                                cellTexts.Add(cellText);
                            }

                            // Join cells with commas and write the CSV line
                            writer.WriteLine(string.Join(",", cellTexts));
                        }
                    }

                    Console.WriteLine($"Extracted Table {tableIndex} to '{csvPath}'.");
                    tableIndex++;
                }

                if (tableAbsorber.TableList.Count == 0)
                {
                    Console.WriteLine("No tables were found in the PDF.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
