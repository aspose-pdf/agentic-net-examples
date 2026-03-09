using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_tables.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Create a TableAbsorber to find tables
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from the entire document
                absorber.Visit(doc);

                // Copy the TableList to avoid modifying the collection while iterating
                var tables = new System.Collections.Generic.List<AbsorbedTable>(absorber.TableList);

                // Remove each found table from the page
                foreach (var table in tables)
                {
                    absorber.Remove(table);
                }

                // Save the modified document
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