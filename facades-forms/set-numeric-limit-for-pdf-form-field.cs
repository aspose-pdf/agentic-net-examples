using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block to ensure proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field named "Quantity" from the document's form fields collection
            TextBoxField quantityField = doc.Form?.Fields?
                .FirstOrDefault(f => string.Equals(f.FullName, "Quantity", StringComparison.OrdinalIgnoreCase))
                as TextBoxField;

            if (quantityField == null)
            {
                Console.Error.WriteLine("Field 'Quantity' not found or is not a TextBoxField.");
            }
            else
            {
                // Define a numeric limit for the field: values must be between 1 and 100.
                // Aspose.Pdf does not expose a SetFieldLimit method. Use a JavaScript validation action.
                string js = @"if (event.value < 1 || event.value > 100) {\n" +
                            "    app.alert('Please enter a value between 1 and 100');\n" +
                            "    event.rc = false;\n" +
                            "}";
                quantityField.Actions.OnValidate = new JavascriptAction(js);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field limit applied and saved to '{outputPath}'.");
    }
}
