using System;
using System.IO;
using Aspose.Pdf.Facades;

class AcroFormImportExport
{
    static void Main(string[] args)
    {
        // Input PDF with AcroForm fields
        const string inputPdfPath = "input.pdf";
        // Output PDF after import
        const string outputPdfPath = "output.pdf";
        // Exported data files
        const string exportJsonPath = "formData.json";
        const string importJsonPath = "formDataToImport.json";

        // Validate input PDF
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Export form data to JSON
        // -----------------------------------------------------------------
        using (Form formExporter = new Form())
        {
            // Load the PDF document into the Form facade
            formExporter.BindPdf(inputPdfPath); // load rule

            // Export all field values to a JSON stream
            using (FileStream jsonExportStream = new FileStream(exportJsonPath, FileMode.Create, FileAccess.Write))
            {
                formExporter.ExportJson(jsonExportStream, false);
            }

            Console.WriteLine($"Form data exported to JSON: {exportJsonPath}");
        }

        // -----------------------------------------------------------------
        // (Optional) Modify the exported JSON file externally here.
        // For demonstration we simply copy the exported file to the import file.
        // In real scenarios the import file may contain edited values.
        // -----------------------------------------------------------------
        if (!File.Exists(importJsonPath))
        {
            File.Copy(exportJsonPath, importJsonPath);
        }

        // -----------------------------------------------------------------
        // Import form data from JSON back into a PDF
        // -----------------------------------------------------------------
        using (Form formImporter = new Form())
        {
            // Load the original PDF again (could be a different template)
            formImporter.BindPdf(inputPdfPath); // load rule

            // Import field values from the JSON stream
            using (FileStream jsonImportStream = new FileStream(importJsonPath, FileMode.Open, FileAccess.Read))
            {
                formImporter.ImportJson(jsonImportStream);
            }

            // Save the modified PDF to a new file
            formImporter.Save(outputPdfPath); // save rule
            Console.WriteLine($"Form data imported and PDF saved to: {outputPdfPath}");
        }

        // -----------------------------------------------------------------
        // Demonstrate export to FDF and import from FDF as an alternative
        // -----------------------------------------------------------------
        const string exportFdfPath = "formData.fdf";
        const string importFdfPath = "formDataToImport.fdf";

        using (Form fdfHandler = new Form())
        {
            fdfHandler.BindPdf(inputPdfPath);
            using (FileStream fdfExport = new FileStream(exportFdfPath, FileMode.Create, FileAccess.Write))
            {
                fdfHandler.ExportFdf(fdfExport);
            }
            Console.WriteLine($"Form data exported to FDF: {exportFdfPath}");
        }

        // For import demonstration, copy the exported FDF if import file does not exist
        if (!File.Exists(importFdfPath))
        {
            File.Copy(exportFdfPath, importFdfPath);
        }

        using (Form fdfHandler = new Form())
        {
            fdfHandler.BindPdf(inputPdfPath);
            using (FileStream fdfImport = new FileStream(importFdfPath, FileMode.Open, FileAccess.Read))
            {
                fdfHandler.ImportFdf(fdfImport);
            }
            // Save the result of FDF import
            string fdfOutputPath = "output_from_fdf.pdf";
            fdfHandler.Save(fdfOutputPath);
            Console.WriteLine($"Form data imported from FDF and PDF saved to: {fdfOutputPath}");
        }
    }
}