using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF paths (use arguments if provided)
        string inputPath = args.Length > 0 ? args[0] : "input.pdf";
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Access the AcroForm object
            Form form = pdfDocument.Form;

            // -----------------------------------------------------------------
            // 1. Update existing fields
            // -----------------------------------------------------------------
            foreach (Field field in form.Fields)
            {
                // Set a new value for the field
                field.Value = "Updated";

                // Make the field read‑only
                field.ReadOnly = true;

                // Provide a tooltip (alternate name) for the field
                field.AlternateName = "Field tooltip";

                // Initialize the border after the field object has been created
                // (uses the provided border‑initialization rule)
                field.Border = new Border(field)
                {
                    Style = BorderStyle.Solid,
                    Width = 1
                };
            }

            // -----------------------------------------------------------------
            // 2. Add a new text field to the form
            // -----------------------------------------------------------------
            // Define the rectangle where the new field will appear (coordinates in points)
            Aspose.Pdf.Rectangle newFieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a generic field instance associated with the document
            Field newField = new Field(pdfDocument)
            {
                Name = "NewField",
                Rect = newFieldRect,
                Value = "New Value",
                ReadOnly = false,
                AlternateName = "New field tooltip"
            };

            // Set a dashed border for the new field (border‑initialization rule)
            newField.Border = new Border(newField)
            {
                Style = BorderStyle.Dashed,
                Width = 1
            };

            // Add the newly created field to the form collection
            form.Add(newField);

            // -----------------------------------------------------------------
            // 3. Save the modified PDF
            // -----------------------------------------------------------------
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}