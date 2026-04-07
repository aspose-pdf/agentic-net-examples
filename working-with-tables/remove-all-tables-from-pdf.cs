using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (using ensures disposal)
            using (Document doc = new Document(inputPath))
            {
                // Create a TableAbsorber to locate tables in the document
                TableAbsorber absorber = new TableAbsorber();

                // Extract tables from the whole document
                absorber.Visit(doc);

                // Copy the TableList because Remove modifies the collection
                var tables = absorber.TableList.ToList();

                // Remove each absorbed table from its page
                foreach (var table in tables)
                {
                    // TableAbsorber.Remove removes the table from the page it belongs to
                    absorber.Remove(table);
                }

                // Save the modified PDF
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
