using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
// Aspose.Pdf.Annotations is not required for DefaultAppearance, but kept if other annotations are added later
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_form.pdf";
        const string outputPath = "flattened_form.pdf";
        const string fieldName  = "TextBox1"; // replace with the actual field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Retrieve the specific TextBoxField by name
            TextBoxField txtField = form[fieldName] as TextBoxField;
            if (txtField == null)
            {
                Console.Error.WriteLine($"TextBoxField '{fieldName}' not found.");
                return;
            }

            // Set the desired font size and color using DefaultAppearance
            // DefaultAppearance constructor expects System.Drawing.Color for the third argument
            txtField.DefaultAppearance = new DefaultAppearance("Helvetica", 14, System.Drawing.Color.Blue);

            // Flatten the form – removes all interactive fields and places their values on the page
            doc.Flatten();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}
