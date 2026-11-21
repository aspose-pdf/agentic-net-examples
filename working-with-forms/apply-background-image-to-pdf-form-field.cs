using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        const string outputPdf = "form_with_background.pdf";
        const string imagePath = "logo.png";
        const string fieldName = "myButton";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Verify that the form contains the target field
                if (!doc.Form.HasField(fieldName))
                {
                    Console.Error.WriteLine($"Form field '{fieldName}' does not exist.");
                    return;
                }

                // Retrieve the field and cast to ButtonField (push‑button style)
                ButtonField button = doc.Form[fieldName] as ButtonField;
                if (button == null)
                {
                    Console.Error.WriteLine($"Field '{fieldName}' is not a button field.");
                    return;
                }

                // Hide the button border so only the image is visible
                button.Border = new Border(button) { Width = 0 };

                // Determine the page that contains the button field.
                // ButtonField exposes PageIndex (0‑based). Convert to 1‑based for the Pages collection.
                int pageNumber = button.PageIndex + 1;
                Page page = doc.Pages[pageNumber];

                // Create an ImageStamp that matches the button rectangle.
                var rect = button.Rect;
                var imgStamp = new ImageStamp(imagePath)
                {
                    Width = rect.URX - rect.LLX,
                    Height = rect.URY - rect.LLY,
                    XIndent = rect.LLX,
                    YIndent = rect.LLY
                };

                // Add the stamp to the page – this becomes the visual background of the button.
                page.AddStamp(imgStamp);

                // Save the modified PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Background image applied and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
