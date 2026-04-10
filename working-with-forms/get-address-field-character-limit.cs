using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the form field named "Address"
            // The Form indexer returns a WidgetAnnotation, so cast to TextBoxField
            TextBoxField addressField = doc.Form["Address"] as TextBoxField;

            if (addressField == null)
            {
                Console.WriteLine("The 'Address' field was not found or is not a text box.");
                return;
            }

            // MaxLen property holds the character limit for the field
            int charLimit = addressField.MaxLen;

            Console.WriteLine($"Character limit for 'Address' field: {charLimit}");
        }
    }
}