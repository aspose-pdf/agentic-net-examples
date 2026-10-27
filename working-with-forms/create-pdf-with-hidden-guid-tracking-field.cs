using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "tracking.pdf";

        // Generate a GUID at runtime for tracking
        string guid = Guid.NewGuid().ToString();

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to host the form field
            Page page = doc.Pages.Add();

            // Define the field rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Rectangle rect = new Rectangle(100, 500, 300, 550);

            // Create a hidden text box field and assign the GUID as its value
            TextBoxField hiddenField = new TextBoxField(page, rect)
            {
                PartialName = "TrackingId",
                Value = guid
            };

            // Hide the field by setting the widget annotation flag to Hidden
            hiddenField.Flags = AnnotationFlags.Hidden;

            // Add the field to the document's form collection
            doc.Form.Add(hiddenField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden GUID field saved to '{outputPath}'.");
    }
}
