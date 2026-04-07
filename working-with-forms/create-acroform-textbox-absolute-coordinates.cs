using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

// Create a new PDF document and ensure proper disposal
using (Document doc = new Document())
{
    // Add a blank page (pages are 1‑based)
    Page page = doc.Pages.Add();

    // Define the rectangle where the field will be placed (absolute coordinates)
    // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
    Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650); // llx, lly, urx, ury

    // Create a text box field (constructor requires the Document)
    TextBoxField txtField = new TextBoxField(doc);
    txtField.PartialName = "SampleTextBox";   // field name
    txtField.Value = "Enter text here";       // default value
    txtField.Color = Aspose.Pdf.Color.LightGray;

    // Border must be assigned after the field instance exists because it needs the parent annotation
    txtField.Border = new Border(txtField) { Width = 1 };

    // Add the field to the form on page 1
    doc.Form.Add(txtField, 1);

    // Add an additional appearance of the field at the specified location
    doc.Form.AddFieldAppearance(txtField, 1, fieldRect);

    // Save the PDF (no SaveOptions needed for PDF output)
    doc.Save("AcroFormAbsoluteCoordinates.pdf");
}