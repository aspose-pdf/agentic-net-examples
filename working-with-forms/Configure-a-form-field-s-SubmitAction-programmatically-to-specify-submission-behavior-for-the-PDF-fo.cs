using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the output PDF and the submit URL
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string submitButtonName = "btnSubmit";
        const string submitUrl = "https://example.com/submit";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Create a FormEditor facade and bind it to the loaded document
            using (Aspose.Pdf.Facades.FormEditor formEditor = new Aspose.Pdf.Facades.FormEditor())
            {
                formEditor.BindPdf(pdfDoc);

                // Set the URL that the submit button will post to
                bool urlSet = formEditor.SetSubmitUrl(submitButtonName, submitUrl);
                if (!urlSet)
                {
                    Console.Error.WriteLine($"Failed to set URL for button '{submitButtonName}'.");
                }

                // Configure the submit behavior.
                // Example: submit the whole PDF (SubmitFormFlag.Pdf) and use HTTP GET (SubmitFormFlag.Html)
                // Flags can be combined using bitwise OR.
                Aspose.Pdf.Facades.SubmitFormFlag flags = Aspose.Pdf.Facades.SubmitFormFlag.Pdf |
                                                          Aspose.Pdf.Facades.SubmitFormFlag.Html;
                bool flagSet = formEditor.SetSubmitFlag(submitButtonName, flags);
                if (!flagSet)
                {
                    Console.Error.WriteLine($"Failed to set submit flags for button '{submitButtonName}'.");
                }

                // Save the modified PDF to the output path
                formEditor.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF saved with configured submit action: {outputPdfPath}");
    }
}