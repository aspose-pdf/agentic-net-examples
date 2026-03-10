using System;
using System.IO;
using System.Linq; // Added for ToArray extension method
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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document (lifecycle rule: use Visit(Document))
            absorber.Visit(doc);

            // Copy the list of found tables because Remove() modifies the collection
            var tables = absorber.TableList.ToArray(); // ToArray now available via System.Linq

            // Remove each identified table while preserving surrounding content
            foreach (var table in tables)
            {
                absorber.Remove(table);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save(string))
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tables removed. Output saved to '{outputPath}'.");
    }
}
