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
        const string outputPath = "template_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Collect fields that reside on the first page (page index 0)
            List<Field> fieldsToRemove = new List<Field>();
            foreach (Field field in doc.Form.Fields)
            {
                if (field.PageIndex == 0) // first page (zero‑based index)
                {
                    fieldsToRemove.Add(field);
                }
            }

            // Remove the collected fields from the form
            foreach (Field field in fieldsToRemove)
            {
                doc.Form.Delete(field);
            }

            // Save the cleaned document
            doc.Save(outputPath);
        }

        Console.WriteLine($"First‑page form fields removed. Saved as '{outputPath}'.");
    }
}
