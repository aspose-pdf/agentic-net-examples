using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        try
        {
            // Form (not FormEditor) provides access to existing form data.
            using (Form form = new Form())
            {
                form.BindPdf(inputPath);

                // Retrieve all form field names via the FieldNames property.
                string[] fieldNames = form.FieldNames;

                Console.WriteLine($"Form fields in '{Path.GetFileName(inputPath)}':");
                foreach (string name in fieldNames)
                {
                    Console.WriteLine($"- {name}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
