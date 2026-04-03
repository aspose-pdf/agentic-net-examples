using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms; // for creating a simple form when the source file is missing

class Program
{
    static void Main()
    {
        const string inputPdf  = "input_form.pdf";
        const string outputPdf = "output_form_zoomed.pdf";

        // ------------------------------------------------------------
        // Ensure the source PDF exists. If it does not, create a minimal
        // PDF with a single page (and optionally a form field) so the
        // rest of the example can run without a FileNotFoundException.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            // Create a new blank document
            Document tempDoc = new Document();
            Page page = tempDoc.Pages.Add();

            // (Optional) add a simple text box form field so the file really
            // looks like a form PDF – this mirrors the original intent.
            TextBoxField txt = new TextBoxField(page, new Rectangle(100, 600, 200, 620))
            {
                PartialName = "SampleField",
                Value = "Demo"
            };
            tempDoc.Form.Add(txt);

            // Save the temporary PDF that will be used as input
            tempDoc.Save(inputPdf);
            Console.WriteLine($"Created placeholder PDF '{inputPdf}' because it was missing.");
        }

        // ------------------------------------------------------------
        // Load the PDF with FormEditor (facade API) and set the opening
        // zoom level using a GoTo action with an XYZ destination.
        // ------------------------------------------------------------
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);

            // Access the underlying Document object
            Document doc = formEditor.Document;

            // Ensure there is at least one page
            if (doc.Pages.Count > 0)
            {
                // Choose the first page as the target for the opening action
                Page firstPage = doc.Pages[1];

                // Create a destination that sets the zoom factor (e.g., 150%)
                // left = 0, top = 0 positions the view at the upper‑left corner
                XYZExplicitDestination zoomDest = new XYZExplicitDestination(firstPage, 0, 0, 1.5);

                // Build a GoToAction that uses the destination
                GoToAction openAction = new GoToAction(firstPage)
                {
                    Destination = zoomDest
                };

                // Assign the action to be performed when the document is opened
                doc.OpenAction = openAction;
            }
            else
            {
                Console.WriteLine("The PDF does not contain any pages – no zoom action applied.");
            }

            // Save the modified PDF (FormEditor handles the save operation)
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with opening zoom level to '{outputPdf}'.");
    }
}
