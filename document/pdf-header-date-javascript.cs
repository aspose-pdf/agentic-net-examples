using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

public class Program
{
    public static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add the first page (header will be placed here)
            Page firstPage = doc.Pages.Add();

            // Define the rectangle for the date field in the header area
            Aspose.Pdf.Rectangle dateRect = new Aspose.Pdf.Rectangle(50, 750, 200, 770);

            // Create a shared date field that will appear on every page
            DateField dateField = new DateField(firstPage, dateRect);
            dateField.IsSharedField = true;
            dateField.ReadOnly = true;
            doc.Form.Add(dateField);

            // JavaScript that sets the field value to the current date
            JavascriptAction jsAction = new JavascriptAction("this.value = new Date().toLocaleDateString();");
            dateField.ExecuteFieldJavaScript(jsAction);

            // Add additional pages to demonstrate the header appears on all pages
            doc.Pages.Add();
            doc.Pages.Add();

            // Save the PDF document
            doc.Save("output.pdf");
        }
    }
}