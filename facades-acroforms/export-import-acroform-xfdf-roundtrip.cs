using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "form.pdf";          // source PDF with AcroForm fields
        const string xfdfPath      = "form_data.xfdf";    // temporary XFDF file
        const string outputPdfPath = "form_roundtrip.pdf"; // PDF after import

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Fill the form fields with sample data and export to XFDF
        // ------------------------------------------------------------
        using (Form formFiller = new Form(inputPdfPath))
        {
            // Fill each field with a distinct test value
            foreach (string fieldName in formFiller.FieldNames)
            {
                formFiller.FillField(fieldName, $"TestValue_{fieldName}");
            }

            // Export the filled field values to XFDF
            using (FileStream xfdfOut = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                formFiller.ExportXfdf(xfdfOut);
            }

            // Persist the filled values back to the original PDF
            formFiller.Save(); // saves to the bound document (inputPdfPath)
        }

        // ------------------------------------------------------------
        // 2. Import the XFDF back into a fresh copy of the PDF
        // ------------------------------------------------------------
        using (Form formImporter = new Form(inputPdfPath))
        {
            // Load the previously exported XFDF
            using (FileStream xfdfIn = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            {
                formImporter.ImportXfdf(xfdfIn);
            }

            // Save the result to a new file for verification
            formImporter.Save(outputPdfPath);

            // Verify that each field retained the original value
            Console.WriteLine("Round‑trip verification results:");
            foreach (string fieldName in formImporter.FieldNames)
            {
                object fieldValue = formImporter.GetField(fieldName);
                Console.WriteLine($"{fieldName} = {fieldValue}");
            }
        }
    }
}