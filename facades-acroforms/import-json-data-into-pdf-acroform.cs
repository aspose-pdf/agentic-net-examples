using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Sample data class to be serialized to JSON
    public class FormData
    {
        // Mark properties as required to satisfy non‑nullable warnings
        public required string Name { get; set; }
        public required int Age { get; set; }
        public required string Email { get; set; }
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Create an instance of the data to import
        FormData data = new FormData
        {
            Name = "John Doe",
            Age = 30,
            Email = "john.doe@example.com"
        };

        // Serialize the object to JSON
        string jsonString = JsonSerializer.Serialize(data);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);

        // Wrap the JSON bytes in a memory stream and import via the Facade Form
        using (MemoryStream jsonStream = new MemoryStream(jsonBytes))
        using (Document pdfDoc = new Document(inputPdfPath))
        // Fully‑qualify the Form class to avoid ambiguity with Aspose.Pdf.Forms.Form
        using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(pdfDoc))
        {
            // Correct API call – ImportJson expects a Stream
            form.ImportJson(jsonStream);

            // Save the modified PDF using the facade
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPdfPath}'.");
    }
}
