using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_moved_fields.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF with the core API to read existing field rectangles
        using (Document doc = new Document(inputPdf))
        // Use FormEditor (Facades) to modify field positions and save the result
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the same PDF file to the FormEditor
            formEditor.BindPdf(inputPdf);

            // Iterate over all form fields using the Fields collection
            foreach (Field field in doc.Form.Fields)
            {
                if (field == null) continue;

                // The field name is stored in PartialName
                string fieldName = field.PartialName;

                // Current rectangle coordinates
                var rect = field.Rect;
                if (rect == null) continue;

                float newLlx = (float)rect.LLX + 10f;
                float newLly = (float)rect.LLY + 15f;
                float newUrx = (float)rect.URX + 10f;
                float newUry = (float)rect.URY + 15f;

                // Move the field to the new position
                formEditor.MoveField(fieldName, newLlx, newLly, newUrx, newUry);
            }

            // Save the updated PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Form fields moved and saved to '{outputPdf}'.");
    }
}
