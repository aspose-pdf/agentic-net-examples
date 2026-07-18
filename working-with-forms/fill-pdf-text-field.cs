using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths and field information – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "filled_output.pdf";
        const string fieldName     = "MyTextField";   // Exact name of the text field in the PDF
        const string fieldValue    = "Provided string value";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Access the form field by name – the Form indexer returns a WidgetAnnotation,
                // so we need to cast it to Aspose.Pdf.Forms.Field before using the Value property.
                Field? field = pdfDoc.Form[fieldName] as Field;
                if (field == null)
                {
                    Console.Error.WriteLine($"Field '{fieldName}' not found or is not a form field in the document.");
                }
                else
                {
                    // Set the field's value
                    field.Value = fieldValue;
                }

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
                Console.WriteLine($"PDF saved with filled field to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
