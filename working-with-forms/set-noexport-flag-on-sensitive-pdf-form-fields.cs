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
                // Identify sensitive fields (example: SSN or CreditCard)
                if (field.FullName != null &&
                    (field.FullName.Equals("SSN", StringComparison.OrdinalIgnoreCase) ||
                     field.FullName.Equals("CreditCard", StringComparison.OrdinalIgnoreCase)))
                {
                    // Set the Exportable flag to false – this corresponds to the NoExport flag
                    field.Exportable = false;
                }
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with NoExport flags to '{outputPath}'.");
    }
}