using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class ExportCheckboxesToFdf
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFdfPath = "checkboxes.fdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the Facades Form object with the loaded document
            Aspose.Pdf.Facades.Form formFacade = new Aspose.Pdf.Facades.Form(pdfDoc);

            // Iterate over all form fields in the document
            foreach (Field field in pdfDoc.Form.Fields)
            {
                // If the field is a checkbox, keep it exportable (default is true)
                // Otherwise, mark it as non‑exportable so it won't appear in the FDF
                if (field is CheckboxField)
                {
                    // Ensure checkbox fields are exportable
                    ((CheckboxField)field).Exportable = true;
                }
                else
                {
                    // Hide non‑checkbox fields from the export
                    field.Exportable = false;
                }
            }

            // Export only the checkbox field definitions to an FDF stream
            using (FileStream fdfStream = new FileStream(outputFdfPath, FileMode.Create, FileAccess.Write))
            {
                formFacade.ExportFdf(fdfStream);
            }

            Console.WriteLine($"Checkbox field definitions exported to '{outputFdfPath}'.");
        }
    }
}
