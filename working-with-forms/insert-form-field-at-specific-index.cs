using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int insertIndex = 2; // zero‑based index where the new field will be placed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap the Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a new text box form field on the first page
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);
            TextBoxField newField = new TextBoxField(doc.Pages[1], rect)
            {
                PartialName = "NewField",
                Value = "Default text"
            };

            // -----------------------------------------------------------------
            // Insert the new field at a specific index within the form's field list.
            // Aspose.Pdf's Form.Fields property is read‑only, so we cannot assign a
            // reordered array back. Instead we:
            //   1. Capture the current fields.
            //   2. Remove all existing fields from the form.
            //   3. Re‑add them in the desired order, inserting the new field at
            //      the requested position.
            // -----------------------------------------------------------------
            List<Field> existingFields = doc.Form?.Fields?.ToList() ?? new List<Field>();

            // Remove all current fields from the document form.
            foreach (var f in existingFields)
            {
                // The Delete method expects the field's name.
                // Use Name if available, otherwise FullName.
                string fieldName = f.Name ?? f.FullName;
                if (!string.IsNullOrEmpty(fieldName))
                {
                    doc.Form.Delete(fieldName);
                }
            }

            // Re‑add fields in the new order.
            int currentIndex = 0;
            foreach (var f in existingFields)
            {
                if (currentIndex == insertIndex)
                {
                    // Insert the new field before the field that would occupy this index.
                    doc.Form.Add(newField);
                }
                doc.Form.Add(f);
                currentIndex++;
            }

            // If the insert index is greater than the original count, append the new field.
            if (insertIndex >= existingFields.Count)
            {
                doc.Form.Add(newField);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field inserted and saved to '{outputPath}'.");
    }
}
