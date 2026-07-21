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

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all form fields in the document
            foreach (Field field in doc.Form.Fields)
            {
                // Identify sensitive fields by name (adjust as needed)
                if (field.FullName != null &&
                    (field.FullName.Equals("SSN", StringComparison.OrdinalIgnoreCase) ||
                     field.FullName.Equals("CreditCard", StringComparison.OrdinalIgnoreCase)))
                {
                    // Set the NoExport flag – Exportable = false prevents the field from being exported
                    field.Exportable = false;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with NoExport flags to '{outputPath}'.");
    }
}