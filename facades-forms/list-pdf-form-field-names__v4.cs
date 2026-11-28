using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // PDF containing form fields
        const string outputTxtPath = "field_names.txt";    // Text file to write field names

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Form implements IDisposable – wrap in using for deterministic disposal
            using (Form form = new Form())
            {
                // Load the PDF document into the Form facade
                form.BindPdf(inputPdfPath);

                // Retrieve all form field names via the FieldNames property (GetFields does not exist)
                string[] fieldNames = form.FieldNames;

                // Write each field name to the output text file
                using (StreamWriter writer = new StreamWriter(outputTxtPath, false))
                {
                    foreach (string name in fieldNames)
                    {
                        writer.WriteLine(name);
                    }
                }
            }

            Console.WriteLine($"Field names written to '{outputTxtPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
