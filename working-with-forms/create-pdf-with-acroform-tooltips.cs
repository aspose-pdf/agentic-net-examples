using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithTooltips.pdf";

        // Create a new PDF document and ensure it is disposed properly
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // ----- First Name field -----
            // Define the field rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle firstNameRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);
            // Create the text box field on the page
            TextBoxField firstNameField = new TextBoxField(page, firstNameRect);
            // Set the internal field name (used for form data)
            firstNameField.Name = "FirstName";
            // Set the tooltip that appears in PDF viewers
            firstNameField.AlternateName = "Enter your first name";
            // Add the field to the document's form (which internally adds the widget to the page)
            doc.Form.Add(firstNameField, 1);

            // ----- Last Name field -----
            Aspose.Pdf.Rectangle lastNameRect = new Aspose.Pdf.Rectangle(100, 560, 300, 590);
            TextBoxField lastNameField = new TextBoxField(page, lastNameRect);
            lastNameField.Name = "LastName";
            lastNameField.AlternateName = "Enter your last name";
            doc.Form.Add(lastNameField, 1);

            // Save the PDF to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields created: {outputPath}");
    }
}
