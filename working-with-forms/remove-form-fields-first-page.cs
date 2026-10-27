using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for WidgetAnnotation

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "template_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Guard against documents without a form
            if (doc.Form == null || doc.Form.Fields == null)
            {
                Console.WriteLine("No form fields present in the document.");
                doc.Save(outputPath);
                return;
            }

            // Collect the names of all form fields that reside on the first page
            List<string> fieldsOnFirstPage = new List<string>();
            Page firstPage = doc.Pages[1]; // Aspose.Pdf pages are 1‑based
            foreach (Annotation annotation in firstPage.Annotations)
            {
                if (annotation is WidgetAnnotation widget)
                {
                    // The widget's Name corresponds to the field's name in the AcroForm
                    fieldsOnFirstPage.Add(widget.Name);
                }
            }

            // Delete each collected field from the document
            foreach (string fieldName in fieldsOnFirstPage)
            {
                doc.Form.Delete(fieldName);
            }

            // Save the cleaned PDF (first page now has no form fields)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Template page saved to '{outputPath}'.");
    }
}
