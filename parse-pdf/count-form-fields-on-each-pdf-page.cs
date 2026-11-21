using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for WidgetAnnotation
using Aspose.Pdf.Forms; // for form field types

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form (AcroForm)
            var form = doc.Form;
            if (form == null || form.Fields == null || form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                int fieldsOnPage = 0;

                // Count widget annotations (form fields) that belong to the current page
                foreach (Annotation annotation in page.Annotations)
                {
                    if (annotation is WidgetAnnotation)
                        fieldsOnPage++;
                }

                // Log the count for monitoring purposes
                Console.WriteLine($"Page {pageIndex}: {fieldsOnPage} field(s) extracted.");
            }
        }
    }
}
