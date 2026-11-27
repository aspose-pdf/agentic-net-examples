using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "FirstTable.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            AbsorbedTable firstTable = null;

            // Iterate pages (1‑based indexing) until a table is found
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page); // correct method to extract tables

                if (absorber.TableList.Count > 0)
                {
                    firstTable = absorber.TableList[0]; // take the first extracted table
                    break;
                }
            }

            if (firstTable == null)
            {
                Console.WriteLine("No tables were found in the document.");
                return;
            }

            // Write the extracted table to CSV
            using (StreamWriter writer = new StreamWriter(outputCsvPath))
            {
                foreach (var row in firstTable.RowList)
                {
                    List<string> csvFields = new List<string>();

                    foreach (var cell in row.CellList)
                    {
                        // Concatenate all text fragments inside the cell
                        string cellText = string.Empty;
                        foreach (var fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }

                        // Escape double quotes by doubling them and wrap the field in quotes
                        cellText = cellText?.Replace("\"", "\"\"") ?? string.Empty;
                        csvFields.Add($"\"{cellText}\"");
                    }

                    writer.WriteLine(string.Join(",", csvFields));
                }
            }

            Console.WriteLine($"First table exported to '{outputCsvPath}'.");
        }
    }
}
