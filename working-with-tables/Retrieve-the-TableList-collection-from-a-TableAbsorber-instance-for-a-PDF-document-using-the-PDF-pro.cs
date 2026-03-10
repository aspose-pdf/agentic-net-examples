using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Text;          // TableAbsorber and related types

class Program
{
    static void Main()
    {
        // Path to the input PDF file
        const string inputPath = "input.pdf";

        // Verify that the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber instance – it will search for tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document (alternatively, use absorber.Visit(doc.Pages[1]) for a single page)
            absorber.Visit(doc);

            // Retrieve the read‑only collection of found tables
            var tableList = absorber.TableList; // IList<AbsorbedTable>

            // Example: output basic information about each detected table
            for (int i = 0; i < tableList.Count; i++)
            {
                var table = tableList[i];
                Console.WriteLine($"Table {i + 1}:");
                Console.WriteLine($"  Page number   : {table.PageNum}");
                Console.WriteLine($"  Rows count    : {table.RowList.Count}");
                Console.WriteLine($"  Columns (first row) count: {table.RowList[0].CellList.Count}");
            }

            // No need to save the document if only reading tables,
            // but you could modify tables and then call doc.Save(...) if required.
        }
    }
}