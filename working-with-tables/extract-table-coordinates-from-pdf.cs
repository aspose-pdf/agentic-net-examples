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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Iterate over each found table
            for (int i = 0; i < absorber.TableList.Count; i++)
            {
                AbsorbedTable table = absorber.TableList[i];

                // Get the rectangle that describes the table position
                Aspose.Pdf.Rectangle rect = table.Rectangle;

                // Output table index, page number and rectangle coordinates
                Console.WriteLine(
                    $"Table {i + 1} on page {table.PageNum}: " +
                    $"LLX={rect.LLX}, LLY={rect.LLY}, URX={rect.URX}, URY={rect.URY}");
            }
        }
    }
}