using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text; // for HorizontalAlignment enum

class SetRightAlignedNumberField
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "right_aligned_number_field.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the field will be placed (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a NumberField (inherits from TextBoxField) on the document
            NumberField numberField = new NumberField(doc, fieldRect)
            {
                Name = "Amount",                         // field name
                AllowedChars = "0123456789.,",           // restrict to numeric characters
                TextHorizontalAlignment = HorizontalAlignment.Right // right‑align the text
            };

            // Add the field to the document's form collection
            doc.Form.Add(numberField);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with right‑aligned number field: {outputPdf}");
    }
}