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
        const string sourceFieldName = "MyTextField";   // name of the existing field to clone
        const int targetPageNumber = 2;                // page where the clone will be placed (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form associated with the document
            Form form = doc.Form;

            // Retrieve the original field; the Form indexer returns WidgetAnnotation,
            // so cast it explicitly to Aspose.Pdf.Forms.Field
            Field originalField = form[sourceFieldName] as Field;
            if (originalField == null)
            {
                Console.Error.WriteLine($"Field '{sourceFieldName}' not found in the document.");
                return;
            }

            // Clone the field onto the target page with a new partial name.
            // The Add method creates a copy if the field is already placed elsewhere.
            const string clonedPartialName = "MyTextField_Clone";
            Field clonedField = form.Add(originalField, clonedPartialName, targetPageNumber);

            // Modify properties of the cloned field as needed
            clonedField.Value = "Cloned value"; // set the field's value
            // Set a new rectangle (position and size) on the target page
            clonedField.Rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            // Optionally change the visual appearance (e.g., border color)
            clonedField.Color = Aspose.Pdf.Color.Blue;

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cloned field saved to '{outputPath}'.");
    }
}