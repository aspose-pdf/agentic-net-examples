using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF that contains form fields
        const string inputPdf = "input.pdf";

        // JSON file with form field values to import
        const string jsonData = "formData.json";

        // Output PDF after processing
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(jsonData))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonData}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Access the Form object (represents the interactive form)
            Form pdfForm = doc.Form;

            // Example: disable automatic recalculation for performance when filling many fields
            pdfForm.AutoRecalculate = false;

            // Import form field values from a JSON file
            // The method returns a collection of results, which we ignore here
            pdfForm.ImportFromJson(jsonData);

            // Re‑enable automatic recalculation if further changes are expected
            pdfForm.AutoRecalculate = true;

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Form data imported and PDF saved to '{outputPdf}'.");
    }
}