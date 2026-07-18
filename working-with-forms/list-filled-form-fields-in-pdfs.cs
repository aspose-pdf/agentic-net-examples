using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing the PDFs to process
        const string inputFolder = "pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Iterate over each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            Console.WriteLine($"Processing: {Path.GetFileName(pdfPath)}");

            // Load the PDF document (using the lifecycle rule: wrap in using)
            using (Document doc = new Document(pdfPath))
            {
                // Ensure the document actually contains a form with fields
                if (doc.Form == null ||
                    doc.Form.Fields == null ||
                    !doc.Form.Fields.Any())
                {
                    Console.WriteLine("  No form fields found.");
                    continue;
                }

                // List each filled field and its value
                foreach (Field field in doc.Form.Fields)
                {
                    // Most field types expose the current value via the Value property.
                    // For fields that have a specific value property (e.g., CheckBoxField), the
                    // generic Value property still returns the appropriate representation.
                    string value = field.Value != null ? field.Value.ToString() : "<empty>";
                    Console.WriteLine($"  Field: {field.PartialName}, Value: {value}");
                }
            }
        }
    }
}