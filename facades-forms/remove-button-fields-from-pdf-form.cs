using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (required to access the Form fields collection)
        Document pdfDocument = new Document(inputPath);

        // Prepare the FormEditor for the same document instance
        using (FormEditor editor = new FormEditor(pdfDocument))
        {
            // Collect the names of all button fields (ButtonField) to remove.
            // We cannot modify the collection while iterating, so store names first.
            List<string> buttonFieldNames = new List<string>();
            foreach (Field field in pdfDocument.Form.Fields)
            {
                if (field is ButtonField) // covers push‑button, check‑box, radio‑button etc.
                {
                    // Use PartialName – the identifier used by FormEditor.RemoveField
                    buttonFieldNames.Add(field.PartialName);
                }
            }

            // Remove each button field
            foreach (string name in buttonFieldNames)
            {
                editor.RemoveField(name);
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"All button fields removed. Output saved to '{outputPath}'.");
    }
}
