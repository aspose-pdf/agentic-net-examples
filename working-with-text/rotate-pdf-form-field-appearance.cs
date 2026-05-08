using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class RotateFormFieldExample
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF containing a form field
        const string outputPdf = "rotated_form.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has a form object
            Form form = doc.Form;
            if (form == null)
            {
                Console.Error.WriteLine("The PDF does not contain a form.");
                return;
            }

            // ------------------------------------------------------------
            // 1. Locate the target field (by its full name) that we want to rotate.
            // ------------------------------------------------------------
            // Example: assume the field name is "TextField1". Adjust as needed.
            const string targetFieldName = "TextField1";

            // Retrieve the field object from the form collection
            Field targetField = null;
            foreach (Field f in form.Fields)
            {
                if (f.FullName == targetFieldName)
                {
                    targetField = f;
                    break;
                }
            }

            if (targetField == null)
            {
                Console.Error.WriteLine($"Field '{targetFieldName}' not found in the form.");
                return;
            }

            // ------------------------------------------------------------
            // 2. Create a rotated appearance for the field.
            // ------------------------------------------------------------
            // Define the rectangle where the field will be placed (original position).
            // Here we reuse the existing rectangle of the field.
            Aspose.Pdf.Rectangle originalRect = targetField.Rect;

            // Create a copy of the rectangle and rotate it by 90 degrees.
            // The Rotate method modifies the rectangle coordinates in place.
            Aspose.Pdf.Rectangle rotatedRect = new Aspose.Pdf.Rectangle(
                originalRect.LLX, originalRect.LLY, originalRect.URX, originalRect.URY);
            rotatedRect.Rotate(90); // rotate 90 degrees clockwise

            // Add a new appearance for the field on the same page using the rotated rectangle.
            // The AddFieldAppearance method adds an additional appearance stream.
            // The page number is 1‑based; use the field's PageIndex (also 1‑based).
            form.AddFieldAppearance(targetField, targetField.PageIndex, rotatedRect);

            // ------------------------------------------------------------
            // 3. Save the modified PDF.
            // ------------------------------------------------------------
            doc.Save(outputPdf);
            Console.WriteLine($"Rotated form field saved to '{outputPdf}'.");
        }
    }
}