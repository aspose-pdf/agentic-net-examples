using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;               // Facade for Export/Import JSON
using Aspose.Pdf.Forms;                 // Form field classes (e.g., TextBoxField)

// Alias the two "Form" classes to avoid ambiguity.
using FacadeForm = Aspose.Pdf.Facades.Form;
using PdfForm = Aspose.Pdf.Forms.Form;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the destination PDF
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Ensure the input PDF exists – create a minimal PDF with a form field if it does not.
        if (!File.Exists(inputPdfPath))
        {
            CreateSamplePdfWithForm(inputPdfPath);
            Console.WriteLine($"Sample PDF created at '{inputPdfPath}'.");
        }

        // Memory stream that will hold the exported JSON data
        using (MemoryStream jsonStream = new MemoryStream())
        {
            // ---------- Export form fields to JSON ----------
            // Load the PDF into a Form facade and export all AcroForm fields to the stream.
            using (FacadeForm formExporter = new FacadeForm(inputPdfPath))
            {
                // ExportJson writes the JSON representation of the form fields.
                // The second argument 'true' enables pretty‑printed (indented) output.
                formExporter.ExportJson(jsonStream, true);
            }

            // Reset the stream position so it can be read from the beginning.
            jsonStream.Position = 0;

            // ---------- Import JSON data into a PDF ----------
            // Load the same (or another) PDF into a Form facade for importing.
            using (FacadeForm formImporter = new FacadeForm(inputPdfPath))
            {
                // ImportJson reads the JSON data from the stream and fills matching fields.
                formImporter.ImportJson(jsonStream);

                // Save the modified PDF to the output path.
                formImporter.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Form fields exported to JSON (in memory) and imported back. Output saved to '{outputPdfPath}'.");
    }

    /// <summary>
    /// Creates a very simple PDF containing a single text box form field.
    /// This method is used only when the expected input PDF is missing.
    /// </summary>
    private static void CreateSamplePdfWithForm(string path)
    {
        // Create a new PDF document.
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Define a rectangle for the text box field (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y).
        var fieldRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);
        TextBoxField textBox = new TextBoxField(page, fieldRect)
        {
            PartialName = "SampleField",
            Value = "Initial value"
        };

        // Add the field to the form collection.
        doc.Form.Add(textBox, 1);

        // Save the PDF to the specified path.
        doc.Save(path);
    }
}
