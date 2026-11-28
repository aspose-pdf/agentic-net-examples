using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "MultiFill.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Ensure the document actually contains a form.
        if (pdfDocument.Form == null || pdfDocument.Form.Fields == null)
        {
            Console.Error.WriteLine("The PDF does not contain any form fields.");
            return;
        }

        // Fill the required fields.
        SetFieldValue(pdfDocument, "Name", "John Doe");
        SetFieldValue(pdfDocument, "Email", "john.doe@example.com");
        SetFieldValue(pdfDocument, "Phone", "+1-555-1234");

        // Save the updated PDF.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Form fields filled and saved to '{outputPath}'.");
    }

    private static void SetFieldValue(Document doc, string fieldName, string value)
    {
        // Document.Form indexer returns a Field object. Cast to Field to access the Value property.
        Field field = doc.Form[fieldName] as Field;
        if (field != null)
        {
            field.Value = value;
        }
        else
        {
            Console.Error.WriteLine($"Field '{fieldName}' not found in the PDF.");
        }
    }
}
