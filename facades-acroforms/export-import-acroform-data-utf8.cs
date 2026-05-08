using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // required for form field classes if needed

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input_form.pdf";
        const string outputPdfPath = "output_form.pdf";
        const string jsonPath = "form_data.json";
        const string xmlPath = "form_data.xml";

        // ------------------------------------------------------------
        // Ensure a source PDF exists – create a minimal one with a form
        // field if the file is missing. This prevents the runtime
        // FileNotFoundException shown in the original build output.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            // Create a new PDF document with a single page
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();

                // Add a simple text box form field that will hold Unicode text
                TextBoxField txtField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 600, 300, 620))
                {
                    PartialName = "UnicodeField",
                    Value = "Привет, 世界" // sample Unicode value
                };
                doc.Form.Add(txtField);
                doc.Save(inputPdfPath);
            }
        }

        // Load the PDF that contains form fields
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the Form facade on the loaded document (fully qualified to avoid ambiguity)
            Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(pdfDoc);

            // --------------------------------------------------------
            // Export form data to JSON using UTF‑8 encoding explicitly
            // --------------------------------------------------------
            using (MemoryStream jsonMs = new MemoryStream())
            {
                // ExportJson writes raw bytes; we capture them in a MemoryStream
                form.ExportJson(jsonMs, false);
                // Convert the bytes to a UTF‑8 string and write to file
                string jsonString = Encoding.UTF8.GetString(jsonMs.ToArray());
                File.WriteAllText(jsonPath, jsonString, Encoding.UTF8);
            }

            // --------------------------------------------------------
            // Export form data to XML using UTF‑8 encoding explicitly
            // --------------------------------------------------------
            using (MemoryStream xmlMs = new MemoryStream())
            {
                form.ExportXml(xmlMs);
                string xmlString = Encoding.UTF8.GetString(xmlMs.ToArray());
                File.WriteAllText(xmlPath, xmlString, Encoding.UTF8);
            }

            // --------------------------------------------------------
            // Import form data from JSON (UTF‑8)
            // --------------------------------------------------------
            string jsonInput = File.ReadAllText(jsonPath, Encoding.UTF8);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonInput);
            using (MemoryStream jsonInMs = new MemoryStream(jsonBytes))
            {
                form.ImportJson(jsonInMs);
            }

            // --------------------------------------------------------
            // Import form data from XML (UTF‑8)
            // --------------------------------------------------------
            string xmlInput = File.ReadAllText(xmlPath, Encoding.UTF8);
            byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlInput);
            using (MemoryStream xmlInMs = new MemoryStream(xmlBytes))
            {
                form.ImportXml(xmlInMs);
            }

            // Save the PDF with the imported values
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine("Form data exported, re‑imported, and PDF saved successfully.");
    }
}
