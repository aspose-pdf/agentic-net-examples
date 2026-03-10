using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Instantiate a new, empty PDF document.
        // The using block ensures deterministic disposal (see document-disposal-with-using rule).
        using (Document pdfDoc = new Document())
        {
            // The document is now ready for rendering, editing, or other manipulations.
            // Example: add a blank page to the document.
            pdfDoc.Pages.Add();

            // Optional: save the document to verify that it was created successfully.
            // Saving as PDF does not require explicit SaveOptions (see save-to-non-pdf-always-use-save-options rule).
            pdfDoc.Save("empty.pdf");
        }
    }
}