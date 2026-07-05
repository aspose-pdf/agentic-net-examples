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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form.Fields)
            {
                // Identify sensitive fields (example: name contains "Sensitive")
                if (field.FullName != null &&
                    field.FullName.IndexOf("Sensitive", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Set the Exportable flag to false (NoExport) to exclude from exported data
                    field.Exportable = false;
                }
            }

            // Save the updated PDF (lifecycle rule: save within using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with NoExport flags applied: {outputPath}");
    }
}