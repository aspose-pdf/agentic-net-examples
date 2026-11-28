using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string reportPath = "report.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Form implements IDisposable, so wrap it in a using block
            using (Form form = new Form())
            {
                // Bind the PDF document to the Form facade
                form.BindPdf(pdfPath);

                // Retrieve the collection of field names from the PDF form
                var fieldNames = form.FieldNames; // IEnumerable<string>

                // Write a simple summary of field names to a text file
                using (StreamWriter writer = new StreamWriter(reportPath, false))
                {
                    writer.WriteLine("Form Fields Summary");
                    writer.WriteLine("====================");
                    foreach (var name in fieldNames)
                    {
                        writer.WriteLine(name);
                    }
                }

                Console.WriteLine($"Report saved to '{reportPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
