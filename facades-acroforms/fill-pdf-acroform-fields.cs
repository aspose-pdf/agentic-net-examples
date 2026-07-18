using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

public static class PdfFormFiller
{
    /// <summary>
    /// Fills AcroForm fields in a PDF using Aspose.Pdf.Facades.Form and saves the result.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF containing form fields.</param>
    /// <param name="fieldValues">Dictionary where key = fully qualified field name, value = field value.</param>
    /// <param name="outputPdfPath">Path where the filled PDF will be saved.</param>
    public static void FillPdfForm(string inputPdfPath, Dictionary<string, string> fieldValues, string outputPdfPath)
    {
        if (string.IsNullOrEmpty(inputPdfPath))
            throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));

        if (string.IsNullOrEmpty(outputPdfPath))
            throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));

        if (fieldValues == null)
            throw new ArgumentNullException(nameof(fieldValues));

        // Form implements IDisposable via SaveableFacade, so wrap it in a using block.
        using (Form form = new Form(inputPdfPath))
        {
            // Iterate over the supplied field/value pairs and fill each field.
            foreach (KeyValuePair<string, string> kvp in fieldValues)
            {
                // FillField returns a bool indicating success; ignore it here or handle as needed.
                form.FillField(kvp.Key, kvp.Value);
            }

            // Save the modified document to the specified output path.
            form.Save(outputPdfPath);
        }
    }
}

// ---------------------------------------------------------------------------
// Minimal console entry point so the project compiles as an executable.
// This can be removed or replaced with a proper test harness when the code is
// consumed as a library.
// ---------------------------------------------------------------------------
public class Program
{
    public static void Main(string[] args)
    {
        // Simple demonstration of usage:
        //   dotnet run input.pdf output.pdf Name=John Age=30
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputPdfPath> <outputPdfPath> [fieldName=fieldValue ...]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        var values = new Dictionary<string, string>();

        for (int i = 2; i < args.Length; i++)
        {
            var parts = args[i].Split(new[] { '=' }, 2);
            if (parts.Length == 2)
                values[parts[0]] = parts[1];
        }

        try
        {
            PdfFormFiller.FillPdfForm(inputPath, values, outputPath);
            Console.WriteLine($"PDF form filled and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
