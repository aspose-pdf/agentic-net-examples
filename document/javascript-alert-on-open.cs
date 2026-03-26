using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "alert_on_open.pdf";

        using (Document doc = new Document())
        {
            // Add a blank page (optional, ensures the PDF has content)
            Page page = doc.Pages.Add();

            // JavaScript that shows an alert when the document is opened
            JavascriptAction jsAction = new JavascriptAction("app.alert('Document opened');");

            // Assign the JavaScript as the document's open action
            doc.OpenAction = jsAction;

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine("PDF with JavaScript alert saved to '" + outputPath + "'.");
    }
}