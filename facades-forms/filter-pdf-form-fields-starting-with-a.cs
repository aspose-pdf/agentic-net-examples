using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputTxt = "filtered.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Bind the PDF to a Form object (Facades API)
        using (var pdfForm = new Form())
        {
            pdfForm.BindPdf(pdfPath);

            // Retrieve all field names via the FieldNames property
            string[] allFields = pdfForm.FieldNames;

            // Filter names that start with 'A' (case‑sensitive)
            var filtered = allFields
                .Where(name => !string.IsNullOrEmpty(name) && name.StartsWith("A"))
                .ToArray();

            // Write the filtered field names to a text file, one per line
            File.WriteAllLines(outputTxt, filtered);
        }

        Console.WriteLine($"Filtered field names saved to '{outputTxt}'.");
    }
}
