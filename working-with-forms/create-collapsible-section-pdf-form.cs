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
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Create the checkbox that will control the section
            // -------------------------------------------------
            // Rectangle: left, bottom, right, top (coordinates in points)
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);
            CheckboxField toggleChk = new CheckboxField(page, chkRect);
            toggleChk.Name = "toggleChk";          // full field name
            toggleChk.PartialName = "toggleChk";
            toggleChk.ExportValue = "On";          // value when checked
            // Optional visual settings
            toggleChk.Color = Aspose.Pdf.Color.Blue;
            // Border must be assigned after the field is instantiated
            toggleChk.Border = new Border(toggleChk) { Width = 1 };
            // Add the checkbox to the form
            doc.Form.Add(toggleChk);

            // -------------------------------------------------
            // 2. Create the collapsible content (a multiline text box)
            // -------------------------------------------------
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(50, 600, 550, 730);
            TextBoxField collapsibleTxt = new TextBoxField(page, txtRect)
            {
                Name = "collapsibleTxt",
                PartialName = "collapsibleTxt",
                Multiline = true,
                // Initially hidden
                Flags = AnnotationFlags.Hidden,
                // Sample placeholder text
                Contents = "This is the collapsible section.\nIt becomes visible when the checkbox is checked."
            };
            doc.Form.Add(collapsibleTxt);

            // -------------------------------------------------
            // 3. Attach JavaScript to the checkbox to toggle visibility
            // -------------------------------------------------
            // The script toggles the 'display' property of the target field.
            string js = @"
var f = this.getField('collapsibleTxt');
if (f.display == display.hidden) {
    f.display = display.visible;
} else {
    f.display = display.hidden;
}";
            toggleChk.OnActivated = new JavascriptAction(js);

            // -------------------------------------------------
            // 4. Save the PDF
            // -------------------------------------------------
            doc.Save("CollapsibleForm.pdf");
        }

        Console.WriteLine("PDF with collapsible section created successfully.");
    }
}