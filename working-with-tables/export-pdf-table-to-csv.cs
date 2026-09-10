using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string csvPath = "output.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Extract tables from the document
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc); // searches all pages

            // Write extracted table data to CSV
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                foreach (var table in absorber.TableList)
                {
                    foreach (var row in table.RowList)
                    {
                        List<string> cellValues = new List<string>();

                        foreach (var cell in row.CellList)
                        {
                            // Concatenate all text fragments inside the cell
                            string cellText = "";
                            foreach (var fragment in cell.TextFragments)
                            {
                                cellText += fragment.Text;
                            }

                            // Escape double quotes
                            if (cellText.Contains("\""))
                                cellText = cellText.Replace("\"", "\"\"");

                            // Enclose in quotes if needed (comma, quote, or newline)
                            if (cellText.Contains(",") || cellText.Contains("\"") ||
                                cellText.Contains("\n") || cellText.Contains("\r"))
                                cellText = $"\"{cellText}\"";

                            cellValues.Add(cellText);
                        }

                        // Write one CSV line per table row
                        writer.WriteLine(string.Join(",", cellValues));
                    }
                }
            }
        }

        Console.WriteLine($"CSV exported to '{csvPath}'.");
    }
}