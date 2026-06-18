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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the form field named "Address"
            // The Form indexer returns a generic Field; cast to TextBoxField to access MaxLen
            TextBoxField addressField = doc.Form["Address"] as TextBoxField;

            if (addressField == null)
            {
                Console.WriteLine("The 'Address' field was not found or is not a text box field.");
                return;
            }

            // MaxLen holds the current character limit; -1 indicates no limit set
            int charLimit = addressField.MaxLen;

            Console.WriteLine($"Current character limit for the 'Address' field: {charLimit}");
        }
    }
}