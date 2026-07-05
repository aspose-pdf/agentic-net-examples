using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xfdfPath = "fields.xfdf";
        const string xmlPath = "fields.xml";
        const string outputPdfPath = "output.pdf";
        const string outputFromXmlPath = "output_from_xml.pdf";

        // ------------------------------------------------------------
        // 1. Create a sample PDF with a form field containing Unicode
        // ------------------------------------------------------------
        CreateSamplePdfWithForm(inputPdfPath);

        // ------------------------------------------------------------
        // 2. Export form fields to XFDF (UTF‑8 characters are preserved)
        // ------------------------------------------------------------
        using (Aspose.Pdf.Facades.Form exportForm = new Aspose.Pdf.Facades.Form(inputPdfPath))
        {
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                // ExportXfdf writes UTF‑8 encoded XML to the stream internally.
                exportForm.ExportXfdf(xfdfStream);
            }
        }

        // ------------------------------------------------------------
        // 3. Export form fields to XML (UTF‑8 characters are preserved)
        // ------------------------------------------------------------
        using (Aspose.Pdf.Facades.Form exportFormXml = new Aspose.Pdf.Facades.Form(inputPdfPath))
        {
            using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                exportFormXml.ExportXml(xmlStream);
            }
        }

        // ------------------------------------------------------------
        // 4. Import XFDF back into a new PDF – read the XFDF file as UTF‑8 text first
        // ------------------------------------------------------------
        using (Aspose.Pdf.Facades.Form importForm = new Aspose.Pdf.Facades.Form(inputPdfPath))
        {
            string xfdfContent = File.ReadAllText(xfdfPath, Encoding.UTF8);
            using (MemoryStream xfdfMemory = new MemoryStream(Encoding.UTF8.GetBytes(xfdfContent)))
            {
                importForm.ImportXfdf(xfdfMemory);
            }
            // Save to a separate destination file using the new Save overload
            importForm.Save(outputPdfPath);
        }

        // ------------------------------------------------------------
        // 5. Import XML back into a new PDF – read the XML file as UTF‑8 text first
        // ------------------------------------------------------------
        using (Aspose.Pdf.Facades.Form importFormXml = new Aspose.Pdf.Facades.Form(inputPdfPath))
        {
            string xmlContent = File.ReadAllText(xmlPath, Encoding.UTF8);
            using (MemoryStream xmlMemory = new MemoryStream(Encoding.UTF8.GetBytes(xmlContent)))
            {
                importFormXml.ImportXml(xmlMemory);
            }
            importFormXml.Save(outputFromXmlPath);
        }

        Console.WriteLine("Export and import of form fields with Unicode characters completed.");
    }

    /// <summary>
    /// Creates a minimal PDF containing a single text box form field whose value
    /// includes Unicode characters. The file is saved to <paramref name="path"/>.
    /// </summary>
    private static void CreateSamplePdfWithForm(string path)
    {
        // Create an empty document and add a page.
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Define a rectangle for the text box field (in points).
        // Position: lower‑left (100, 600), upper‑right (300, 620).
        Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

        // Create a TextBoxField, give it a name and a Unicode default value.
        TextBoxField txtField = new TextBoxField(page, fieldRect)
        {
            PartialName = "UnicodeTextBox",
            Value = "こんにちは世界" // "Hello World" in Japanese.
        };

        // Add the field to the document's form collection.
        doc.Form.Add(txtField);

        // Save the PDF so that subsequent operations have a real file to work with.
        doc.Save(path);
    }
}
