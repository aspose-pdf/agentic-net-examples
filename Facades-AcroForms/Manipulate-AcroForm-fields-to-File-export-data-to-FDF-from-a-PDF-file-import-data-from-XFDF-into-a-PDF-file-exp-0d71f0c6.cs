using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing AcroForm fields
        const string inputPdfPath = "input.pdf";

        // Paths for exported/imported data
        const string fdfExportPath = "formData.fdf";
        const string xfdfImportPath = "importData.xfdf";
        const string xfdfExportPath = "formData.xfdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // Export form fields to FDF
        // ------------------------------------------------------------
        using (Form formExporter = new Form())
        {
            // Bind the PDF document to the Form facade
            formExporter.BindPdf(inputPdfPath);

            // Export the field data to an FDF file
            using (FileStream fdfStream = new FileStream(fdfExportPath, FileMode.Create, FileAccess.Write))
            {
                formExporter.ExportFdf(fdfStream);
            }
        }

        // ------------------------------------------------------------
        // Import form fields from XFDF (if the XFDF file exists)
        // ------------------------------------------------------------
        if (File.Exists(xfdfImportPath))
        {
            using (Form formImporter = new Form())
            {
                formImporter.BindPdf(inputPdfPath);

                // Import field values from the XFDF stream
                using (FileStream xfdfIn = new FileStream(xfdfImportPath, FileMode.Open, FileAccess.Read))
                {
                    formImporter.ImportXfdf(xfdfIn);
                }

                // Save the PDF with the imported values
                const string updatedPdfPath = "updated_from_xfdf.pdf";
                formImporter.Save(updatedPdfPath);
            }
        }

        // ------------------------------------------------------------
        // Export form fields to XFDF
        // ------------------------------------------------------------
        using (Form xfdfExporter = new Form())
        {
            xfdfExporter.BindPdf(inputPdfPath);

            // Export the field data to an XFDF file
            using (FileStream xfdfOut = new FileStream(xfdfExportPath, FileMode.Create, FileAccess.Write))
            {
                xfdfExporter.ExportXfdf(xfdfOut);
            }
        }
    }
}