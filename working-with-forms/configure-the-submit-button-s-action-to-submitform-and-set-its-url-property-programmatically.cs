using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing the submit button
        const string outputPdf = "output.pdf";         // destination PDF
        const string buttonName = "btnSubmit";         // exact name of the submit button field
        const string submitUrl = "https://www.example.com/submit"; // URL to be assigned

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the FormEditor facade
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPdf);

            // Set the URL for the submit button (SubmitForm action)
            bool urlSet = editor.SetSubmitUrl(buttonName, submitUrl);
            if (!urlSet)
            {
                Console.Error.WriteLine($"Failed to set URL for button '{buttonName}'.");
            }

            // Optionally define the submit format (e.g., HTML form submission)
            editor.SetSubmitFlag(buttonName, SubmitFormFlag.Html);

            // Persist the changes
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Submit button '{buttonName}' configured with URL '{submitUrl}'. Saved to '{outputPdf}'.");
    }
}