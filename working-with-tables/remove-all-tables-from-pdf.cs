using System;
using System.Collections.Generic;
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
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from all pages
            absorber.Visit(doc);

            // Copy the TableList because Remove modifies the collection
            List<AbsorbedTable> tables = new List<AbsorbedTable>(absorber.TableList);

            // Remove each absorbed table from its page
            foreach (AbsorbedTable table in tables)
            {
                absorber.Remove(table);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tables removed and document saved to '{outputPath}'.");
    }
}