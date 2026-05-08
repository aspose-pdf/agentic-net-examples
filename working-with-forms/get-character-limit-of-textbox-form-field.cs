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

        // Load the PDF document using the core Aspose.Pdf API.
        using (Document doc = new Document(inputPdf))
        {
            // Access the form field named "Address".
            // The Form indexer returns a generic Field; cast it to TextBoxField
            // because MaxLen (character limit) is defined on TextBoxField.
            TextBoxField addressField = doc.Form["Address"] as TextBoxField;

            if (addressField == null)
            {
                Console.WriteLine("The 'Address' field was not found or is not a text box field.");
                return;
            }

            // Retrieve the current character limit.
            int charLimit = addressField.MaxLen;

            // Output the result.
            Console.WriteLine($"Character limit for 'Address' field: {charLimit}");
        }
    }
}