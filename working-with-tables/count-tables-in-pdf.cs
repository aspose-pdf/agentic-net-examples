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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // TableAbsorber extracts table structures from the document
            TableAbsorber absorber = new TableAbsorber();

            // Apply the absorber to the whole document (or to specific pages via absorber.Visit(doc.Pages[i]))
            absorber.Visit(doc);

            // The TableList collection holds each detected table
            int tableCount = absorber.TableList.Count;

            Console.WriteLine($"Number of tables in '{Path.GetFileName(inputPath)}': {tableCount}");
        }
    }
}