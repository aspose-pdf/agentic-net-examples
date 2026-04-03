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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the first page (adjust page index as needed)
            absorber.Visit(doc.Pages[1]);

            // If at least one table is found, remove the first one
            if (absorber.TableList.Count > 0)
            {
                absorber.Remove(absorber.TableList[0]);
            }
            else
            {
                Console.WriteLine("No tables found on the first page.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table removed and saved to '{outputPath}'.");
    }
}