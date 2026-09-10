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

        // Load the PDF document (using rule for document disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through each page (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Get all form fields on the current page in tab order
                var fields = page.FieldsInTabOrder;

                if (fields != null && fields.Count > 0)
                {
                    Console.WriteLine($"Page {i} contains {fields.Count} form field(s):");
                    foreach (Field field in fields)
                    {
                        // Most field types derive from WidgetAnnotation which has a Name property
                        // Use the field's FullyQualifiedName if available, otherwise fallback to Name
                        string fieldName = field.FullName ?? field.Name ?? "(unnamed)";
                        Console.WriteLine($"  - Field: {fieldName}");
                    }
                }
                else
                {
                    Console.WriteLine($"Page {i} contains no form fields.");
                }
            }

            // No modifications are made, but saving follows the lifecycle rule
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing complete. Document saved to '{outputPath}'.");
    }
}