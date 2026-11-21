using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber and absorb tables from the whole document
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc); // <-- correct way to run the absorber

            Console.WriteLine($"Tables found: {absorber.TableList.Count}");

            int tableNumber = 1;
            // Iterate over each absorbed table
            foreach (AbsorbedTable table in absorber.TableList)
            {
                Console.WriteLine($"Table {tableNumber}:");
                int rowNumber = 1;
                // Iterate over rows in the current table
                foreach (AbsorbedRow row in table.RowList)
                {
                    int colNumber = 1;
                    // Iterate over cells in the current row
                    foreach (AbsorbedCell cell in row.CellList)
                    {
                        // Concatenate all text fragments inside the cell
                        string cellText = string.Empty;
                        foreach (TextFragment fragment in cell.TextFragments)
                        {
                            cellText += fragment.Text;
                        }

                        Console.WriteLine($"  Row {rowNumber}, Column {colNumber}: {cellText}");
                        colNumber++;
                    }
                    rowNumber++;
                }
                tableNumber++;
            }
        }
    }
}
