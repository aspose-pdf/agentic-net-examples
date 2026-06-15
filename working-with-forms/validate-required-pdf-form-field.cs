using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";               // source PDF with a form
        const string outputPath = "output_validated.pdf";   // result PDF
        const string logPath = "validation_log.txt";        // validation report

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // NOTE: The property "EmulateRequiredGroups" does not exist in recent Aspose.Pdf versions.
            // Required‑field visual validation is performed automatically by the Validate method.
            // Therefore the line that attempted to set this property has been removed.

            // Example: set a specific field as required.
            // Replace "myField" with the actual field name in your PDF.
            const string fieldName = "myField";

            if (form.HasField(fieldName))
            {
                // Retrieve the field. In recent Aspose.PDF versions the indexer returns a WidgetAnnotation.
                // Cast it to WidgetAnnotation (which also implements the Field members we need).
                WidgetAnnotation field = (WidgetAnnotation)form[fieldName];

                // Mark the field as required
                field.Required = true;

                // Set the visual border: color is set on the annotation itself, Border only defines width.
                field.Color = Aspose.Pdf.Color.Red; // border color
                field.Border = new Border(field) { Width = 1 };
            }
            else
            {
                Console.WriteLine($"Field '{fieldName}' not found in the form.");
            }

            // Validate the document – this creates a log file.
            // The validation process will highlight required fields that are empty.
            bool isValid = doc.Validate(logPath, PdfFormat.PDF_A_1B);
            Console.WriteLine(isValid
                ? "Document is valid."
                : "Document has validation errors. See log for details.");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
