using System;
using System.IO;
using Aspose.Pdf.Facades;

class FormFieldImportExportDemo
{
    static void Main()
    {
        // Paths for the source PDF and the resulting PDFs after import operations
        const string sourcePdfPath = "input.pdf";
        const string exportedFdfPath = "exported.fdf";
        const string exportedJsonPath = "exported.json";
        const string exportedXfdfPath = "exported.xfdf";
        const string exportedXmlPath = "exported.xml";

        const string importedFromFdfPdf = "imported_from_fdf.pdf";
        const string importedFromJsonPdf = "imported_from_json.pdf";
        const string importedFromXfdfPdf = "imported_from_xfdf.pdf";
        const string importedFromXmlPdf = "imported_from_xml.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // Export form fields from the source PDF to various formats
        // ------------------------------------------------------------
        using (Form formExporter = new Form(sourcePdfPath))
        {
            // Export to FDF
            using (FileStream fdfStream = new FileStream(exportedFdfPath, FileMode.Create, FileAccess.Write))
            {
                formExporter.ExportFdf(fdfStream);
            }

            // Export to JSON (second parameter: includeButtonValues = false)
            using (FileStream jsonStream = new FileStream(exportedJsonPath, FileMode.Create, FileAccess.Write))
            {
                formExporter.ExportJson(jsonStream, false);
            }

            // Export to XFDF
            using (FileStream xfdfStream = new FileStream(exportedXfdfPath, FileMode.Create, FileAccess.Write))
            {
                formExporter.ExportXfdf(xfdfStream);
            }

            // Export to XML
            using (FileStream xmlStream = new FileStream(exportedXmlPath, FileMode.Create, FileAccess.Write))
            {
                formExporter.ExportXml(xmlStream);
            }
        }

        // ------------------------------------------------------------
        // Import the previously exported data back into new PDFs
        // ------------------------------------------------------------

        // 1. Import from FDF
        using (Form formFromFdf = new Form(sourcePdfPath, importedFromFdfPdf))
        {
            using (FileStream fdfIn = new FileStream(exportedFdfPath, FileMode.Open, FileAccess.Read))
            {
                formFromFdf.ImportFdf(fdfIn);
            }
            formFromFdf.Save(); // Saves to the output file specified in the constructor
        }

        // 2. Import from JSON
        using (Form formFromJson = new Form(sourcePdfPath, importedFromJsonPdf))
        {
            using (FileStream jsonIn = new FileStream(exportedJsonPath, FileMode.Open, FileAccess.Read))
            {
                formFromJson.ImportJson(jsonIn);
            }
            formFromJson.Save();
        }

        // 3. Import from XFDF
        using (Form formFromXfdf = new Form(sourcePdfPath, importedFromXfdfPdf))
        {
            using (FileStream xfdfIn = new FileStream(exportedXfdfPath, FileMode.Open, FileAccess.Read))
            {
                formFromXfdf.ImportXfdf(xfdfIn);
            }
            formFromXfdf.Save();
        }

        // 4. Import from XML
        using (Form formFromXml = new Form(sourcePdfPath, importedFromXmlPdf))
        {
            using (FileStream xmlIn = new FileStream(exportedXmlPath, FileMode.Open, FileAccess.Read))
            {
                // The second overload allows ignoring template changes; here we keep default (false)
                formFromXml.ImportXml(xmlIn);
            }
            formFromXml.Save();
        }

        // ------------------------------------------------------------
        // Example: Fill a couple of fields programmatically and save
        // ------------------------------------------------------------
        const string filledPdfPath = "filled_output.pdf";
        using (Form formFiller = new Form(sourcePdfPath, filledPdfPath))
        {
            // Fill a text field
            formFiller.FillField("FirstName", "John");
            formFiller.FillField("LastName", "Doe");

            // Fill a checkbox (true = checked)
            formFiller.FillField("SubscribeNewsletter", true);

            // Fill a list box with the second item (index is zero‑based)
            formFiller.FillField("CountryList", 1);

            // Save the modified PDF
            formFiller.Save();
        }

        Console.WriteLine("Export and import operations completed successfully.");
    }
}