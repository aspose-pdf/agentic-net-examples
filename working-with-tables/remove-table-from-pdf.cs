using System;
using System.IO;
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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize TableAbsorber to locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from all pages of the document
            absorber.Visit(doc);

            // If at least one table is found, remove the first one
            if (absorber.TableList.Count > 0)
            {
                // Remove the first absorbed table from its page
                absorber.Remove(absorber.TableList[0]);
            }
            else
            {
                Console.WriteLine("No tables found to remove.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table removed and saved to '{outputPath}'.");
    }
}