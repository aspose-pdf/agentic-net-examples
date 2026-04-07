using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Enable drawing of red rectangles for required XFA groups (if the PDF contains XFA)
            doc.Form.EmulateRequierdGroups = true;

            // Iterate over all form fields
            foreach (Field field in doc.Form)
            {
                // Example condition: apply to a specific field name; remove the if to apply to all fields
                if (field.PartialName == "myRequiredField")
                {
                    // Mark the field as required
                    field.Required = true;

                    // Set a red border around the field
                    field.Color = Aspose.Pdf.Color.Red;                     // border color
                    field.Border = new Border(field) { Width = 1 };         // border thickness
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with required-field highlighting: '{outputPath}'.");
    }
}