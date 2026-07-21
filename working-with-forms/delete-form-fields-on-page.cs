using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_cleared_page5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 5 pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("Document does not contain page 5.");
                return;
            }

            // Get page 5
            Page page5 = doc.Pages[5];

            // Retrieve all form fields that intersect the page rectangle
            // GetFieldsInRect returns a collection of Field objects located on the page
            var fieldsOnPage = doc.Form.GetFieldsInRect(page5.Rect);

            // Delete each field found on page 5
            foreach (Field field in fieldsOnPage)
            {
                doc.Form.Delete(field);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 5 cleared of form fields. Saved to '{outputPath}'.");
    }
}