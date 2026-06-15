using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form.Fields)
            {
                // Set the Exportable flag to false to mark the field as NoExport
                // This prevents the field value from being included in exported data (e.g., FDF, JSON)
                field.Exportable = false;

                // Optionally, also set the NoView flag to hide the field from the UI
                // field.Flags |= AnnotationFlags.NoView;
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with NoExport flags applied: {outputPath}");
    }
}