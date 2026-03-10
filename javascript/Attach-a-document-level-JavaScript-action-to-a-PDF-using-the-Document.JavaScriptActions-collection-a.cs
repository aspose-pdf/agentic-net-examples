using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "document_with_js.pdf";

        // Create a new PDF document and add a simple page
        using (Document doc = new Document())
        {
            // Add a blank page (required to have a document structure)
            doc.Pages.Add();

            // Attach a document‑level JavaScript action that runs when the document is opened.
            // The JavaScript collection (Document.JavaScript) is read‑only; use OpenAction instead.
            doc.OpenAction = new JavascriptAction("app.alert('Document opened');");

            // Save the PDF with the attached JavaScript.
            doc.Save(outputPath);
        }

        // Load the PDF that now contains the JavaScript action
        using (Document loadedDoc = new Document(outputPath))
        {
            // Verify that the JavaScript action is present via OpenAction
            if (loadedDoc.OpenAction is JavascriptAction jsAction)
            {
                Console.WriteLine("JavaScript action attached successfully.");
                Console.WriteLine($"Script: {jsAction.Script}");
            }
            else
            {
                Console.WriteLine("No JavaScript actions found.");
            }
        }
    }
}
