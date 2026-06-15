using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a sample PDF file
        using (Document document = new Document())
        {
            document.Pages.Add();
            document.Save("input.pdf");
        }

        // Open the PDF and add JavaScript to hide toolbar and menu bar on open
        using (Document document = new Document("input.pdf"))
        {
            // Set document‑level JavaScript that runs when the document is opened
            document.OpenAction = new JavascriptAction("app.toolbar = false; app.menuBar = false;");

            // Set viewer preference properties (reinforces the behavior)
            document.HideToolBar = true;
            document.HideMenubar = true;

            document.Save("output.pdf");
        }
    }
}