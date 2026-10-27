using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document using the core API.
        Document doc = new Document(pdfPath);

        // Retrieve the form field named "Address" as a TextBoxField.
        var addressField = doc.Form["Address"] as TextBoxField;
        if (addressField == null)
        {
            Console.Error.WriteLine("Field 'Address' not found or is not a text box.");
            return;
        }

        // The MaxLen property represents the character limit of the field.
        int limit = addressField.MaxLen;

        Console.WriteLine($"Character limit for 'Address' field: {limit}");
    }
}