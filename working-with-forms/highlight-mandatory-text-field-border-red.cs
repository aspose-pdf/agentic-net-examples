using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // required for Border class

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "MandatoryField"; // name of the text field to highlight

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the text box field by its name
                TextBoxField txtField = doc.Form[fieldName] as TextBoxField;

                if (txtField != null)
                {
                    // Set the border colour – for form fields the colour is controlled by the field's Color property
                    txtField.Color = Aspose.Pdf.Color.Red;

                    // Increase border width so the red border is noticeable
                    // Border requires the parent annotation (the field) in its constructor
                    txtField.Border = new Border(txtField) { Width = 2 };
                }
                else
                {
                    Console.Error.WriteLine($"Text field '{fieldName}' not found.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Highlighted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
