using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int targetPage = 5; // Aspose.Pdf uses 1‑based page numbers

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            if (doc.Form == null || doc.Form.Fields == null)
            {
                Console.WriteLine("No form fields found in the document.");
                doc.Save(outputPath);
                return;
            }

            // Collect the names of fields that belong to the target page
            List<string> fieldNamesToRemove = new List<string>();

            // Iterate over pages and look for WidgetAnnotation objects (form fields)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                if (pageIndex != targetPage)
                    continue;

                Page page = doc.Pages[pageIndex];
                foreach (Annotation annotation in page.Annotations)
                {
                    if (annotation is WidgetAnnotation widget)
                    {
                        // The widget's Name corresponds to the field's name in the Form collection
                        fieldNamesToRemove.Add(widget.Name);
                    }
                }
            }

            // Delete the collected fields from the document's form
            foreach (string fieldName in fieldNamesToRemove)
            {
                doc.Form.Delete(fieldName);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All form fields on page {targetPage} have been removed. Saved to '{outputPath}'.");
    }
}
