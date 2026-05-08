using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_center_aligned.pdf";
        const string fieldName = "MyTextField"; // replace with the actual field name

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the form field by name and cast to TextBoxField
            if (doc.Form[fieldName] is TextBoxField textField)
            {
                // Set horizontal alignment to center
                textField.TextHorizontalAlignment = HorizontalAlignment.Center;

                // Optionally, set vertical alignment as well
                // textField.TextVerticalAlignment = VerticalAlignment.Center;

                // Save the modified document
                doc.Save(outputPdf);
                Console.WriteLine($"Field '{fieldName}' alignment set to center. Saved to '{outputPdf}'.");
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a text box field.");
            }
        }
    }
}