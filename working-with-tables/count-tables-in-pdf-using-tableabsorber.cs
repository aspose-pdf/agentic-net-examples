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
            // Open the PDF document inside a using block for proper disposal
            using (Document doc = new Document(inputPath))
            {
                // Create a TableAbsorber to search for tables
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from the entire document
                absorber.Visit(doc);

                // The TableList collection holds all found tables
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