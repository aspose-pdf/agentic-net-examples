using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor with the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a submit button on page 1
                // Parameters: field name, page number, button caption, submit URL, llx, lly, urx, ury
                string buttonName = "SubmitBtn";
                formEditor.AddSubmitBtn(buttonName, 1, "Submit", "https://example.com/submit", 100, 100, 200, 150);

                // Attach JavaScript that shows a confirmation dialog before submitting
                string jsCode = "if(app.alert('Are you sure you want to submit?', 3) == 4) { this.submitForm(); }";
                formEditor.AddFieldScript(buttonName, jsCode);

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with confirmation dialog saved to '{outputPath}'.");
    }
}