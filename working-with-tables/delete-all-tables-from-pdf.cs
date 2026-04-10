using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_tables.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: create, load, save)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Copy the list because Remove modifies the original collection
            List<AbsorbedTable> tables = new List<AbsorbedTable>(absorber.TableList);

            // Remove each identified table from its page
            foreach (AbsorbedTable table in tables)
            {
                absorber.Remove(table);
            }

            // Save the modified PDF (no PreSave needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All tables removed. Saved to '{outputPath}'.");
    }
}