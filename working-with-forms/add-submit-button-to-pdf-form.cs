using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string submitUrl = "https://example.com/submit";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Access the AcroForm; it is created automatically if it does not exist.
                Form pdfForm = doc.Form;

                // Define button rectangle (llx, lly, urx, ury) in points.
                var btnRect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

                // Create a button field on the first page.
                ButtonField submitBtn = new ButtonField(doc.Pages[1], btnRect);
                submitBtn.PartialName = "SubmitButton";          // field name
                submitBtn.AlternateName = "Submit form data";   // tooltip / alt text
                submitBtn.Value = "Submit";                     // visible caption

                // Create a SubmitFormAction and assign the target URL.
                SubmitFormAction submitAction = new SubmitFormAction();
                submitAction.Url = new FileSpecification(submitUrl);
                submitBtn.OnActivated = submitAction;            // attach action to button

                // Add the button to the form on page 1.
                pdfForm.Add(submitBtn, 1);

                // Save the modified PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
