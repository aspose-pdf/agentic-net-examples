using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_validated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Example: mark a specific field as required and give it a red border
            const string requiredFieldName = "Name"; // change to your field name

            if (form.HasField(requiredFieldName))
            {
                // Retrieve the field (as a generic Field, which derives from WidgetAnnotation)
                Field field = (Field)form[requiredFieldName];

                // Set the field as required
                field.Required = true;

                // Set a red border (color is set on the annotation itself)
                field.Color = Aspose.Pdf.Color.Red;

                // Optionally set border width via Border object (requires parent annotation in ctor)
                field.Border = new Border(field) { Width = 1 };
            }

            // Simulate form submission: check each required field and highlight empty ones
            foreach (Field field in form.Fields)
            {
                if (field.Required)
                {
                    string value = field.Value?.ToString();

                    // If the field is empty, ensure the border is red
                    if (string.IsNullOrEmpty(value))
                    {
                        field.Color = Aspose.Pdf.Color.Red;
                    }
                    else
                    {
                        // Optional: reset to default color when filled
                        field.Color = Aspose.Pdf.Color.Black;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Validated PDF saved to '{outputPath}'.");
    }
}
