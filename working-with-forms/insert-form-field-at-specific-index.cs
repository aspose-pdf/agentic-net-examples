using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a new text box field on page 1
            // Rectangle constructor: (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);
            TextBoxField newField = new TextBoxField(doc.Pages[1], rect)
            {
                PartialName = "InsertedField",
                Value       = "Default Text"
            };

            // Desired zero‑based index within the form's field collection
            int insertIndex = 2; // adjust as needed

            // The Form.Fields property returns a collection that can be cast to IList<Field>
            IList<Field> fieldList = doc.Form.Fields as IList<Field>;

            if (fieldList != null && insertIndex >= 0 && insertIndex <= fieldList.Count)
            {
                // Insert the new field at the specified position
                fieldList.Insert(insertIndex, newField);
            }
            else
            {
                // Fallback: add the field at the end if insertion is not possible
                doc.Form.Add(newField);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with inserted field: {outputPdf}");
    }
}