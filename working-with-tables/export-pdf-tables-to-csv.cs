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

        // Load the PDF document (lifecycle: using block ensures disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Extract tables from the document
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc); // process all pages

            // Write extracted table data to CSV
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                foreach (var table in absorber.TableList)
                {
                    foreach (var row in table.RowList)
                    {
                        List<string> cellTexts = new List<string>();

                        foreach (var cell in row.CellList)
                        {
                            // Concatenate all text fragments within the cell
                            string cellText = "";
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

                        // Write a single CSV line for the current row
                        writer.WriteLine(string.Join(",", cellTexts));
                    }
                }
            }
        }

        Console.WriteLine($"CSV exported to '{csvPath}'.");
    }
}