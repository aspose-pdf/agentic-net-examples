using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the resulting PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Submit button properties
        const string fieldName = "submitBtn";               // Unique name of the button field
        const string label     = "Submit";                  // Caption displayed on the button
        const string url       = "https://www.example.com/submit"; // URL to which the form will be submitted

        // Button rectangle (lower‑left and upper‑right coordinates) on page 1
        const int    pageNumber = 1; // Aspose.Pdf uses 1‑based page indexing
        const float  llx = 100f;    // lower‑left X
        const float  lly = 200f;    // lower‑left Y
        const float  urx = 200f;    // upper‑right X
        const float  ury = 250f;    // upper‑right Y

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor is a facade for editing AcroForm fields.
        // It implements IDisposable, so we wrap it in a using block.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF document into the facade
            formEditor.BindPdf(inputPdf);

            // Add a submit button with the specified properties.
            // Method signature:
            // AddSubmitBtn(string fieldName, int page, string label,
            //              string url, float llx, float lly, float urx, float ury)
            formEditor.AddSubmitBtn(fieldName, pageNumber, label, url,
                                    llx, lly, urx, ury);

            // Persist the changes to a new file.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Submit button added and saved to '{outputPdf}'.");
    }
}