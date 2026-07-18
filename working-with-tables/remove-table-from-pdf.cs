using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // TableAbsorber, AbsorbedTable

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from all pages (Visit overload that accepts Document)
            absorber.Visit(doc);

            // Ensure at least one table was found
            if (absorber.TableList.Count > 0)
            {
                // Get the first absorbed table
                AbsorbedTable tableToRemove = absorber.TableList[0];

                // Remove the table from its page
                absorber.Remove(tableToRemove);

                Console.WriteLine("Table removed successfully.");
            }
            else
            {
                Console.WriteLine("No tables found in the document.");
            }

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}