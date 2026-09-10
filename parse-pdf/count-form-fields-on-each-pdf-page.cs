using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

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

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form (AcroForm)
            Form pdfForm = doc.Form;
            if (pdfForm == null || pdfForm.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Iterate through each page (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Get the current page object
                Page page = doc.Pages[pageIndex];

                // Count form fields on this page. Form fields are represented as WidgetAnnotation objects.
                int fieldsOnPage = page.Annotations.Count(a => a is WidgetAnnotation);

                // Log the number of fields extracted from the current page
                Console.WriteLine($"Page {pageIndex}: {fieldsOnPage} form field(s) extracted.");
            }
        }
    }
}
