using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

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
                if (field.FullName != null && field.FullName.IndexOf("Sensitive", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Set the Exportable flag to false (equivalent to NoExport)
                    field.Exportable = false;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}