using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "sorted_desc.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Form implements IDisposable, so wrap it in a using block for deterministic disposal
        using (Form form = new Form())
        {
            // Load the PDF document into the Form object
            form.BindPdf(pdfPath);

            // Retrieve all field names from the PDF form using the FieldNames property
            string[] fieldNames = form.FieldNames;

            // Sort the field names in descending order
            string[] sortedDesc = fieldNames.OrderByDescending(name => name).ToArray();

            // Write the sorted list to a text file
            File.WriteAllLines(outputPath, sortedDesc);
        }

        Console.WriteLine($"Sorted field names saved to '{outputPath}'.");
    }
}
