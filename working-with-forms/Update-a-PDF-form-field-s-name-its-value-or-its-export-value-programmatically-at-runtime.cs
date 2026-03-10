using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "updated.pdf";

        // Names before and after the update
        const string originalFieldName = "CustomerName";
        const string newFieldName      = "ClientName";

        // New value to set in the field
        const string newValue = "Acme Corporation";

        // New export (mapping) name – this is the name used when exporting form data
        const string newExportName = "client_name";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document actually contains a form
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Retrieve the field by its original name and cast to Field
            Field? field = doc.Form[originalFieldName] as Field;
            if (field == null)
            {
                Console.WriteLine($"Field '{originalFieldName}' not found or is not a form field.");
                return;
            }

            // Update the field's name (identifier used within the PDF)
            field.Name = newFieldName;

            // Update the field's displayed value
            field.Value = newValue;

            // Update the export/mapping name (used when exporting data, e.g., to XFDF/JSON)
            field.MappingName = newExportName;

            // Optionally, update the alternate name (tooltip shown in Acrobat)
            field.AlternateName = "Client name field";

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Form field updated and saved to '{outputPdf}'.");
    }
}
