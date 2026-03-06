using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF that already contains a submit button field (or any button field)
        const string inputPdf  = "input.pdf";
        // Name of the button field to which the submit action will be attached
        const string buttonName = "SubmitBtn";
        // URL that the button will submit the form to
        const string submitUrl = "https://www.example.com/submit";

        // Output PDF with the updated submit action
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use FormEditor (a Facades class) to modify the form field.
        // FormEditor implements IDisposable via SaveableFacade, so wrap it in a using block.
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document to the editor.
            formEditor.BindPdf(inputPdf);

            // Set the submit URL for the specified button field.
            // Returns true if the field was found and the URL was set.
            bool success = formEditor.SetSubmitUrl(buttonName, submitUrl);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to set submit URL. Field \"{buttonName}\" not found.");
                return;
            }

            // Save the modified PDF to a new file.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Submit action set successfully. Output saved to '{outputPdf}'.");
    }
}