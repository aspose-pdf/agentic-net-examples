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
        const string inputPdf  = "input.pdf";
        const string outputCsv = "tables.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            ExportTablesToCsv(inputPdf, outputCsv);
            Console.WriteLine($"Tables exported to CSV: {outputCsv}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Extracts all tables from a PDF and writes them to a CSV file.
    /// Cell borders are represented by visual delimiters:
    ///   - Each cell is wrapped with '|' characters.
    ///   - Horizontal borders are shown as a line of dashes between rows.
    /// </summary>
    static void ExportTablesToCsv(string pdfPath, string csvPath)
    {
        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Prepare a StringBuilder to accumulate CSV content
            StringBuilder csvBuilder = new StringBuilder();

            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Use TableAbsorber to find tables on the current page
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page);

                // If no tables were found, continue to next page
                if (absorber.TableList == null || absorber.TableList.Count == 0)
                    continue;

                // Process each discovered table
                foreach (AbsorbedTable absorbedTable in absorber.TableList)
                {
                    // Optional: add a marker indicating start of a new table
                    csvBuilder.AppendLine("# Table Start (Page " + pageIndex + ")");

                    // Iterate rows
                    foreach (AbsorbedRow row in absorbedTable.RowList)
                    {
                        List<string> cellValues = new List<string>();

                        // Iterate cells within the row
                        foreach (AbsorbedCell cell in row.CellList)
                        {
                            // Concatenate all text fragments inside the cell
                            StringBuilder cellText = new StringBuilder();
                            foreach (TextFragment fragment in cell.TextFragments)
                            {
                                cellText.Append(fragment.Text);
                            }

                            // Wrap cell content with visual border delimiters
                            string wrapped = $"|{cellText.ToString().Replace(",", " ")}|";
                            cellValues.Add(wrapped);
                        }

                        // Join cell values with commas to form a CSV line
                        csvBuilder.AppendLine(string.Join(",", cellValues));

                        // Add a horizontal border line after each row for visual reference
                        csvBuilder.AppendLine(new string('-', 80));
                    }

                    // Optional: add a marker indicating end of the table
                    csvBuilder.AppendLine("# Table End");
                    csvBuilder.AppendLine(); // blank line between tables
                }
            }

            // Write the accumulated CSV content to the output file
            File.WriteAllText(csvPath, csvBuilder.ToString(), Encoding.UTF8);
        }
    }
}