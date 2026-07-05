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
                // Create a TableAbsorber to locate tables
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(doc);

                // Copy the list because Remove modifies the collection
                var tables = absorber.TableList.ToList();

                // Remove each absorbed table from its page
                foreach (var table in tables)
                {
                    absorber.Remove(table);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Tables removed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}