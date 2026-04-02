using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create the target PDF with two pages
        using (Document targetDoc = new Document())
        {
            // Add first page
            targetDoc.Pages.Add();
            // Add second page
            targetDoc.Pages.Add();

            // Create an external PDF that contains a single page with a different size and rotation
            using (Document externalDoc = new Document())
            {
                // Add a page to the external document
                Page extPage = externalDoc.Pages.Add();
                // Set custom size (A5) and rotate the page 90 degrees
                extPage.PageInfo.Width = 420f;
                extPage.PageInfo.Height = 595f;
                // Use the Rotate property of Page and the correct enum value
                extPage.Rotate = Rotation.on90;

                // Save the external PDF to a temporary file
                externalDoc.Save("external.pdf");
            }

            // Load the external PDF and obtain the page to be inserted
            using (Document externalDoc = new Document("external.pdf"))
            {
                Page pageToInsert = externalDoc.Pages[1];
                // Insert the page at index 2 (after the first page) preserving its size and rotation
                targetDoc.Pages.Insert(2, pageToInsert);
            }

            // Save the resulting PDF
            targetDoc.Save("output.pdf");
        }
    }
}