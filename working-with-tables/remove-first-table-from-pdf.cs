using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // If at least one table was found, remove the first one
            if (absorber.TableList.Count > 0)
            {
                // The first absorbed table on the page
                AbsorbedTable table = absorber.TableList[0];

                // Remove the table from its page
                absorber.Remove(table);
            }
            else
            {
                Console.WriteLine("No tables found to remove.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table removed, saved to '{outputPath}'.");
    }
}