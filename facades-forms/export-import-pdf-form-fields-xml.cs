using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Provides the Form class for import/export of XML data
using Aspose.Pdf.Forms;   // Provides form field classes such as TextBoxField

class Program
{
    static void Main()
    {
        const string sourcePdf = "input.pdf";          // Original PDF with form fields
        const string exportXml = "form_fields.xml";    // XML file to archive the form data
        const string reimportPdf = "reimported.pdf";   // PDF after re‑importing the XML data

        // Ensure a source PDF exists – create a minimal one with a form field if missing
        if (!File.Exists(sourcePdf))
        {
            CreateSamplePdf(sourcePdf);
            Console.WriteLine($"Sample PDF created at '{sourcePdf}'.");
        }

        // -------------------------------------------------
        // Export the form fields to XML for archival
        // -------------------------------------------------
        using (Aspose.Pdf.Facades.Form formExporter = new Aspose.Pdf.Facades.Form(sourcePdf))
        using (FileStream xmlOut = new FileStream(exportXml, FileMode.Create, FileAccess.Write))
        {
            // ExportXml writes the form field values (except button values) to the provided stream
            formExporter.ExportXml(xmlOut);
        }
        Console.WriteLine($"Form data exported to '{exportXml}'.");

        // -------------------------------------------------
        // Re‑import the previously exported XML back into a PDF
        // -------------------------------------------------
        using (Aspose.Pdf.Facades.Form formImporter = new Aspose.Pdf.Facades.Form(sourcePdf))
        using (FileStream xmlIn = new FileStream(exportXml, FileMode.Open, FileAccess.Read))
        {
            // ImportXml reads the XML stream and populates the form fields
            formImporter.ImportXml(xmlIn);

            // Save the PDF with the imported data to a new file
            formImporter.Save(reimportPdf);
        }
        Console.WriteLine($"PDF with re‑imported data saved as '{reimportPdf}'.");
    }

    /// <summary>
    /// Creates a very simple PDF containing a single text box form field.
    /// This helper is used only when the expected input file does not exist.
    /// </summary>
    private static void CreateSamplePdf(string path)
    {
        // Create an empty document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Define a rectangle for the text box field (in points)
            var rect = new Aspose.Pdf.Rectangle(100, 700, 300, 650);

            // Create a text box field and add it to the page's form
            TextBoxField txtField = new TextBoxField(page, rect)
            {
                PartialName = "SampleTextBox",
                Value = "Initial value"
            };
            doc.Form.Add(txtField);

            // Save the document to the specified path
            doc.Save(path);
        }
    }
}
