using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

public class PdfFormFiller
{
    /// <summary>
    /// Fills the form fields of a PDF and saves the result.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF that contains form fields.</param>
    /// <param name="fieldValues">Dictionary where the key is the field name and the value is the text to set.</param>
    /// <param name="outputPdfPath">Path where the filled PDF will be written.</param>
    public static void FillPdfForm(string inputPdfPath, IDictionary<string, string> fieldValues, string outputPdfPath)
    {
        // Validate input arguments
        if (string.IsNullOrWhiteSpace(inputPdfPath))
            throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"The source PDF file was not found: '{inputPdfPath}'.", inputPdfPath);
        if (fieldValues == null)
            throw new ArgumentNullException(nameof(fieldValues));
        if (string.IsNullOrWhiteSpace(outputPdfPath))
            throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));

        // Use the non‑obsolete constructor that only takes the source file.
        // The Form facade implements IDisposable, so wrap it in a using block.
        using (Form form = new Form(inputPdfPath))
        {
            // Populate each field with the supplied value.
            foreach (KeyValuePair<string, string> kvp in fieldValues)
            {
                form.FillField(kvp.Key, kvp.Value);
            }

            // Ensure the directory for the output file exists.
            string outputDir = Path.GetDirectoryName(outputPdfPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            // Save the updated PDF to the destination path using the non‑obsolete overload.
            form.Save(outputPdfPath);
        }
    }

    // Example usage
    public static void Main()
    {
        string inputPath = "input.pdf";   // Ensure this file exists in the working directory or provide a full path.
        string outputPath = "filled.pdf";
        var values = new Dictionary<string, string>
        {
            { "FirstName", "John" },
            { "LastName", "Doe" },
            { "Email", "john.doe@example.com" }
        };

        try
        {
            FillPdfForm(inputPath, values, outputPath);
            Console.WriteLine($"Form fields filled and saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
