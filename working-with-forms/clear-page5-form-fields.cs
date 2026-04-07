using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "cleared_page5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Access the core Form object (contains the collection of fields)
            Aspose.Pdf.Forms.Form form = doc.Form;

            // Collect names of all fields that reside on page 5 (page index is zero‑based)
            List<string> fieldsOnPage5 = new List<string>();
            foreach (Aspose.Pdf.Forms.Field field in form.Fields)
            {
                // PageIndex returns zero‑based page number
                if (field.PageIndex == 4) // page 5 => index 4
                {
                    fieldsOnPage5.Add(field.Name);
                }
            }

            // Delete each identified field from the form
            foreach (string fieldName in fieldsOnPage5)
            {
                form.Delete(fieldName);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 5 cleared and saved to '{outputPath}'.");
    }
}