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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to find tables
            TableAbsorber absorber = new TableAbsorber();

            // Absorb tables from each page (pages are 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                absorber.Visit(doc.Pages[i]);
            }

            // Remove the first table found, if any
            if (absorber.TableList.Count > 0)
            {
                // TableAbsorber.Remove expects an AbsorbedTable instance
                absorber.Remove(absorber.TableList[0]);
                Console.WriteLine("A table was removed.");
            }
            else
            {
                Console.WriteLine("No tables found to remove.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
