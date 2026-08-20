using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor with source and destination PDF files
        FormEditor formEditor = new FormEditor(inputPdf, outputPdf);

        // Add a submit button named "SubmitForm" on page 1.
        // Parameters: fieldName, page, label, url, llx, lly, urx, ury
        formEditor.AddSubmitBtn(
            "SubmitForm",          // field name
            1,                     // page number (1‑based)
            "Submit",              // button caption
            "https://api.example.com/submit", // target URL
            50, 750, 150, 800);   // rectangle coordinates (lower‑left x/y, upper‑right x/y)

        // Persist changes to the output PDF
        formEditor.Save();

        // Release resources
        formEditor.Close();

        Console.WriteLine($"Submit button added and saved to '{outputPdf}'.");
    }
}