using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "clean_template.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page firstPage = doc.Pages[1];

            // Build a rectangle that represents the whole page area
            var pageRect = new Rectangle(0, 0, firstPage.PageInfo.Width, firstPage.PageInfo.Height);

            // Retrieve all form fields that intersect the page rectangle
            var fieldsOnFirstPage = doc.Form.GetFieldsInRect(pageRect);

            // Delete each field found on the first page
            foreach (var field in fieldsOnFirstPage)
            {
                doc.Form.Delete(field);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Clean template saved to '{outputPath}'.");
    }
}
