using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";      // PDF containing a form field
        const string outputPdf = "output.pdf";    // Resulting PDF with dashed border
        const string fieldName = "OptionalField"; // Name of the form field to modify

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Retrieve the form field and cast it to Aspose.Pdf.Forms.Field
            Field? formField = pdfDoc.Form[fieldName] as Field;
            if (formField == null)
            {
                Console.Error.WriteLine($"Form field \"{fieldName}\" not found or is not a form field.");
                return;
            }

            // Set a dashed border on the field (form fields are also annotations)
            formField.Border = new Border(formField)
            {
                Style = BorderStyle.Dashed, // Dashed border style
                Width = 1                    // Border width in points (integer)
            };

            // Save the modified PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Form field \"{fieldName}\" border set to dashed. Saved to '{outputPdf}'.");
    }
}