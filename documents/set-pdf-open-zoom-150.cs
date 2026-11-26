using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (required because the destination must reference a page)
            doc.Pages.Add();

            // Define a destination on page 1 with a zoom factor of 1.5 (no offset)
            // Use the XYZExplicitDestination constructor that takes a page number.
            XYZExplicitDestination destination = new XYZExplicitDestination(
                pageNumber: 1,   // 1‑based page index
                left: 0,         // X coordinate (0 = left edge)
                top: 0,          // Y coordinate (0 = top edge)
                zoom: 1.5        // Desired zoom factor
            );

            // Create a GoToAction that points to the defined destination
            GoToAction openAction = new GoToAction(destination);

            // Set the document's OpenAction so the PDF opens with the specified zoom
            doc.OpenAction = openAction;

            // Save the resulting PDF
            doc.Save("Zoomed1_5.pdf");
        }
    }
}