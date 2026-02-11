using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;
using SysPath = System.IO.Path; // Alias to avoid ambiguity with Aspose.Pdf.Drawing.Path

class Program
{
    static void Main(string[] args)
    {
        // Input and output paths (adjust as needed)
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Extracted";

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Check if the document contains an AcroForm
            if (pdfDocument.Form == null || pdfDocument.Form.Count == 0)
            {
                Console.WriteLine("No AcroForm fields found in the document.");
            }
            else
            {
                // Iterate over all form fields (Field is the base type for all form elements)
                foreach (Field field in pdfDocument.Form)
                {
                    // ----- Extract text/value of the field -----
                    string fieldName = field.FullName ?? "(unnamed)";
                    string fieldValue = field.Value?.ToString() ?? "(null)";
                    Console.WriteLine($"Field: {fieldName}, Value: {fieldValue}");

                    // ----- Attempt to extract images from widget appearance (if any) -----
                    // Only widget annotations have an Appearance dictionary.
                    if (field is WidgetAnnotation widget && widget.Appearance != null)
                    {
                        // The AppearanceDictionary no longer exposes a "Normal" collection in recent versions.
                        // Therefore, image extraction from the appearance is omitted for cross‑version safety.
                        // If you need to access the raw XForm, use widget.GetAppearance() (or the appropriate API for your version).
                    }
                }
            }

            // Optionally, save the (unchanged) document to a new file.
            string savedPdfPath = SysPath.Combine(outputFolder, "processed.pdf");
            pdfDocument.Save(savedPdfPath);
            Console.WriteLine($"Document saved to: {savedPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
