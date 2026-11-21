using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;               // Core Aspose.PDF API
using Aspose.Pdf.Forms;        // For form field access

class Program
{
    // Entry point – async to allow await of file operations
    static async Task Main(string[] args)
    {
        // Paths – adjust as needed
        const string pdfTemplatePath = "template.pdf";
        const string outputPdfPath   = "filled_output.pdf";
        const string dataSourcePath  = "data.xlsx"; // placeholder – any file that would contain the Excel data

        // Validate input files
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(dataSourcePath))
        {
            Console.Error.WriteLine($"Data source not found: {dataSourcePath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Asynchronously read the XLSX source (simulated) and build the field dictionary
            // ------------------------------------------------------------
            var fieldValues = await LoadFieldValuesAsync(dataSourcePath);

            // ------------------------------------------------------------
            // 2. Load the PDF template using the core Aspose.Pdf API
            // ------------------------------------------------------------
            var pdfDocument = new Document(pdfTemplatePath);

            // ------------------------------------------------------------
            // 3. Fill the form fields. Use the Aspose.Pdf.Forms.Field class (not WidgetAnnotation).
            // ------------------------------------------------------------
            var form = pdfDocument.Form;
            foreach (var kvp in fieldValues)
            {
                // Find the field by its partial name (the key we have).
                Field field = FindFieldByName(form, kvp.Key);
                if (field != null)
                {
                    field.Value = kvp.Value; // Field.Value is the correct property.
                }
                else
                {
                    Console.Error.WriteLine($"Warning: field '{kvp.Key}' not found in the PDF template.");
                }
            }

            // ------------------------------------------------------------
            // 4. Save the filled PDF to a MemoryStream (synchronous API)
            // ------------------------------------------------------------
            using var memoryStream = new MemoryStream();
            pdfDocument.Save(memoryStream);
            memoryStream.Position = 0; // rewind for reading

            // ------------------------------------------------------------
            // 5. Asynchronously write the MemoryStream to the final output file
            // ------------------------------------------------------------
            await using var outputStream = new FileStream(
                outputPdfPath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 81920,
                useAsync: true);

            await memoryStream.CopyToAsync(outputStream);

            Console.WriteLine($"PDF successfully filled and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Helper that searches a Form for a field whose PartialName matches the supplied name.
    /// Returns null if the field cannot be found.
    /// </summary>
    private static Field FindFieldByName(Form form, string name)
    {
        // The Form.Fields collection contains objects derived from Field (e.g., TextBoxField, CheckBoxField, etc.).
        foreach (Field f in form.Fields)
        {
            // PartialName is the name used in the PDF form definition.
            if (string.Equals(f.PartialName, name, StringComparison.OrdinalIgnoreCase))
                return f;
        }
        return null;
    }

    /// <summary>
    /// Simulates asynchronous reading of an Excel (XLSX) file and returns a dictionary of field names/values.
    /// In a real‑world scenario you would parse the Excel file (e.g., using ClosedXML or EPPlus) after loading the bytes.
    /// </summary>
    private static async Task<Dictionary<string, string>> LoadFieldValuesAsync(string path)
    {
        // Asynchronously read the whole file – this demonstrates async I/O without pulling in a heavy Excel parser.
        await using var stream = new FileStream(
            path,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 81920,
            useAsync: true);

        // Discard the content – replace with real parsing logic as needed.
        using var ms = new MemoryStream();
        await stream.CopyToAsync(ms);
        // For demonstration we return a static set of values.
        return new Dictionary<string, string>
        {
            { "Name", "John Doe" },
            { "Date", DateTime.Today.ToShortDateString() }
            // Add more field/value pairs as required by the PDF form
        };
    }
}
