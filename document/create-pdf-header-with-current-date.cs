using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "HeaderWithDate.pdf";

        using (Document doc = new Document())
        {
            // Add a single page (default size A4).
            Page page = doc.Pages.Add();

            // Define a rectangle for the header field (top of the page).
            // Coordinates: lower‑left X, lower‑left Y, upper‑right X, upper‑right Y.
            // A4 width ≈ 595 points, height ≈ 842 points.
            Aspose.Pdf.Rectangle headerRect = new Aspose.Pdf.Rectangle(0, 750, 595, 800);

            // Create a read‑only text box field that will hold the date.
            TextBoxField dateField = new TextBoxField(page, headerRect)
            {
                PartialName = "dateHeader",   // field name used in JavaScript
                Value = "",                   // initial value (will be set by script)
                ReadOnly = true,
                Color = Aspose.Pdf.Color.Transparent // optional: make background transparent
            };

            // The Border property expects an Aspose.Pdf.Annotations.Border instance.
            dateField.Border = new Border(dateField) { Width = 0 };

            // Add the field to the document's form collection (not directly to page annotations).
            doc.Form.Add(dateField);

            // JavaScript that runs when the document is opened.
            // It sets the value of the "dateHeader" field to the current date.
            string js = "this.getField('dateHeader').value = (new Date()).toLocaleDateString();";
            doc.OpenAction = new JavascriptAction(js);

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with date header saved to '{outputPath}'.");
    }
}
