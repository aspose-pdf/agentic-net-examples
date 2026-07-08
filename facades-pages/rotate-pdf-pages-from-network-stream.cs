using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main()
    {
        // Replace with a valid, publicly reachable PDF URL.
        const string pdfUrl = "https://example.com/sample.pdf";

        using var httpClient = new HttpClient();

        // Download the PDF.
        using var response = await httpClient.GetAsync(pdfUrl);
        if (!response.IsSuccessStatusCode)
        {
            Console.Error.WriteLine($"Failed to download PDF. Status: {response.StatusCode}");
            return;
        }

        await using var networkStream = await response.Content.ReadAsStreamAsync();

        // Copy the network stream to a seek‑able memory stream.
        using var inputMemory = new MemoryStream();
        await networkStream.CopyToAsync(inputMemory);
        inputMemory.Position = 0; // reset for reading

        // Bind the PDF to the editor and rotate all pages.
        using var editor = new PdfPageEditor();
        editor.BindPdf(inputMemory);
        editor.Rotation = 90; // allowed values: 0, 90, 180, 270
        editor.ApplyChanges();

        // Save the edited PDF back into the same memory stream.
        inputMemory.SetLength(0); // clear original content
        editor.Save(inputMemory);
        inputMemory.Position = 0; // reset for further consumption

        // Example: write the result to a file (replace with your own handling).
        await File.WriteAllBytesAsync("rotated_output.pdf", inputMemory.ToArray());

        // At this point 'inputMemory' contains the rotated PDF and can be returned to the caller
        // or further processed as needed.
    }
}
