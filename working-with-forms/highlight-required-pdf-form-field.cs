using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "template.pdf";
        const string outputPath = "validated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // The EmulateRequiredGroups property does not exist in the current Aspose.Pdf API.
            // Required‑field visualisation can be achieved by setting the field's border/color manually.

            Form form = doc.Form;
            const string fieldName = "NameField";

            // If the field does not exist, create it
            if (!form.HasField(fieldName))
            {
                // Define the field rectangle (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);
                TextBoxField txtField = new TextBoxField(doc, rect)
                {
                    PartialName = fieldName,
                    Required = true,
                    // Set the border color (the Color property affects the annotation's border color)
                    Color = Aspose.Pdf.Color.Red
                };
                // Set border width (Border requires the parent annotation in constructor)
                txtField.Border = new Border(txtField) { Width = 2 };
                form.Add(txtField);
            }
            else
            {
                // Retrieve existing field and mark it as required with a red border
                // The Form indexer returns a WidgetAnnotation; cast it to Field to access form‑field members.
                Field existingField = form[fieldName] as Field;
                if (existingField != null)
                {
                    existingField.Required = true;
                    existingField.Color = Aspose.Pdf.Color.Red;
                    existingField.Border = new Border(existingField) { Width = 2 };
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with required field highlighted: {outputPath}");
    }
}
