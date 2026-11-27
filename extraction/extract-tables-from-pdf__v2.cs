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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber instance to prepare for table extraction
            TableAbsorber tableAbsorber = new TableAbsorber();

            // Visit each page with the absorber (TableAbsorber does not implement AnnotationSelector)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                tableAbsorber.Visit(doc.Pages[i]);
            }

            // Example: report how many tables were found
            Console.WriteLine($"Tables detected: {tableAbsorber.TableList.Count}");
        }
    }
}
