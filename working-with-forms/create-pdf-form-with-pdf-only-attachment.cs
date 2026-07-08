using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "pdf_with_file_attachment_form.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle where the file input box will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Use a TextBoxField to simulate a file‑select field (FileSelectBoxField has no public constructor)
            TextBoxField fileInput = new TextBoxField(page, rect);
            fileInput.PartialName = "pdfAttachment";

            // JavaScript validation to allow only PDF files
            JavascriptAction validateJs = new JavascriptAction(
                "if (event.value != null && event.value.length > 0) {" +
                "    var ext = event.value.substr(event.value.lastIndexOf('.')).toLowerCase();" +
                "    if (ext != '.pdf') {" +
                "        app.alert('Only PDF files are allowed.');" +
                "        event.rc = false;" +
                "    }" +
                "}");
            fileInput.Actions.OnValidate = validateJs;

            // Optionally set a tooltip (alternate name) for the field
            fileInput.AlternateName = "Attach a PDF file";

            // Add the field to the document's form
            doc.Form.Add(fileInput);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with file attachment field saved to '{outputPath}'.");
    }
}
