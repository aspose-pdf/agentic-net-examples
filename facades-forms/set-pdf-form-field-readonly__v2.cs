using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // Added missing namespace for TextFragment and Position

class Program
{
    static void Main()
    {
        const string inputPath  = "application_form_v7.pdf";
        const string outputPath = "readonly_version.pdf";

        // Load existing PDF or create a placeholder with a "Version" field.
        Document pdfDoc;
        if (File.Exists(inputPath))
        {
            pdfDoc = new Document(inputPath);
        }
        else
        {
            pdfDoc = CreatePlaceholderPdf();
            pdfDoc.Save(inputPath);
        }

        // Bind the document to FormEditor and set the field as read‑only.
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(pdfDoc);
            formEditor.SetFieldAttribute("Version", PropertyFlag.ReadOnly);
            formEditor.Save(outputPath);
        }
    }

    // Creates a minimal PDF containing a single editable text box named "Version".
    private static Document CreatePlaceholderPdf()
    {
        var doc = new Document();
        var page = doc.Pages.Add();
        var fieldRect = new Rectangle(100, 700, 300, 730);
        var versionField = new TextBoxField(page, fieldRect)
        {
            PartialName = "Version",
            Value = "1.0",
            ReadOnly = false // will be set to read‑only by FormEditor later
        };
        doc.Form.Add(versionField, 1);
        // Add a label for visual clarity.
        page.Paragraphs.Add(new TextFragment("Version:") { Position = new Position(100, 750) });
        return doc;
    }
}
