using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ExportTableWithBordersToCsv
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "output.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Absorb tables from the document
            TableAbsorber tableAbsorber = new TableAbsorber();
            tableAbsorber.Visit(pdfDoc);

            // Write CSV with visual border markers
            using (StreamWriter csvWriter = new StreamWriter(outputCsvPath))
            {
                // Iterate over absorbed tables using the correct API (TableList collection)
                for (int t = 0; t < tableAbsorber.TableList.Count; t++)
                {
                    AbsorbedTable table = tableAbsorber.TableList[t];

                    // Iterate over rows (RowList collection)
                    for (int r = 0; r < table.RowList.Count; r++)
                    {
                        AbsorbedRow row = table.RowList[r];
                        List<string> cellValues = new List<string>();

                        // Iterate over cells (CellList collection)
                        for (int c = 0; c < row.CellList.Count; c++)
                        {
                            AbsorbedCell cell = row.CellList[c];

                            // Concatenate all text fragments inside the cell
                            string cellText = string.Join(" ",
                                cell.TextFragments.Select(tf => tf.Text.Trim()));

                            // If the cell has any border information, wrap the text with delimiters
                            if (cell.BorderInfo != null)
                            {
                                cellText = $"|{cellText}|";
                            }

                            // Escape commas in the cell text
                            if (cellText.Contains(","))
                            {
                                cellText = $"\"{cellText}\"";
                            }

                            cellValues.Add(cellText);
                        }

                        // Write the CSV line (comma‑separated values)
                        csvWriter.WriteLine(string.Join(",", cellValues));
                    }
                }
            }
        }

        Console.WriteLine($"CSV exported with border markers to '{outputCsvPath}'.");
    }
}
