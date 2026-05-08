using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_cleared_page5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object of the document
            Form pdfForm = doc.Form;

            // Collect fields that belong to page 5 (pages are 1‑based)
            List<Field> fieldsOnPage5 = new List<Field>();
            foreach (Field field in pdfForm.Fields)
            {
                int pageNumber = GetFieldPageNumber(field, doc);
                if (pageNumber == 5)
                    fieldsOnPage5.Add(field);
            }

            // Delete each collected field from the form
            foreach (Field field in fieldsOnPage5)
            {
                pdfForm.Delete(field);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 5 fields removed. Saved to '{outputPath}'.");
    }

    /// <summary>
    /// Determines the page number a form field belongs to by checking the field's rectangle
    /// against each page's dimensions. Returns -1 if the page cannot be determined.
    /// </summary>
    private static int GetFieldPageNumber(Field field, Document doc)
    {
        if (field == null || field.Rect == null)
            return -1;

        // The rectangle coordinates are defined in the page's coordinate system.
        // Iterate through pages and find the one whose size contains the rectangle.
        foreach (Page page in doc.Pages)
        {
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Simple containment check – assumes the field does not span multiple pages.
            if (field.Rect.LLX >= 0 && field.Rect.URX <= pageWidth &&
                field.Rect.LLY >= 0 && field.Rect.URY <= pageHeight)
            {
                return page.Number; // Page numbers are 1‑based.
            }
        }
        return -1; // Not found.
    }
}