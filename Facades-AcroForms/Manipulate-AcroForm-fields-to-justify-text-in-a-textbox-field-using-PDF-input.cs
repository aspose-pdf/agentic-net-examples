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

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Name of the textbox field to modify (replace with the actual field name)
            const string fieldName = "TextBox1";

            // Retrieve the field from the form collection
            var field = pdfDocument.Form[fieldName];

            // Ensure the field is a TextBoxField before applying justification
            if (field is TextBoxField textBox)
            {
                // Center‑align the text horizontally and vertically using the correct enums
                textBox.TextHorizontalAlignment = HorizontalAlignment.Center;
                textBox.TextVerticalAlignment   = VerticalAlignment.Center;

                // Optional: allow multiple lines if needed
                textBox.Multiline = true;
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a TextBoxField.");
                return;
            }

            // Save the modified PDF
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF saved with justified textbox field: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
