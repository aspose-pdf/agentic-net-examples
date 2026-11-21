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
            // Extract tables from the document
            TableAbsorber absorber = new TableAbsorber();

            // Visit each page individually – TableAbsorber.Visit expects a Page, not a PageCollection
            foreach (Page page in doc.Pages)
            {
                absorber.Visit(page);
            }

            // Remove the first detected table, if any
            if (absorber.TableList.Count > 0)
            {
                // Remove expects an AbsorbedTable instance
                absorber.Remove(absorber.TableList[0]);
                Console.WriteLine("A table was removed from the document.");
            }
            else
            {
                Console.WriteLine("No tables found to remove.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
