using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    /// <summary>
    /// Provides functionality to fill AcroForm fields in a PDF document using Aspose.Pdf.Facades.Form.
    /// </summary>
    public static class PdfFormFiller
    {
        /// <summary>
        /// Fills AcroForm fields in a PDF document using Aspose.Pdf.Facades.Form and saves the result.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF containing form fields.</param>
        /// <param name="fieldValues">Dictionary where key = fully‑qualified field name, value = value to set.</param>
        /// <param name="outputPdfPath">Path where the filled PDF will be saved.</param>
        public static void FillPdfForm(string inputPdfPath, IDictionary<string, string> fieldValues, string outputPdfPath)
        {
            if (string.IsNullOrEmpty(inputPdfPath))
                throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));

            if (string.IsNullOrEmpty(outputPdfPath))
                throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));

            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

            // The Form class implements SaveableFacade which is IDisposable.
            // Use a using block to ensure proper resource cleanup.
            using (Form form = new Form(inputPdfPath))
            {
                // Iterate over the supplied field/value pairs and fill each field.
                foreach (KeyValuePair<string, string> kvp in fieldValues)
                {
                    // FillField(string fieldName, string fieldValue) returns a bool indicating success.
                    // Ignoring the return value here; in production code you might log failures.
                    form.FillField(kvp.Key, kvp.Value);
                }

                // Save the modified document to the specified output path.
                form.Save(outputPdfPath);
            }
        }
    }

    /// <summary>
    /// Simple console entry point required for a buildable executable.
    /// Accepts input PDF, output PDF and optional field=value pairs.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // Expected usage: <exe> <inputPdfPath> <outputPdfPath> [field=value ...]
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: AsposePdfApi <inputPdfPath> <outputPdfPath> [field=value ...]");
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
                else
                {
                    Console.WriteLine($"Ignoring invalid argument '{args[i]}'. Expected format field=value.");
                }
            }

            try
            {
                PdfFormFiller.FillPdfForm(inputPath, fieldValues, outputPath);
                Console.WriteLine($"PDF form filled successfully. Saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error filling PDF form: {ex.Message}");
            }
        }
    }
}
