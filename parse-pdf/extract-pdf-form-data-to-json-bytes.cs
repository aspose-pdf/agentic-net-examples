using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

public static class FormDataSerializer
{
    /// <summary>
    /// Extracts all form fields from the specified PDF, serializes them to JSON,
    /// and returns the JSON as a byte array.
    /// </summary>
    /// <param name="pdfPath">Path to the source PDF file.</param>
    /// <returns>Byte array containing the JSON representation of the form data.</returns>
    public static byte[] ExtractFormDataAsJsonBytes(string pdfPath)
    {
        // Ensure the PDF file exists before attempting to load it.
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        using (MemoryStream jsonStream = new MemoryStream())
        {
            // Export all form fields to JSON and write them into the memory stream.
            // The ExportToJson method writes directly to the provided stream.
            doc.Form.ExportToJson(jsonStream);

            // Convert the memory stream contents to a byte array.
            // MemoryStream.ToArray returns the underlying buffer as a new array.
            return jsonStream.ToArray();
        }
    }

    // ---------------------------------------------------------------------
    // Entry point – added to satisfy the compiler when the project is built
    // as an executable. It can be omitted if the project is changed to a
    // class‑library (OutputType = Library).
    // ---------------------------------------------------------------------
    public static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            try
            {
                byte[] jsonBytes = ExtractFormDataAsJsonBytes(args[0]);
                Console.WriteLine($"JSON extracted successfully. Byte length: {jsonBytes.Length}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Usage: <executable> <pdfPath>");
        }
    }
}