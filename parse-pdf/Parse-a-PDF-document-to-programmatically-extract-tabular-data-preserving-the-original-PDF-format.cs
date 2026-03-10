using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TableAbsorber, AbsorbedTable, TextFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPdfPath = "output_copy.pdf";
        const string csvPath = "extracted_tables.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Extract tables from the entire document
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc); // populates absorber.TableList

            // Write extracted tables to a CSV file
            using (StreamWriter writer = new StreamWriter(csvPath, false))
            {
                int tableNumber = 1;
                foreach (AbsorbedTable table in absorber.TableList)
                {
                    writer.WriteLine($"Table {tableNumber}");
                    foreach (var row in table.RowList)
                    {
                        List<string> cellValues = new List<string>();
                        foreach (var cell in row.CellList)
                        {
                            // Concatenate all text fragments within the cell
                            string cellText = string.Empty;
                            foreach (TextFragment fragment in cell.TextFragments)
                            {
                                cellText += fragment.Text;
                            }
                            // Escape double quotes for CSV compliance
                            cellText = cellText.Replace("\"", "\"\"");
                            cellValues.Add($"\"{cellText}\"");
                        }
                        writer.WriteLine(string.Join(",", cellValues));
                    }
                    writer.WriteLine(); // blank line between tables
                    tableNumber++;
                }
            }

            // Preserve the original PDF format by saving an unchanged copy
            doc.Save(outputPdfPath);
        }

        Console.WriteLine("Table extraction completed. CSV saved to " + csvPath);
    }
}