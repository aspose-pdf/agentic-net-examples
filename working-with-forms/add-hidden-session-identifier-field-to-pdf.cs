using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_hidden_field.pdf";
        const string sessionId  = "ABC123XYZ"; // example session identifier

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a hidden text box field to store the session identifier
            // Rectangle placed off‑page (0,0,1,1) – size is irrelevant because the field will be hidden
            TextBoxField hiddenField = new TextBoxField(
                doc,
                new Aspose.Pdf.Rectangle(0, 0, 1, 1)   // left, bottom, width, height
            );

            // Set the field name (partial name) and its value
            hiddenField.PartialName = "SessionId";
            hiddenField.Value = sessionId;

            // Mark the field as hidden (and optionally read‑only)
            hiddenField.Flags = AnnotationFlags.Hidden;
            hiddenField.ReadOnly = true;

            // Add the field to the form on page 1 (Aspose.Pdf uses 1‑based indexing)
            doc.Form.Add(hiddenField, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden session field: {outputPath}");
    }
}