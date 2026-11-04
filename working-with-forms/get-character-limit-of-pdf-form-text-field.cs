using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";   // path to the PDF containing the form
        const string fieldName = "Address";   // name of the field whose limit we want

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document using the core Document API
        Document doc = new Document(pdfPath);

        // Access the form field via the Document.Form collection
        TextBoxField addressField = doc.Form[fieldName] as TextBoxField;

        // The character limit is stored in the MaxLen property of a TextBoxField
        int limit = addressField?.MaxLen ?? 0;

        Console.WriteLine($"Field '{fieldName}' character limit: {limit}");
    }
}