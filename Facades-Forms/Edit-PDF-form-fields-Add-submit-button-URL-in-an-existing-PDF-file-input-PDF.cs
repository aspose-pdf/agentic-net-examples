using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Submit button configuration
        const string buttonName = "SubmitBtn";
        const int pageNumber = 1;                     // 1‑based page index
        const float llx = 100f;                       // lower‑left X
        const float lly = 100f;                       // lower‑left Y
        const float urx = 200f;                       // upper‑right X
        const float ury = 150f;                       // upper‑right Y
        const string submitUrl = "https://example.com/submit";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Initialize the FormEditor facade and bind the existing PDF
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(inputPdfPath);

                // Add a submit button to the specified page and rectangle
                // Parameters: field name, page number, button caption, URL, rectangle coordinates
                formEditor.AddSubmitBtn(buttonName, pageNumber, buttonName, submitUrl, llx, lly, urx, ury);

                // Ensure the button's URL is set (optional if already provided above)
                formEditor.SetSubmitUrl(buttonName, submitUrl);

                // Save the modified PDF to the output file
                formEditor.Save(outputPdfPath);
            }

            Console.WriteLine($"Submit button added successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}