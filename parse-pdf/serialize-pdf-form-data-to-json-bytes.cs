using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

public static class FormDataSerializer
{
    /// <summary>
    /// Loads a PDF, extracts all form fields to JSON, writes the JSON into a memory stream,
    /// and returns the resulting byte array.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF file containing form fields.</param>
    /// <returns>Byte array with the JSON representation of the form data.</returns>
    public static byte[] SerializeFormDataToJsonBytes(string pdfPath)
    {
        // Ensure the PDF file exists before attempting to load it.
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        using (MemoryStream jsonStream = new MemoryStream())
        {
            // Export all form fields to JSON and write directly into the memory stream.
            // ExportToJson writes UTF‑8 JSON by default.
            doc.Form.ExportToJson(jsonStream);

            // Reset the stream position to the beginning before reading.
            jsonStream.Position = 0;

            // Convert the memory stream contents to a byte array.
            return jsonStream.ToArray();
        }
    }
}

// Minimal entry point required for a console‑application project.
public static class Program
{
    public static void Main(string[] args)
    {
        // Optional demonstration: if a PDF path is supplied, serialize its form data.
        if (args.Length > 0)
        {
            try
            {
                byte[] jsonBytes = FormDataSerializer.SerializeFormDataToJsonBytes(args[0]);
                Console.WriteLine($"Serialized {jsonBytes.Length} bytes of JSON.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}