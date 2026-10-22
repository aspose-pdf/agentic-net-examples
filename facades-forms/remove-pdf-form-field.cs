using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "ObsoleteField";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // The Form indexer returns a WidgetAnnotation; cast it to Field to work with field‑specific members
        Field? field = pdfDocument.Form[fieldName] as Field;
        if (field != null)
        {
            // Delete the field using the Form API
            pdfDocument.Form.Delete(fieldName);
        }

        // Save the updated PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Removed field '{fieldName}' and saved to '{outputPath}'.");
    }
}
