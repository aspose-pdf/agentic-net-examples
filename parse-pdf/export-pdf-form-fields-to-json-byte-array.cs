using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

public static class PdfFormSerializer
{
    /// <summary>
    /// Loads a PDF document, extracts its form fields as JSON,
    /// writes the JSON to a MemoryStream and returns the stream contents as a byte array.
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
        {
            // Prepare a memory stream to receive the JSON output.
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // Export the form fields to JSON directly into the memory stream.
                // The ExportToJson method writes JSON and returns field serialization results (ignored here).
                doc.Form.ExportToJson(jsonStream);

                // Convert the populated memory stream to a byte array.
                // MemoryStream.ToArray returns the underlying bytes.
                return jsonStream.ToArray();
            }
        }
    }
}

// ---------------------------------------------------------------------------
// Added entry point to satisfy the console‑application requirement.
// This simple Main method demonstrates usage and ensures the assembly builds
// as an executable without altering the original library logic.
// ---------------------------------------------------------------------------
public class Program
{
    public static void Main(string[] args)
    {
        // If a PDF path is supplied, serialize its form data and write the byte count.
        if (args.Length > 0)
        {
            try
            {
                byte[] jsonBytes = PdfFormSerializer.SerializeFormDataToJsonBytes(args[0]);
                Console.WriteLine($"Serialized JSON length: {jsonBytes.Length} bytes");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                Environment.Exit(1);
            }
        }
        else
        {
            Console.WriteLine("Usage: <executable> <pdf-path>");
        }
    }
}