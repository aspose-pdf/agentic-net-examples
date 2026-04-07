using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // for Aspose.Pdf.Color

// Create a new PDF document and ensure deterministic disposal
using (Document doc = new Document())
{
    // Add a blank page (1‑based indexing)
    Page page = doc.Pages.Add();

    // Define the rectangle where the form field will appear
    // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
    Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

    // Create a text box form field on the page
    TextBoxField textBox = new TextBoxField(page, fieldRect);

    // Set the default appearance (font, size, color) for this field
    // Use the constructor that accepts font name, size, and Aspose.Pdf.Color
    textBox.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Blue);

    // Add the field to the document's form collection
    doc.Form.Add(textBox);

    // Save the PDF to disk
    doc.Save("field_with_default_appearance.pdf");
}