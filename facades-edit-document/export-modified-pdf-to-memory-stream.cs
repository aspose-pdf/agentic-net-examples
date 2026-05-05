using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Ensure the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Create the facade that supports editing and saving to a stream
        PdfPageEditor editor = new PdfPageEditor();

        // Load the PDF document into the facade
        editor.BindPdf(inputPdfPath);

        // Example modification: set zoom to 75%
        editor.Zoom = 0.75f;

        // Apply any pending changes (optional for some operations)
        editor.ApplyChanges();

        // Export the modified PDF to a memory stream
        using (MemoryStream outputStream = new MemoryStream())
        {
            editor.Save(outputStream);          // Save to stream using the facade's Save method
            outputStream.Position = 0;          // Reset position for downstream processing

            // At this point the stream contains the modified PDF.
            // Example: write the stream to a file (optional)
            File.WriteAllBytes("modified_output.pdf", outputStream.ToArray());

            // Further processing can be done directly with 'outputStream'
            Console.WriteLine($"Modified PDF exported to memory stream (size: {outputStream.Length} bytes).");
        }

        // Release resources held by the facade
        editor.Close();
    }
}