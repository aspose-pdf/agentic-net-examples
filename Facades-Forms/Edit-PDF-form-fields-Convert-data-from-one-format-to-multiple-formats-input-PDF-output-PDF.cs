using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_filled.pdf";
        const string fdfPath = "formData.fdf";
        const string xmlPath = "formData.xml";
        const string jsonPath = "formData.json";
        const string xfdfPath = "formData.xfdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // -------------------------------------------------
            // 1. Load the PDF form using the Form facade
            // -------------------------------------------------
            using (Form form = new Form(inputPdfPath))
            {
                // -------------------------------------------------
                // 2. Export form data to various formats
                // -------------------------------------------------
                // Export to FDF
                using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportFdf(fdfStream);
                }

                // Export to XML
                using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportXml(xmlStream);
                }

                // Export to JSON (second parameter: include empty fields)
                using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
                {
                    // The ExportJson overload does not support named arguments; pass the boolean directly.
                    form.ExportJson(jsonStream, true);
                }

                // Export to XFDF
                using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportXfdf(xfdfStream);
                }

                // -------------------------------------------------
                // 3. Demonstrate importing data back into a new PDF
                // -------------------------------------------------
                // Create a copy of the original PDF to work on
                File.Copy(inputPdfPath, outputPdfPath, overwrite: true);

                using (Form importForm = new Form(outputPdfPath))
                {
                    // Import data from one of the exported streams (e.g., XML)
                    using (FileStream importXml = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
                    {
                        importForm.ImportXml(importXml);
                    }

                    // Save the modified PDF
                    importForm.Save(outputPdfPath);
                }
            }

            Console.WriteLine("Form data exported to multiple formats and imported back successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
