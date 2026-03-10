using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document; the using block ensures proper disposal (document‑disposal‑with‑using rule)
        using (Document doc = new Document(inputPath))
        {
            // Check that the document contains at least one page
            if (doc.Pages.Count == 0)
            {
                Console.WriteLine("The document has no pages.");
                return;
            }

            // Retrieve the operator collection of the first page (pages are 1‑based)
            OperatorCollection contents = doc.Pages[1].Contents;

            // Iterate over each operator and display its type name
            foreach (Operator op in contents)
            {
                Console.WriteLine(op.GetType().Name);
            }
        }
    }
}