using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;          // Required for XmlLoadOptions (inherits from LoadOptions)
using Aspose.Pdf.Annotations;   // Required for JavascriptAction

// Example: Convert an XML file to PDF and apply page transition effects for presentation mode.
class Program
{
    static void Main()
    {
        // Paths to the source XML and the resulting PDF.
        const string xmlInputPath  = "input.xml";
        const string pdfOutputPath = "output.pdf";

        // Verify that the XML file exists.
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"Error: XML file not found – {xmlInputPath}");
            return;
        }

        // Load the XML using XmlLoadOptions (no XSL transformation in this example).
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Create the PDF document from the XML.
        using (Document pdfDoc = new Document(xmlInputPath, loadOptions))
        {
            // Apply a transition effect to each page using JavaScript actions.
            // This approach works even when the Page.Transition property is not available in the used library version.
            const string transitionJs = "this.transition = 'Fade'; this.transitionDuration = 1; this.duration = 5;";

            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Attach a JavaScript action that sets the transition and display duration when the page opens.
                page.Actions.OnOpen = new JavascriptAction(transitionJs);
            }

            // Save the resulting PDF with the applied transitions.
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF generated with page transitions: {pdfOutputPath}");
    }
}
