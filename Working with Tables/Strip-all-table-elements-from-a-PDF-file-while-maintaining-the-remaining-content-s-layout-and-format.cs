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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate all tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Visit the whole document – this populates absorber.TableList
            absorber.Visit(doc);

            // Remove each found table. Use a copy of the list because Remove()
            // modifies the original collection.
            var tables = absorber.TableList.ToList();
            foreach (var table in tables)
            {
                absorber.Remove(table);
            }

            // Save the modified document. No extra SaveOptions are needed for PDF output.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tables removed. Output saved to '{outputPath}'.");
    }
}