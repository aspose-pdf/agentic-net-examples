using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF with a form field
        const string outputPath = "output.pdf";  // destination PDF
        const string fieldName  = "myTextField"; // name of the textbox field to modify
        const int    maxLength  = 10;            // desired maximum character length

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the recommended using block for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form and the specified field
            if (doc.Form != null &&
                doc.Form.Fields != null &&
                doc.Form.Fields.Any(f => f.Name == fieldName))
            {
                // Retrieve the field as a TextBoxField
                TextBoxField txtField = doc.Form[fieldName] as TextBoxField;
                if (txtField != null)
                {
                    // Set the maximum allowed characters for user input
                    txtField.MaxLen = maxLength;
                }
                else
                {
                    Console.Error.WriteLine($"Field '{fieldName}' is not a TextBoxField.");
                }
            }
            else
            {
                Console.Error.WriteLine($"Form does not contain a field named '{fieldName}'.");
            }

            // Save the modified PDF (PDF format does not require explicit SaveOptions)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
