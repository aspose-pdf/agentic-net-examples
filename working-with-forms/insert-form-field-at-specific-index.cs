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
        const string outputPath = "output.pdf";
        const int insertIndex = 2; // zero‑based position in the Fields collection

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // The Form object is obtained from the document; it is never null for a valid PDF.
                Form form = doc.Form;

                // Create a new text box form field on the first page
                TextBoxField txtField = new TextBoxField(
                    doc.Pages[1],
                    new Aspose.Pdf.Rectangle(100, 600, 300, 620))
                {
                    PartialName = "NewField",
                    Value = "Default"
                };

                // Insert the new field at the desired index using the mutable collection returned by Form.Fields
                // Form.Fields implements IList<Field>, so we can use Insert.
                IList<Field> fields = form.Fields;
                int index = Math.Max(0, Math.Min(insertIndex, fields.Count));
                fields.Insert(index, txtField);

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Form field inserted and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
