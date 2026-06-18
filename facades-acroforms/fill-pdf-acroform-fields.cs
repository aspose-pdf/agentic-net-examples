using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfFormFiller
{
    /// <summary>
    /// Fills AcroForm fields in a PDF file using Aspose.Pdf.Facades.Form and saves the result.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF containing form fields.</param>
    /// <param name="fieldValues">Dictionary where key = fully qualified field name, value = value to set.</param>
    /// <param name="outputPdfPath">Path where the filled PDF will be saved.</param>
    public static void FillForm(string inputPdfPath, Dictionary<string, string> fieldValues, string outputPdfPath)
    {
        if (string.IsNullOrEmpty(inputPdfPath))
            throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));

        if (string.IsNullOrEmpty(outputPdfPath))
            throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));

        if (fieldValues == null)
            throw new ArgumentNullException(nameof(fieldValues));

        // Ensure the source file exists before proceeding.
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Source PDF not found: {inputPdfPath}");

        // Use the Form facade to bind to the source PDF.
        // Form implements IDisposable via SaveableFacade, so wrap it in a using block.
        using (Form form = new Form(inputPdfPath))
        {
            // Iterate over the supplied field/value pairs and fill each field.
            foreach (KeyValuePair<string, string> kvp in fieldValues)
            {
                // FillField returns true if the field exists and was filled successfully.
                // Ignoring the return value here; you could log or handle failures as needed.
                form.FillField(kvp.Key, kvp.Value);
            }

            // Save the modified document to the specified output path.
            // Form.Save(string) writes a PDF regardless of the file extension.
            form.Save(outputPdfPath);
        }
    }
}

// Entry point required for a console application.
public static class Program
{
    public static void Main(string[] args)
    {
        // The Main method is intentionally minimal. It exists solely to satisfy the
        // compiler's requirement for an entry point. Real usage of PdfFormFiller can be
        // performed by calling PdfFormFiller.FillForm from another project or by
        // extending this method with argument parsing as needed.
        Console.WriteLine("PdfFormFiller library loaded successfully.");
    }
}
