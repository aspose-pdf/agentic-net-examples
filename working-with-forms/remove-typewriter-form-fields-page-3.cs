using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Use TextFragmentAbsorber on page 3 (as required)
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            // Page indexing in Aspose.Pdf is 1‑based
            doc.Pages[3].Accept(absorber);

            // Collect names of Typewriter (TextBoxField) form fields on page 3
            List<string> fieldsToRemove = new List<string>();

            foreach (Field field in doc.Form.Fields)
            {
                // Check field type
                if (field is TextBoxField)
                {
                    // PageIndex is 1‑based; compare with page 3
                    if (field.PageIndex == 3)
                    {
                        fieldsToRemove.Add(field.PartialName);
                    }
                }
            }

            // Remove the identified fields
            foreach (string fieldName in fieldsToRemove)
            {
                doc.Form.Delete(fieldName);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form fields removed and saved to '{outputPath}'.");
    }
}