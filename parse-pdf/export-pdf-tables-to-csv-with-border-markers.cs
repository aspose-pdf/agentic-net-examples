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
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "output.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Extract tables from the PDF
            TableAbsorber tableAbsorber = new TableAbsorber();
            tableAbsorber.Visit(pdfDoc);

            StringBuilder csvBuilder = new StringBuilder();

            // Iterate over each extracted table
            foreach (var table in tableAbsorber.TableList) // TableInfo objects
            {
                // Iterate over rows
                foreach (var row in table.RowList) // RowInfo objects
                {
                    List<string> cellValues = new List<string>();

                    // Iterate over cells in the row
                    foreach (var cell in row.CellList) // CellInfo objects
                    {
                        // Retrieve cell text (concatenate all fragments if more than one)
                        string cellText = string.Empty;
                        foreach (TextFragment fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }

                        // Add visual border markers for CSV (e.g., wrap content with '|')
                        string markedText = $"|{cellText}|";

                        // Escape double quotes for CSV compliance
                        markedText = $"\"{markedText.Replace("\"", "\"\"")}\"";

                        cellValues.Add(markedText);
                    }

                    // Join cell values with commas and add a new line
                    csvBuilder.AppendLine(string.Join(",", cellValues));
                }

                // Add an empty line to separate tables
                csvBuilder.AppendLine();
            }

            // Write the CSV content to file (non‑PDF save handled via System.IO)
            File.WriteAllText(outputCsvPath, csvBuilder.ToString());
        }

        Console.WriteLine($"CSV exported with border markers to '{outputCsvPath}'.");
    }
}
