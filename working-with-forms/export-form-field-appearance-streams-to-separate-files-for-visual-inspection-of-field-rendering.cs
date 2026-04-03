using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class ExportFormFieldAppearances
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "FieldAppearances";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over each form field (WidgetAnnotation) in the document
            int fieldIndex = 1;
            foreach (WidgetAnnotation field in pdfDoc.Form)
            {
                // Build a file name for the current field's appearance export
                string fieldFileName = Path.Combine(outputFolder, $"field_{fieldIndex}.json");

                // Export the field (including its appearance stream) to a JSON file
                // The ExportToJson method writes the field definition and appearance data.
                field.ExportToJson(fieldFileName);

                Console.WriteLine($"Exported field {fieldIndex} to '{fieldFileName}'");
                fieldIndex++;
            }
        }

        Console.WriteLine("All form field appearances have been exported.");
    }
}