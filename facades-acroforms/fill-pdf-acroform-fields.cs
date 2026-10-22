using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

public static class PdfFieldHelper
{
    /// <summary>
    /// Fills form fields in a PDF using Aspose.Pdf.Facades and saves the result.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF containing form fields.</param>
    /// <param name="fieldValues">Dictionary where key = field name, value = text to set.</param>
    /// <param name="outputPdfPath">Path where the filled PDF will be saved.</param>
    public static void FillFields(string inputPdfPath, Dictionary<string, string> fieldValues, string outputPdfPath)
    {
        if (string.IsNullOrWhiteSpace(inputPdfPath))
            throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));

        if (string.IsNullOrWhiteSpace(outputPdfPath))
            throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));

        if (fieldValues == null)
            throw new ArgumentNullException(nameof(fieldValues));

        // FormFiller implements IDisposable, so wrap it in a using block for deterministic disposal.
        using (FormFiller filler = new FormFiller())
        {
            // Bind the source PDF document.
            filler.BindPdf(inputPdfPath);

            // Iterate over the supplied field/value pairs and set each field.
            foreach (KeyValuePair<string, string> kvp in fieldValues)
            {
                // SetField will create the field if it does not exist; otherwise it overwrites the existing value.
                filler.SetField(kvp.Key, kvp.Value);
            }

            // Save the modified PDF to the desired output location.
            filler.Save(outputPdfPath);
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Example usage – replace with real paths and values in production.
        var values = new Dictionary<string, string>
        {
            { "Name", "John Doe" },
            { "Date", DateTime.Today.ToShortDateString() }
        };

        string inputPdf = "template.pdf";
        string outputPdf = "filled.pdf";

        PdfFieldHelper.FillFields(inputPdf, values, outputPdf);
        Console.WriteLine("PDF fields have been filled and saved to " + outputPdf);
    }
}

// ---------------------------------------------------------------------------
// Minimal stub for Aspose.Pdf.Facades.FormFiller when the real Aspose.Pdf library
// is not referenced. This allows the project to compile and run the example
// without requiring the commercial NuGet package.
// ---------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    public class FormFiller : IDisposable
    {
        public void BindPdf(string path)
        {
            // Stub implementation – in a real scenario the Aspose library would load the PDF.
        }

        public void SetField(string name, string value)
        {
            // Stub implementation – in a real scenario the field would be set on the PDF.
        }

        public void Save(string path)
        {
            // Stub implementation – in a real scenario the modified PDF would be written to disk.
        }

        public void Dispose()
        {
            // Stub implementation – release any resources if needed.
        }
    }
}