using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // needed for WidgetAnnotation

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "clean_template_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf pages are 1‑based)
            Page firstPage = doc.Pages[1];

            // Collect all widget annotations (form fields) that belong to the first page.
            // These annotations are also the Field objects stored in doc.Form.
            List<Field> fieldsOnFirstPage = new List<Field>();
            foreach (Annotation ann in firstPage.Annotations)
            {
                if (ann is WidgetAnnotation widget && widget is Field field)
                {
                    fieldsOnFirstPage.Add(field);
                }
            }

            // Delete each collected field from the form
            foreach (Field field in fieldsOnFirstPage)
            {
                doc.Form.Delete(field);
            }

            // Save the modified document; the first page now has no form fields
            doc.Save(outputPath);
        }

        Console.WriteLine($"Clean template page saved to '{outputPath}'.");
    }
}
