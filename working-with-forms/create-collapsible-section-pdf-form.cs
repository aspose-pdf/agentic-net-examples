using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to host the form fields
            Page page = doc.Pages.Add();

            // Define the rectangle for the checkbox (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);
            // Create the checkbox field
            CheckboxField chk = new CheckboxField(page, chkRect);
            chk.Name = "expandCheckbox";
            chk.ExportValue = "Checked";
            // Optional visual settings
            chk.Color = Aspose.Pdf.Color.LightGray;
            // Border must be set after the checkbox instance is created
            chk.Border = new Border(chk) { Width = 1 };

            // Define the rectangle for the collapsible section (a simple text box)
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(50, 650, 550, 730);
            // Create the text box field that will be shown/hidden
            TextBoxField txt = new TextBoxField(page, txtRect)
            {
                Name = "collapsibleSection",
                // Initially hidden
                Flags = AnnotationFlags.Hidden,
                // Provide some placeholder content
                Contents = "This is the collapsible content. It becomes visible when the checkbox is checked."
            };

            // JavaScript to toggle visibility of the text box based on checkbox state
            string js = @"
if (event.target.checked) {
    this.getField('collapsibleSection').display = display.visible;
} else {
    this.getField('collapsibleSection').display = display.hidden;
}";
            // Assign the JavaScript action to the checkbox activation event
            chk.OnActivated = new JavascriptAction(js);

            // Add fields to the document's form
            doc.Form.Add(chk);
            doc.Form.Add(txt);

            // Save the PDF
            doc.Save("CollapsibleForm.pdf");
        }

        Console.WriteLine("PDF with collapsible section created successfully.");
    }
}