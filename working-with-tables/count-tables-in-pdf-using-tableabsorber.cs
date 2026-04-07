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

        try
        {
            // Load the PDF document (using block ensures proper disposal)
            using (Document doc = new Document(inputPath))
            {
                // Create a TableAbsorber to find tables in the document
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from all pages of the document
                absorber.Visit(doc);

                // The TableList collection contains all found tables
                int tableCount = absorber.TableList.Count;

                Console.WriteLine($"Number of tables found: {tableCount}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}