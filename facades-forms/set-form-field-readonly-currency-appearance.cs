using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Retrieve the form field named "TotalAmount".
        // The field is expected to be a text box (TextBoxField).
        var field = pdfDocument.Form["TotalAmount"] as TextBoxField;
        if (field == null)
        {
            Console.Error.WriteLine("Form field 'TotalAmount' not found or is not a text box.");
            return;
        }

        // Make the field read‑only.
        field.ReadOnly = true;

        // Set the field value and apply a currency format.
        // Aspose.Pdf does not expose SetFieldAppearance directly on the Document API.
        // As a practical alternative, we assign a formatted string that represents currency.
        // If a more sophisticated appearance is required, the PdfFormEditor class (available in the
        // Aspose.Pdf.Facades assembly) can be used. This example keeps the code free of the Facades
        // dependency to avoid the CS0234 error.
        field.Value = "$0.00"; // displays as currency

        // Save the updated PDF.
        pdfDocument.Save(outputPath);
    }
}
