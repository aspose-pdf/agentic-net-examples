using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string buttonName = "SubmitBtn"; // name of the submit button in the PDF
        const string tooltip = "Please fill all required fields before submitting.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form associated with the document
            Form form = doc.Form;

            // Verify that the button field exists
            if (form.HasField(buttonName))
            {
                // Retrieve the field and cast it to ButtonField
                ButtonField submitButton = form[buttonName] as ButtonField;
                if (submitButton != null)
                {
                    // Set the tooltip (AlternateName) that appears in PDF viewers
                    submitButton.AlternateName = tooltip;
                }
                else
                {
                    Console.Error.WriteLine($"Field '{buttonName}' is not a button field.");
                }
            }
            else
            {
                Console.Error.WriteLine($"Button field '{buttonName}' not found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tooltip to '{outputPath}'.");
    }
}