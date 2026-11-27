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
        const string outputTxtPath = "TableRows.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Use TableAbsorber to extract tables from the document
                TableAbsorber absorber = new TableAbsorber();

                // Visit each page individually – TableAbsorber.Visit expects a Page, not a PageCollection
                foreach (Page page in doc.Pages)
                {
                    absorber.Visit(page);
                }

                // Prepare lines to write: one line per table with its row count
                List<string> resultLines = new List<string>();
                int tableIndex = 1;

                foreach (AbsorbedTable table in absorber.TableList)
                {
                    int rowCount = table.RowList.Count;
                    resultLines.Add($"Table {tableIndex}: {rowCount} rows");
                    tableIndex++;
                }

                // Write the collected information to a text file
                File.WriteAllLines(outputTxtPath, resultLines);
            }

            Console.WriteLine($"Row counts per table have been saved to '{outputTxtPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
