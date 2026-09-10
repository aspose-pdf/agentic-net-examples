using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class FormXmlExportImportExample
{
    static void Main()
    {
        const string pdfPath = "PdfForm.pdf";
        const string xmlExportPath = "formData.xml";
        const string pdfReimportedPath = "PdfForm_Imported.pdf";

        // ------------------------------------------------------------
        // Create a sample PDF with a simple form field if it does not
        // already exist. This makes the example self‑contained.
        // ------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            var doc = new Document();
            var page = doc.Pages.Add();

            // Add a textbox form field.
            var textBox = new TextBoxField(page, new Rectangle(100, 600, 300, 620))
            {
                PartialName = "SampleTextBox",
                Value = "Initial value"
            };
            doc.Form.Add(textBox, 1);

            doc.Save(pdfPath);
        }

        // ------------------------------------------------------------
        // Export form fields to XML.
        // ------------------------------------------------------------
        using (var formExporter = new Aspose.Pdf.Facades.Form(pdfPath))
        using (var xmlStream = new FileStream(xmlExportPath, FileMode.Create, FileAccess.Write))
        {
            formExporter.ExportXml(xmlStream);
        }

        // ------------------------------------------------------------
        // Re‑import the previously exported XML into a fresh copy of the PDF.
        // ------------------------------------------------------------
        using (var formImporter = new Aspose.Pdf.Facades.Form(pdfPath))
        using (var xmlInput = new FileStream(xmlExportPath, FileMode.Open, FileAccess.Read))
        {
            formImporter.ImportXml(xmlInput);
            formImporter.Save(pdfReimportedPath);
        }

        Console.WriteLine("Export and re‑import completed successfully.");
    }
}
