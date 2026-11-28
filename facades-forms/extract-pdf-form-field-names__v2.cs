using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "form_fields.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF into the Form facade
            using (Form form = new Form())
            {
                form.BindPdf(inputPdf);

                // Retrieve all form field names via the FieldNames property
                string[] fieldNames = form.FieldNames ?? Array.Empty<string>();

                // Write the field names to a text file (one per line)
                File.WriteAllLines(outputTxt, fieldNames);
            }

            Console.WriteLine($"Form field names saved to '{outputTxt}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
