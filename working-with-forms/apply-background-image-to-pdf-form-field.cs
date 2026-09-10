using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing; // for Rectangle

class Program
{
    static void Main()
    {
        const string inputPdfPath = "form.pdf";
        const string outputPdfPath = "form_with_background.pdf";
        const string fieldName = "logoField"; // name of the form field to decorate
        const string imagePath = "logo.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Access the AcroForm object
            Form form = doc.Form;

            // ------------------------------------------------------------
            // 1. If the document contains an XFA form, use XFA.SetFieldImage
            // ------------------------------------------------------------
            if (form.HasXfa)
            {
                // Set the background image for the specified XFA field
                using (FileStream imgStream = File.OpenRead(imagePath))
                {
                    form.XFA.SetFieldImage(fieldName, imgStream);
                }
            }
            else
            {
                // ------------------------------------------------------------
                // 2. For a standard AcroForm field, add a background artifact
                // ------------------------------------------------------------
                // Retrieve the field (example assumes a TextBoxField)
                var field = form[fieldName] as TextBoxField;
                if (field != null)
                {
                    // Create a background artifact and assign the image
                    BackgroundArtifact bgArtifact = new BackgroundArtifact();
                    using (FileStream imgStream = File.OpenRead(imagePath))
                    {
                        bgArtifact.SetImage(imgStream);
                    }

                    // Obtain the rectangle that defines the field's location
                    Aspose.Pdf.Rectangle fieldRect = field.Rect;

                    // Add the artifact to the page that contains the field.
                    // For simplicity we use the first page; adjust as needed.
                    Page page = doc.Pages[1];
                    page.Artifacts.Add(bgArtifact);

                    // Ensure the field itself is rendered on top of the background.
                    // This adds an additional appearance of the field at the same rectangle.
                    form.AddFieldAppearance(field, 1, fieldRect);
                }
                else
                {
                    Console.Error.WriteLine($"Field '{fieldName}' not found or not a TextBoxField.");
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with background image applied to field: {outputPdfPath}");
    }
}