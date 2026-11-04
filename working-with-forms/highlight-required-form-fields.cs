using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "highlighted.pdf";
        const string fieldName = "RequiredField"; // name of the field to highlight

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document contains a form
            if (doc.Form != null && doc.Form.Count > 0)
            {
                // Retrieve the field by name; cast to the appropriate field type (e.g., TextBoxField)
                var field = doc.Form[fieldName] as TextBoxField;
                if (field != null)
                {
                    // Set the background color via the field's Characteristics (System.Drawing.Color)
                    field.Characteristics.Background = System.Drawing.Color.Yellow;
                }
                else
                {
                    Console.WriteLine($"Field '{fieldName}' not found or not a TextBoxField.");
                }
            }
            else
            {
                Console.WriteLine("The PDF does not contain any form fields.");
            }

            // Save the modified PDF (still within the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlighted PDF saved to '{outputPath}'.");
    }
}
