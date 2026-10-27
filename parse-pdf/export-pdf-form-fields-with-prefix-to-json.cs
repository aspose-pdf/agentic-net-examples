using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPath = "input.pdf";
        // Output text file with filtered field names and values
        const string outputPath = "filtered_fields.txt";
        // Prefix to filter field names
        const string prefix = "Customer_";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document actually contains a form
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Filter fields whose names start with the specified prefix
            var filteredFields = doc.Form.Fields
                                   .Where(field => !string.IsNullOrEmpty(field.FullName) &&
                                                   field.FullName.StartsWith(prefix, StringComparison.Ordinal));

            // Export the filtered fields to a simple CSV‑like text file
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                foreach (var field in filteredFields)
                {
                    // Some fields may have null values; handle gracefully
                    string value = field.Value?.ToString() ?? string.Empty;
                    writer.WriteLine($"{field.FullName},{value}");
                }
            }

            Console.WriteLine($"Filtered fields saved to '{outputPath}'.");
        }
    }
}
