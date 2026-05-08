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
            // Create a TableAbsorber to locate tables
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the first page (pages are 1‑based)
            absorber.Visit(doc.Pages[1]);

            // If at least one table was found, remove the first one
            if (absorber.TableList.Count > 0)
            {
                AbsorbedTable tableToRemove = absorber.TableList[0];
                absorber.Remove(tableToRemove);
                Console.WriteLine("Removed one table from the document.");
            }
            else
            {
                Console.WriteLine("No tables found on the first page.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}