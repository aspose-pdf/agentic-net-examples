using System;
using System.IO;
using System.Linq;                     // For ToList()
using Aspose.Pdf;                     // Document, Page
using Aspose.Pdf.Text;                // TableAbsorber, AbsorbedTable

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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document
            absorber.Visit(doc);

            // Copy the TableList because Remove modifies the collection
            var tables = absorber.TableList.ToList();

            // Remove each absorbed table from its page
            foreach (AbsorbedTable table in tables)
            {
                absorber.Remove(table);
            }

            // Save the modified PDF (lifecycle rule: save after modifications)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tables removed and document saved to '{outputPath}'.");
    }
}