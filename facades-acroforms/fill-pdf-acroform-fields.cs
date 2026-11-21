using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace AsposePdfApi
{
    public static class PdfFormHelper
    {
        /// <summary>
        /// Fills the form fields of a PDF using Aspose.Pdf.Document.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF containing form fields.</param>
        /// <param name="fieldValues">Dictionary where key = field name, value = text to set.</param>
        /// <param name="outputPdfPath">Path where the filled PDF will be saved.</param>
        public static void FillPdfFields(string inputPdfPath, Dictionary<string, string> fieldValues, string outputPdfPath)
        {
            if (string.IsNullOrWhiteSpace(inputPdfPath))
                throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));

            if (string.IsNullOrWhiteSpace(outputPdfPath))
                throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));

            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException("Input PDF not found.", inputPdfPath);

            // Load the PDF document.
            Document pdfDocument = new Document(inputPdfPath);

            // Ensure the document actually contains a form.
            if (pdfDocument.Form == null || pdfDocument.Form.Count == 0)
                throw new InvalidOperationException("The PDF does not contain any form fields.");

            // Fill each field supplied in the dictionary.
            foreach (KeyValuePair<string, string> kvp in fieldValues)
            {
                // Use HasField to check existence – Form does not have Contains.
                if (pdfDocument.Form.HasField(kvp.Key))
                {
                    // The indexer returns a WidgetAnnotation; cast to Field to access Value.
                    Field field = pdfDocument.Form[kvp.Key] as Field;
                    if (field != null)
                    {
                        field.Value = kvp.Value;
                    }
                }
                else
                {
                    // Field not found – ignore or log as needed.
                    // throw new KeyNotFoundException($"Field '{kvp.Key}' not found in the PDF form.");
                }
            }

            // Save the modified PDF to the specified output path.
            pdfDocument.Save(outputPdfPath);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments: <inputPdfPath> <outputPdfPath>
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: AsposePdfApi <inputPdfPath> <outputPdfPath>");
                return;
            }

            string inputPdfPath = args[0];
            string outputPdfPath = args[1];

            // Example field values – replace with real data as needed.
            var fieldValues = new Dictionary<string, string>
            {
                { "FirstName", "John" },
                { "LastName", "Doe" }
            };

            try
            {
                PdfFormHelper.FillPdfFields(inputPdfPath, fieldValues, outputPdfPath);
                Console.WriteLine($"PDF successfully filled and saved to '{outputPdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
