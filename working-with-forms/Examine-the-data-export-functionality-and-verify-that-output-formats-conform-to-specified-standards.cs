using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class DataExportVerification
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Output files for validation log, XFDF, JSON, HTML, and XML
        const string validationLogPath = "validation.log";
        const string xfdfPath = "annotations.xfdf";
        const string jsonPath = "formfields.json";
        const string htmlPath = "output.html";
        const string xmlPath = "output.xml";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // 1. Validate the PDF against a specific PDF/A format.
            //    The Validate method returns true if the document conforms.
            // ------------------------------------------------------------
            bool isValid = doc.Validate(validationLogPath, PdfFormat.PDF_A_1B);
            Console.WriteLine($"Validation result: {(isValid ? "Valid" : "Invalid")} (log saved to {validationLogPath})");

            // ------------------------------------------------------------
            // 2. Export all annotations to XFDF.
            // ------------------------------------------------------------
            doc.ExportAnnotationsToXfdf(xfdfPath);
            Console.WriteLine($"Annotations exported to XFDF: {xfdfPath}");

            // ------------------------------------------------------------
            // 3. Export form fields to JSON with indentation and password export enabled.
            // ------------------------------------------------------------
            ExportFieldsToJsonOptions jsonOptions = new ExportFieldsToJsonOptions
            {
                ExportPasswordValue = true, // include password values if any
                WriteIndented = true        // produce pretty‑printed JSON
            };

            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                // Document.Form provides access to the PDF form.
                doc.Form.ExportToJson(jsonStream, jsonOptions);
            }
            Console.WriteLine($"Form fields exported to JSON: {jsonPath}");

            // ------------------------------------------------------------
            // 4. Save the document as HTML.
            //    HtmlSaveOptions must be supplied explicitly (extension alone is ignored).
            // ------------------------------------------------------------
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };
            doc.Save(htmlPath, htmlOptions);
            Console.WriteLine($"Document saved as HTML: {htmlPath}");

            // ------------------------------------------------------------
            // 5. Save the document as XML.
            //    XmlSaveOptions are also in the Aspose.Pdf namespace.
            // ------------------------------------------------------------
            XmlSaveOptions xmlOptions = new XmlSaveOptions();
            doc.Save(xmlPath, xmlOptions);
            Console.WriteLine($"Document saved as XML: {xmlPath}");
        }

        // All resources have been released at this point.
        Console.WriteLine("Data export verification completed successfully.");
    }
}