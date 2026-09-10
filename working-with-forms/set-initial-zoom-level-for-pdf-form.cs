using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // For GoToAction and XYZExplicitDestination

class Program
{
    static void Main()
    {
        const string inputPdf  = "form_input.pdf";   // Path to the source PDF form
        const string outputPdf = "form_with_zoom.pdf"; // Path for the output PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document (which may contain a form)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF does not contain any pages.");
                return;
            }

            // Choose the page that will be displayed when the PDF is opened.
            // Typically the first page is used.
            Page firstPage = doc.Pages[1];

            // Desired initial zoom factor (e.g., 150% = 1.5)
            double zoomFactor = 1.5;

            // Create an explicit XYZ destination:
            // left = 0 (horizontal start), top = page height (vertical start from top),
            // zoom = desired factor.
            XYZExplicitDestination destination = new XYZExplicitDestination(
                firstPage,
                left: 0,
                top: firstPage.PageInfo.Height,
                zoom: zoomFactor);

            // Set the document's OpenAction to navigate to the destination with the specified zoom.
            doc.OpenAction = new GoToAction(destination);

            // Optional: make the viewer window fit the page size.
            doc.FitWindow = true;

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with initial zoom set to {1.5 * 100}% at '{outputPdf}'.");
    }
}