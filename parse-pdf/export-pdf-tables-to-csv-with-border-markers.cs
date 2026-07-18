using System;
using System.IO;
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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Prepare a TableAbsorber to extract tables from all pages
            TableAbsorber absorber = new TableAbsorber();

            // Visit each page to collect tables
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                absorber.Visit(doc.Pages[pageNum]);
            }

            // Write extracted tables to CSV with visual border delimiters
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                // Iterate over each extracted table
                foreach (var table in absorber.TableList)
                {
                    // Iterate rows
                    foreach (var row in table.RowList)
                    {
                        // Build a CSV line where each cell is wrapped with a vertical bar '|'
                        string line = string.Empty;
                        foreach (var cell in row.CellList)
                        {
                            // Add left border marker
                            line += "|";

                            // Concatenate all text fragments inside the cell
                            string cellText = string.Empty;
                            foreach (TextFragment fragment in cell.TextFragments)
                            {
                                cellText += fragment.Text;
                            }

                            // Escape double quotes inside cell text
                            cellText = cellText.Replace("\"", "\"\"");

                            // Enclose text in quotes to preserve commas
                            line += $"\"{cellText}\"";
                        }

                        // Append rightmost border marker and write the line
                        line += "|";
                        writer.WriteLine(line);
                    }

                    // After each table, add a horizontal separator line to indicate table borders
                    writer.WriteLine(new string('-', 80));
                }
            }
        }

        Console.WriteLine($"CSV file with border markers saved to '{csvPath}'.");
    }
}
