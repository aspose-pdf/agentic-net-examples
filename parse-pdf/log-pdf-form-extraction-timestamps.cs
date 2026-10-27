using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Capture start timestamp
        DateTime startTime = DateTime.UtcNow;
        Console.WriteLine($"Form extraction started at: {startTime:O}");

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document contains a form and has fields
            if (doc.Form == null || doc.Form.Fields == null || !doc.Form.Fields.Any())
            {
                Console.WriteLine("No form fields found in the document.");
            }
            else
            {
                // Iterate over all form fields and output their names and values
                foreach (var field in doc.Form.Fields)
                {
                    // Field name
                    string name = field.FullName;

                    // Retrieve the field value as a string (handles different field types)
                    string value = field.Value?.ToString() ?? string.Empty;

                    Console.WriteLine($"Field: {name}, Value: {value}");
                }
            }
        }

        // Capture end timestamp
        DateTime endTime = DateTime.UtcNow;
        Console.WriteLine($"Form extraction ended at: {endTime:O}");

        // Optionally, display the duration
        TimeSpan duration = endTime - startTime;
        Console.WriteLine($"Total extraction time: {duration.TotalSeconds:F2} seconds");
    }
}
