using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing a submit button
        const string outputPdf = "output.pdf";         // PDF after URL configuration
        const string buttonName = "SubmitBtn";         // exact name of the submit button field
        const string submitUrl = "https://example.com/submit"; // target URL

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor is a disposable facade; wrap it in a using block.
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document.
            formEditor.BindPdf(inputPdf);

            // Set the URL for the submit button.
            // Returns true if the button was found and the URL was applied.
            bool success = formEditor.SetSubmitUrl(buttonName, submitUrl);
            if (!success)
            {
                Console.Error.WriteLine($"Submit button '{buttonName}' not found or URL could not be set.");
                return;
            }

            // Save the modified PDF.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Submit button URL configured and saved to '{outputPdf}'.");
    }
}