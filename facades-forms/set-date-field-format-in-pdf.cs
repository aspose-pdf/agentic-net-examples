using System;
using System.IO;
using System.Linq;                     // Needed for LINQ extensions
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Forms;        // Form field types (DateField, etc.)

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "Date";               // Exact name of the date field
        const string newFormat = "MM/dd/yyyy";         // Desired display format

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document using the core API (no ambiguous Form class)
        Document pdfDocument = new Document(inputPdf);

        // Verify that the document actually contains a form and that the field exists
        bool fieldExists = pdfDocument.Form?.Fields?.Any(f => f.FullName == fieldName) ?? false;
        if (!fieldExists)
        {
            Console.Error.WriteLine($"Field \"{fieldName}\" not found.");
            return;
        }

        // Retrieve the field as a DateField
        DateField dateField = pdfDocument.Form[fieldName] as DateField;
        if (dateField == null)
        {
            Console.Error.WriteLine($"Field \"{fieldName}\" is not a Date field.");
            return;
        }

        // Set the desired display format for the date field
        dateField.DateFormat = newFormat;

        // Save the modified PDF
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"Date field format updated and saved to '{outputPdf}'.");
    }
}
