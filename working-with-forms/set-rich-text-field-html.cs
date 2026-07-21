using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // PDF containing a RichTextBox field
        const string outputPath = "output.pdf";         // Resulting PDF
        const string fieldName  = "RichTextField1";     // Name of the RichTextBox field

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field by name and cast to RichTextBoxField
            if (doc.Form[fieldName] is RichTextBoxField richField)
            {
                // Set the formatted (HTML) value – this markup will be rendered inside the field
                richField.FormattedValue = "<b>Bold Text</b> <i>Italic Text</i> <u>Underlined</u>";

                // Optionally set a default style (e.g., font family and size) for the field
                richField.Style = "font-family:Helvetica; font-size:12pt;";

                // Save the modified document
                doc.Save(outputPath);
                Console.WriteLine($"Rich text field updated and saved to '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a RichTextBoxField.");
            }
        }
    }
}