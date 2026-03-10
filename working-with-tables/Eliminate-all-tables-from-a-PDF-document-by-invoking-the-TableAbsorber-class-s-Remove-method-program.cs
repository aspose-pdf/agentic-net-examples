using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_tables.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(inputPath))
            {
                // Create a TableAbsorber to locate tables
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from the entire document
                absorber.Visit(doc);

                // Copy the TableList because Remove modifies the collection
                List<AbsorbedTable> tables = new List<AbsorbedTable>(absorber.TableList);

                // Remove each identified table from its page
                foreach (AbsorbedTable table in tables)
                {
                    absorber.Remove(table);
                }

                // Save the modified PDF (PDF format, no extra SaveOptions needed)
                doc.Save(outputPath);
            }

            Console.WriteLine($"All tables removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}