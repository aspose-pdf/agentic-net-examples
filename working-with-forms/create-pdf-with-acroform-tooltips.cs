using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithTooltips.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // ---------- First Name field ----------
            // Define the field rectangle (lower‑left X/Y, upper‑right X/Y)
            Aspose.Pdf.Rectangle firstNameRect = new Aspose.Pdf.Rectangle(50, 750, 250, 770);
            // Create a text box field on the document with the rectangle
            TextBoxField firstNameField = new TextBoxField(doc, firstNameRect);
            firstNameField.Name = "FirstName";                     // field name (used in scripts, extraction, etc.)
            firstNameField.AlternateName = "Enter your first name"; // tooltip shown in Acrobat
            firstNameField.Value = "";                             // initial value (empty)
            // Add the field to the form on page 1
            doc.Form.Add(firstNameField, 1);

            // ---------- Last Name field ----------
            Aspose.Pdf.Rectangle lastNameRect = new Aspose.Pdf.Rectangle(50, 720, 250, 740);
            TextBoxField lastNameField = new TextBoxField(doc, lastNameRect);
            lastNameField.Name = "LastName";
            lastNameField.AlternateName = "Enter your last name";
            lastNameField.Value = "";
            doc.Form.Add(lastNameField, 1);

            // ---------- Email field ----------
            Aspose.Pdf.Rectangle emailRect = new Aspose.Pdf.Rectangle(50, 690, 250, 710);
            TextBoxField emailField = new TextBoxField(doc, emailRect);
            emailField.Name = "Email";
            emailField.AlternateName = "Enter your email address";
            emailField.Value = "";
            doc.Form.Add(emailField, 1);

            // ---------- Age field (numeric) ----------
            Aspose.Pdf.Rectangle ageRect = new Aspose.Pdf.Rectangle(50, 660, 150, 680);
            NumberField ageField = new NumberField(doc, ageRect);
            ageField.Name = "Age";
            ageField.AlternateName = "Enter your age (numeric)";
            ageField.Value = "";
            doc.Form.Add(ageField, 1);

            // Save the PDF (no extra SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}