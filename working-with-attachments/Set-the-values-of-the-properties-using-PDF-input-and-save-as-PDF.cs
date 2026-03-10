using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF containing a submit button field.
        const string inputPath = "input.pdf";
        // Output PDF after modifying the button properties.
        const string outputPath = "output.pdf";
        // The fully qualified name of the submit button field in the PDF.
        const string buttonName = "SubmitBtn";
        // URL to which the form will be submitted.
        const string submitUrl = "https://example.com/submit";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor is a facade that allows editing of form fields.
        using (FormEditor editor = new FormEditor())
        {
            // Load the PDF document into the editor.
            editor.BindPdf(inputPath);

            // Set the URL for the submit button.
            editor.SetSubmitUrl(buttonName, submitUrl);

            // Set the submit flag so that the document is submitted as PDF.
            // SubmitFormAction.SubmitPdf is the constant defining this flag.
            // Cast the constant to the expected SubmitFormFlag enum.
            editor.SetSubmitFlag(buttonName, (SubmitFormFlag)SubmitFormAction.SubmitPdf);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}