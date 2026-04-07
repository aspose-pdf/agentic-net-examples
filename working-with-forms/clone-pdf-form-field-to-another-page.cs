using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string originalFieldName = "TextBox1";   // name of the field to clone
        const int targetPageNumber = 2;                 // page where the clone will be placed (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block (document disposal rule)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document actually contains a form and the field exists
            if (doc.Form == null || !doc.Form.HasField(originalFieldName))
            {
                Console.Error.WriteLine($"Form field '{originalFieldName}' not found.");
                return;
            }

            // Retrieve the original field – the Form indexer returns a WidgetAnnotation, so cast to Field
            Field originalField = doc.Form[originalFieldName] as Field;
            if (originalField == null)
            {
                Console.Error.WriteLine($"Field '{originalFieldName}' is not a form field.");
                return;
            }

            // Clone the field onto the target page with a new partial name.
            // The Add method returns a WidgetAnnotation, so cast the result to Field.
            Field clonedField = doc.Form.Add(originalField, originalFieldName + "_Clone", targetPageNumber) as Field;
            if (clonedField == null)
            {
                Console.Error.WriteLine("Failed to clone the field.");
                return;
            }

            // Modify properties of the cloned field as needed
            clonedField.Value = "Cloned value";
            clonedField.ReadOnly = false;                     // make it editable
            clonedField.Color = Aspose.Pdf.Color.LightGray;   // background color of the annotation

            // Optionally change the position of the cloned field on the target page
            // Here we move it 100 points to the right and 50 points down from its original location
            Aspose.Pdf.Rectangle origRect = clonedField.Rect;
            double offsetX = 100;
            double offsetY = -50; // negative Y moves down because PDF coordinate origin is bottom‑left
            clonedField.Rect = new Aspose.Pdf.Rectangle(
                origRect.LLX + offsetX,
                origRect.LLY + offsetY,
                origRect.URX + offsetX,
                origRect.URY + offsetY);

            // Save the modified PDF (document disposal rule ensures the stream is flushed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cloned field saved to '{outputPath}'.");
    }
}
