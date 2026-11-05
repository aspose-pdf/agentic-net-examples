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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Name of the submit button field in the PDF form
            const string buttonName = "SubmitButton";

            // Ensure the document contains a form
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.Error.WriteLine("No form fields found in the document.");
                return;
            }

            // Retrieve the button field; cast to ButtonField
            ButtonField submitButton = doc.Form[buttonName] as ButtonField;
            if (submitButton == null)
            {
                Console.Error.WriteLine($"Button field '{buttonName}' not found.");
                return;
            }

            // Set the tooltip (alternate name) that explains required fields
            const string tooltip = "Please fill in all required fields before submitting.";
            submitButton.AlternateName = tooltip;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tooltip to '{outputPath}'.");
    }
}
