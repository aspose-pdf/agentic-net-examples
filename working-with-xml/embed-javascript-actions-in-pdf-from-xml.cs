using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // For JavascriptAction
using Aspose.Pdf.Text;          // For XmlLoadOptions (inherits from LoadOptions)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlInputPath  = "input.xml";
        const string pdfOutputPath = "output.pdf";

        // Verify the XML source exists
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlInputPath}");
            return;
        }

        try
        {
            // Load the XML and convert it to a PDF document.
            // XmlLoadOptions tells Aspose.Pdf to treat the source as XML.
            using (Document pdfDoc = new Document(xmlInputPath, new XmlLoadOptions()))
            {
                // -----------------------------------------------------------------
                // Embed a document‑level JavaScript action that runs when the PDF is opened.
                // The action shows a simple alert; replace with any validation script.
                // -----------------------------------------------------------------
                pdfDoc.OpenAction = new JavascriptAction("app.alert('Document opened – ready for validation.');");

                // -----------------------------------------------------------------
                // Optionally add page‑level JavaScript actions.
                // Here we add an OnOpen script to the first page and an OnClose script to the last page.
                // -----------------------------------------------------------------
                if (pdfDoc.Pages.Count >= 1)
                {
                    Page firstPage = pdfDoc.Pages[1]; // 1‑based indexing
                    firstPage.Actions.OnOpen = new JavascriptAction(
                        "app.alert('First page opened – you may initialize form fields here.');");
                }

                if (pdfDoc.Pages.Count >= 1)
                {
                    Page lastPage = pdfDoc.Pages[pdfDoc.Pages.Count];
                    lastPage.Actions.OnClose = new JavascriptAction(
                        "app.alert('Last page closed – perform final validation before saving.');");
                }

                // Save the resulting PDF with the embedded JavaScript.
                pdfDoc.Save(pdfOutputPath);
            }

            Console.WriteLine($"PDF generated with JavaScript actions: {pdfOutputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}