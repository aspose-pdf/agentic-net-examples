using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string submitUrl = "https://example.com/submit";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Bind a FormEditor to the document
            using (FormEditor editor = new FormEditor(doc))
            {
                // Add a submit button named "SubmitBtn" on page 1
                // Parameters: field name, page number, button label, URL, llx, lly, urx, ury
                editor.AddSubmitBtn("SubmitBtn", 1, "Submit", submitUrl, 100, 500, 200, 540);

                // Set the submit button to submit the whole PDF
                editor.SubmitFlag = Aspose.Pdf.Facades.SubmitFormFlag.Pdf;

                // Save the modified PDF
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPdf}'.");
    }
}