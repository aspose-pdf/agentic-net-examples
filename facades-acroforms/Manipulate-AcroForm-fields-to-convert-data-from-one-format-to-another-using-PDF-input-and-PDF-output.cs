using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfTemplatePath = "template.pdf";      // PDF with AcroForm fields
        const string xmlDataPath     = "data.xml";          // Source XML containing form data
        const string outputPdfPath   = "filled_output.pdf"; // Resulting PDF after filling
        const string intermediateFdfPath = "intermediate.fdf"; // Temporary FDF file

        // Verify input files exist
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Convert XML form data to FDF using FormDataConverter
        // ------------------------------------------------------------
        using (FileStream xmlStream = File.OpenRead(xmlDataPath))
        using (FileStream fdfStream = File.Create(intermediateFdfPath))
        {
            // Static method – no instance required
            FormDataConverter.ConvertXmlToFdf(xmlStream, fdfStream);
        }

        // ------------------------------------------------------------
        // Step 2: Load the PDF template and import the FDF data
        // ------------------------------------------------------------
        using (Form pdfForm = new Form(pdfTemplatePath))
        {
            // ImportFdf fills the AcroForm fields with values from the FDF stream
            using (FileStream fdfInput = File.OpenRead(intermediateFdfPath))
            {
                pdfForm.ImportFdf(fdfInput);
            }

            // ------------------------------------------------------------
            // Step 3: Save the filled PDF
            // ------------------------------------------------------------
            pdfForm.Save(outputPdfPath);
        }

        // ------------------------------------------------------------
        // Optional: Export the filled PDF fields to JSON for further use
        // ------------------------------------------------------------
        const string jsonExportPath = "filled_fields.json";
        using (Form filledForm = new Form(outputPdfPath))
        using (FileStream jsonStream = File.Create(jsonExportPath))
        {
            // ExportJson writes field values to a JSON stream; the second argument
            // indicates whether to include button field values (false = exclude)
            filledForm.ExportJson(jsonStream, false);
        }

        // Clean up temporary FDF file
        try { File.Delete(intermediateFdfPath); } catch { /* ignore */ }

        Console.WriteLine($"Form data conversion completed. Output PDF: {outputPdfPath}");
        Console.WriteLine($"Exported JSON: {jsonExportPath}");
    }
}