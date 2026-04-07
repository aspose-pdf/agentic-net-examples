using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xmlPath          = "input.xml";
        const string intermediatePdf  = "intermediate.pdf";
        const string outputPdf        = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // 1. Convert the XML to PDF.
        using (Document pdfDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            pdfDoc.Save(intermediatePdf);
        }

        // 2. Add a document‑level JavaScript action (executed when the PDF is opened).
        using (PdfContentEditor contentEditor = new PdfContentEditor())
        {
            contentEditor.BindPdf(intermediatePdf);
            string jsOnOpen = "app.alert('Please fill all required fields before submitting.');";
            contentEditor.AddDocumentAdditionalAction(PdfContentEditor.DocumentOpen, jsOnOpen);
            // Overwrite the intermediate file with the added action.
            contentEditor.Save(intermediatePdf);
        }

        // 3. Add JavaScript to a push‑button field (field name must exist in the form).
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(intermediatePdf);
            // Example script that validates a field named "Name".
            string fieldJs = "if (this.getField('Name').value == '') { app.alert('Name is required'); }";
            formEditor.AddFieldScript("ValidateBtn", fieldJs);
            // Save the final PDF.
            formEditor.Save(outputPdf);
        }

        // Optional: delete the temporary intermediate file.
        try { File.Delete(intermediatePdf); } catch { }

        Console.WriteLine($"PDF with embedded JavaScript saved to '{outputPdf}'.");
    }
}