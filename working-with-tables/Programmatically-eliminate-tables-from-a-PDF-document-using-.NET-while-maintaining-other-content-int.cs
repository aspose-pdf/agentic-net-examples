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
        const string outputPath = "output_no_tables.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Find all tables in the document
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc);

            // Remove each detected table
            foreach (var table in absorber.TableList.ToList())
            {
                absorber.Remove(table);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tables removed. Output saved to '{outputPath}'.");
    }
}