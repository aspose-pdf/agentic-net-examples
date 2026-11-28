using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "sorted.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Bind the PDF to the Form facade
        using (Form form = new Form())
        {
            form.BindPdf(inputPdfPath);

            // Retrieve all field names via the FieldNames property (no GetFieldNames method)
            string[] fieldNames = form.FieldNames;

            // Sort alphabetically (case‑insensitive)
            var sorted = fieldNames
                         .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                         .ToArray();

            // Write the sorted list to a text file
            File.WriteAllLines(outputTxtPath, sorted);
        }

        Console.WriteLine($"Alphabetically sorted field names written to '{outputTxtPath}'.");
    }
}
