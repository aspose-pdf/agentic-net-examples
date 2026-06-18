using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF file
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Open the PDF and add a link annotation that runs JavaScript to open a URL
        using (Document doc = new Document("input.pdf"))
        {
            Page page = doc.Pages[1];
            // Define the rectangle area for the annotation (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            LinkAnnotation link = new LinkAnnotation(page, rect);

            // JavaScript code to open a web page in a new window
            JavascriptAction jsAction = new JavascriptAction("app.launchURL('https://www.example.com', true);");
            link.Action = jsAction;

            // Optional visual appearance settings
            link.Color = Color.Blue;
            link.Border = new Border(link);

            // Add the annotation to the page
            page.Annotations.Add(link);

            doc.Save("output.pdf");
        }
    }
}
