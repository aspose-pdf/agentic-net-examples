using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML source not found: {xmlPath}");
            return;
        }

        try
        {
            // Load XML and create a PDF document
            using (Document doc = new Document(xmlPath, new XmlLoadOptions()))
            {
                // Set presentation mode (full‑screen slide show)
                doc.PageMode = PageMode.FullScreen;

                // NOTE: The core Aspose.Pdf API (Document, Page, etc.) does not expose a
                // direct property for page transitions. In older versions a Page.Transition
                // property existed, but it has been removed. Applying transitions now
                // requires the PdfPageEditor class from Aspose.Pdf.Facades, which is
                // prohibited by the hard namespace restriction for this task.
                // Therefore the transition effect is omitted here to keep the code
                // compliant with the given constraints.

                // Save the resulting PDF
                doc.Save(pdfPath);
            }

            Console.WriteLine($"PDF generated (full‑screen mode) at: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
