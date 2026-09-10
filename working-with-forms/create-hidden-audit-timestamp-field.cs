using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "audit_timestamp.pdf";

        using (Document doc = new Document())
        {
            // Add a blank page (required for placing the field)
            Page page = doc.Pages.Add();

            // Zero‑size rectangle – field will be hidden
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create a DateField on the page
            DateField timestampField = new DateField(page, rect)
            {
                PartialName = "CreationTimestamp",
                Value = DateTime.Now,
                Flags = AnnotationFlags.Hidden
            };

            // Add the field to the document's form
            doc.Form.Add(timestampField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden audit timestamp saved to '{outputPath}'.");
    }
}
