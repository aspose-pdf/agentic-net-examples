using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber to locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the entire document
            absorber.Visit(doc);

            // Iterate over each detected table
            for (int i = 0; i < absorber.TableList.Count; i++)
            {
                // Rectangle describing the table's position on its page
                Aspose.Pdf.Rectangle rect = absorber.TableList[i].Rectangle;

                // Output table index, page number, and rectangle coordinates
                Console.WriteLine($"Table {i + 1} (Page {absorber.TableList[i].PageNum}):");
                Console.WriteLine($"  LowerLeftX  = {rect.LLX}");
                Console.WriteLine($"  LowerLeftY  = {rect.LLY}");
                Console.WriteLine($"  UpperRightX = {rect.URX}");
                Console.WriteLine($"  UpperRightY = {rect.URY}");
            }
        }
    }
}