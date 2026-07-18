using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;
using PdfForm = Aspose.Pdf.Facades.Form; // Alias to avoid ambiguity with Aspose.Pdf.Forms.Form

class Program
{
    static void Main()
    {
        const string sourcePdf = "input.pdf";          // PDF that will contain a form field
        const string exportXml = "form_fields.xml";    // XML file to store exported data
        const string importedPdf = "imported.pdf";     // PDF after re‑importing the XML

        // ------------------------------------------------------------
        // Create a sample PDF with at least one form field if it does not exist.
        // This satisfies the sandbox requirement that the file be present.
        // ------------------------------------------------------------
        if (!File.Exists(sourcePdf))
        {
            // Create a new empty document.
            Document doc = new Document();
            Page page = doc.Pages.Add();

            // Add a simple text box field to the page.
            TextBoxField txtField = new TextBoxField(page, new Rectangle(100, 600, 300, 650))
            {
                PartialName = "SampleField",
                Value = "Initial value"
            };

            // NOTE: Form fields must be added via the Form collection, not directly to page.Annotations.
            doc.Form.Add(txtField);

            // Persist the document so the later Form operations can load it.
            doc.Save(sourcePdf);
        }

        // ---------- Export form fields to XML ----------
        using (PdfForm formExporter = new PdfForm(sourcePdf))
        {
            using (FileStream xmlOut = new FileStream(exportXml, FileMode.Create, FileAccess.Write))
            {
                formExporter.ExportXml(xmlOut);
            }
        }

        // ---------- Re‑import the XML data into a PDF ----------
        using (PdfForm formImporter = new PdfForm(sourcePdf))
        {
            using (FileStream xmlIn = new FileStream(exportXml, FileMode.Open, FileAccess.Read))
            {
                formImporter.ImportXml(xmlIn);
            }

            // Save the PDF with the imported form data to a new file.
            formImporter.Save(importedPdf);
        }
    }
}
