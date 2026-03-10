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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Instantiate a TableAbsorber to locate tables
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // Iterate over the discovered tables and output basic information
            for (int i = 0; i < absorber.TableList.Count; i++)
            {
                var table = absorber.TableList[i];
                Console.WriteLine($"Table {i + 1} on page {table.PageNum}, rows: {table.RowList.Count}");

                // Example: display text from the first cell's first fragment
                if (table.RowList.Count > 0 &&
                    table.RowList[0].CellList.Count > 0 &&
                    table.RowList[0].CellList[0].TextFragments.Count > 0)
                {
                    TextFragment tf = table.RowList[0].CellList[0].TextFragments[0];
                    Console.WriteLine($"First cell text: {tf.Text}");
                }
            }

            // Optional: modify a text fragment in the first table and save changes
            if (absorber.TableList.Count > 0)
            {
                var firstTable = absorber.TableList[0];
                if (firstTable.RowList.Count > 0 &&
                    firstTable.RowList[0].CellList.Count > 0 &&
                    firstTable.RowList[0].CellList[0].TextFragments.Count > 1)
                {
                    TextFragment fragment = firstTable.RowList[0].CellList[0].TextFragments[1];
                    fragment.Text = "hi world";
                }
            }

            // Save the (potentially modified) document
            doc.Save("output.pdf");
        }
    }
}