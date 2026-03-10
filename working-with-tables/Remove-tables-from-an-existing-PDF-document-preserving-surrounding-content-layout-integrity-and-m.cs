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
            // Create a TableAbsorber to locate tables
            TableAbsorber absorber = new TableAbsorber();

            // Scan the whole document for tables
            absorber.Visit(doc);

            // Remove each found table.
            // Copy the list first because Remove() modifies the collection.
            var tables = absorber.TableList.ToList();
            foreach (var table in tables)
            {
                absorber.Remove(table);
            }

            // Preserve metadata automatically (doc.Info, XMP, etc.) and save.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tables removed. Output saved to '{outputPath}'.");
    }
}