using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfFormFiller
{
    /// <summary>
    /// Fills an AcroForm PDF with the supplied field values and saves the result.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF containing form fields.</param>
    /// <param name="outputPdfPath">Path where the filled PDF will be saved.</param>
    /// <param name="fieldValues">Dictionary where key = full field name, value = value to set.</param>
    public static void FillPdfForm(string inputPdfPath, string outputPdfPath, IDictionary<string, string> fieldValues)
    {
        if (string.IsNullOrEmpty(inputPdfPath))
            throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));

        if (string.IsNullOrEmpty(outputPdfPath))
            throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));

        if (fieldValues == null)
            throw new ArgumentNullException(nameof(fieldValues));

        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

        // Form implements IDisposable via SaveableFacade, so use a using block for deterministic disposal.
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
            form.Save(outputPdfPath);
        }
    }
}

// ---------------------------------------------------------------------------
// Minimal entry point required by the project. It simply forwards command line
// arguments to the PdfFormFiller utility. This satisfies the compiler error
// CS5001 while keeping the library usable from other code.
// ---------------------------------------------------------------------------
public class Program
{
    public static void Main(string[] args)
    {
        // Expected usage: <inputPdf> <outputPdf> [fieldName=fieldValue ...]
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputPdf> <outputPdf> [fieldName=fieldValue ...]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        var fieldValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        for (int i = 2; i < args.Length; i++)
        {
            string arg = args[i];
            int eqIndex = arg.IndexOf('=');
            if (eqIndex > 0 && eqIndex < arg.Length - 1)
            {
                string key = arg.Substring(0, eqIndex);
                string value = arg.Substring(eqIndex + 1);
                fieldValues[key] = value;
            }
        }

        try
        {
            PdfFormFiller.FillPdfForm(inputPath, outputPath, fieldValues);
            Console.WriteLine($"PDF form filled successfully. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            Environment.Exit(1);
        }
    }
}
