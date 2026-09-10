using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

namespace PdfFormFillerApp
{
    /// <summary>
    /// Provides a helper method that fills AcroForm fields in a PDF using Aspose.Pdf.Facades.Form.
    /// </summary>
    public static class PdfFormFiller
    {
        /// <summary>
        /// Fills the specified fields and saves the result to a new PDF file.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF containing form fields.</param>
        /// <param name="fieldValues">Dictionary where key = fully qualified field name, value = value to set.</param>
        /// <param name="outputPdfPath">Path where the filled PDF will be saved.</param>
        public static void FillAndSave(string inputPdfPath, Dictionary<string, string> fieldValues, string outputPdfPath)
        {
            if (string.IsNullOrWhiteSpace(inputPdfPath))
                throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));

            if (string.IsNullOrWhiteSpace(outputPdfPath))
                throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));

            if (fieldValues == null)
                throw new ArgumentNullException(nameof(fieldValues));

            // Form implements IDisposable via SaveableFacade, so wrap it in a using block.
            using (Form form = new Form(inputPdfPath))
            {
                // Iterate over the supplied field/value pairs and fill each field.
                foreach (KeyValuePair<string, string> kvp in fieldValues)
                {
                    // FillField returns true if the field exists and was filled successfully.
                    // The return value is ignored here, but you could log failures if needed.
                    form.FillField(kvp.Key, kvp.Value);
                }

                // Save the modified document to the specified output path.
                form.Save(outputPdfPath);
            }
        }
    }

    /// <summary>
    /// Simple console entry point that demonstrates how to call PdfFormFiller.FillAndSave.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments: <inputPdfPath> <outputPdfPath> [fieldName=fieldValue ...]
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: PdfFormFillerApp <inputPdfPath> <outputPdfPath> [fieldName=fieldValue ...]");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            var fieldValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 2; i < args.Length; i++)
            {
                var parts = args[i].Split(new[] { '=' }, 2);
                if (parts.Length == 2)
                {
                    fieldValues[parts[0]] = parts[1];
                }
            }

            // If no field/value pairs were supplied, add a placeholder to avoid an empty dictionary.
            if (fieldValues.Count == 0)
            {
                fieldValues["SampleField"] = "SampleValue";
            }

            PdfFormFiller.FillAndSave(inputPath, fieldValues, outputPath);
            Console.WriteLine($"PDF form filled and saved to '{outputPath}'.");
        }
    }
}
