using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_without_tables.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to detect tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Copy the list because Remove() modifies the original collection
            var tables = absorber.TableList.Cast<AbsorbedTable>().ToList();

            // Remove each detected table
            foreach (AbsorbedTable table in tables)
            {
                absorber.Remove(table);
            }

            // Save the modified PDF (still PDF format, no special SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tables removed. Output saved to '{outputPath}'.");
    }
}