using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_noexport.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields
            foreach (Field field in doc.Form.Fields)
            {
                // Example condition: fields whose full name contains "Sensitive"
                // Adjust the condition as needed for your specific fields
                if (field.FullName != null && field.FullName.IndexOf("Sensitive", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Set the Exportable flag to false (NoExport) to exclude the field from exported data
                    field.Exportable = false;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with NoExport flags applied: {outputPath}");
    }
}