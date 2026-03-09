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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Instantiate TableAbsorber to search for tables
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // Example: iterate over found tables and display basic information
            for (int i = 0; i < absorber.TableList.Count; i++)
            {
                var table = absorber.TableList[i];
                Console.WriteLine($"Table {i + 1}: Page={table.PageNum}, Rows={table.RowList.Count}");
            }
        }
    }
}