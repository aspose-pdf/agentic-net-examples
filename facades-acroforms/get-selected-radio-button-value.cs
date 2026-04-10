using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the PDF containing the radio button group
        const string inputPdf = "input.pdf";

        // Fully qualified name of the radio button field (as defined in the PDF)
        const string radioFieldName = "RadioGroup1";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document. The Document class provides direct access to form fields
        // and does not rely on the external AsposePdfApi.exe process used by the old Form facade.
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Retrieve the radio button option field from the form collection.
            // The field is cast to RadioButtonOptionField to expose the Value property.
            RadioButtonOptionField radioField = pdfDocument.Form[radioFieldName] as RadioButtonOptionField;

            if (radioField == null)
            {
                Console.Error.WriteLine($"Radio button field '{radioFieldName}' not found or is not a radio button group.");
                return;
            }

            // The Value property contains the export value of the selected option.
            // It can be null if no option is selected.
            string currentValue = radioField.Value?.ToString() ?? "<none>";

            // Output the result.
            Console.WriteLine($"Current value of radio field '{radioFieldName}': {currentValue}");
        }
    }
}
