using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF into a memory stream (no intermediate file on disk)
        using (MemoryStream pdfStream = new MemoryStream(File.ReadAllBytes(pdfPath)))
        // Prepare a memory stream to receive the exported data
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Create a Document from the memory stream
            Document doc = new Document(pdfStream);

            // Export the form fields. Aspose.Pdf does not provide ExportToXml; the
            // supported method is ExportToJson. The JSON can be transformed to XML
            // by the caller if required.
            doc.Form.ExportToJson(outputStream);

            // Reset the position to read the exported content
            outputStream.Position = 0;
            using (StreamReader reader = new StreamReader(outputStream))
            {
                string exportedContent = reader.ReadToEnd();
                Console.WriteLine("Exported Form Data (JSON):");
                Console.WriteLine(exportedContent);
            }
        }
    }
}
