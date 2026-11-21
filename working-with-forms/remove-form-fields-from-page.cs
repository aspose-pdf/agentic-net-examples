using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleared_page5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document actually contains a form
            if (doc.Form == null || doc.Form.Fields == null)
            {
                Console.WriteLine("No form fields found in the document.");
                doc.Save(outputPath);
                return;
            }

            // Collect all form fields that reside on page 5.
            // Aspose.Pdf uses a zero‑based PageIndex for fields (0 = first page).
            List<Field> fieldsOnPage5 = new List<Field>();
            foreach (Field field in doc.Form.Fields)
            {
                if (field.PageIndex == 4) // page 5 (zero‑based index)
                    fieldsOnPage5.Add(field);
            }

            // Delete the collected fields from the form collection
            foreach (Field field in fieldsOnPage5)
            {
                doc.Form.Delete(field);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 5 cleared. Saved to '{outputPath}'.");
    }
}
