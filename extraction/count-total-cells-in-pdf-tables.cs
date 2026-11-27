using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "TotalCellCount.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // TableAbsorber extracts tables from the document
            TableAbsorber absorber = new TableAbsorber();

            // Visit the document (or each page) to populate TableList
            absorber.Visit(doc);

            int totalCellCount = 0;

            // Iterate over each extracted table
            foreach (var table in absorber.TableList)
            {
                // Iterate over each row in the table
                foreach (var row in table.RowList)
                {
                    // CellList contains the cells of the current row
                    totalCellCount += row.CellList.Count;
                }
            }

            // Write the total cell count to the text file
            File.WriteAllText(outputTxt, totalCellCount.ToString());

            Console.WriteLine($"Total cells across all tables: {totalCellCount}");
            Console.WriteLine($"Result saved to '{outputTxt}'.");
        }
    }
}
