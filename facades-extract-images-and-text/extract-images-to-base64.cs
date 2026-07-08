using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // needed for creating a fallback PDF

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        // Ensure the input PDF exists – create a minimal one if it does not.
        if (!File.Exists(inputPdf))
        {
            CreatePlaceholderPdf(inputPdf);
            Console.WriteLine($"Input file not found. A placeholder PDF has been created at '{inputPdf}'.");
        }

        // List to hold Base64 strings of extracted images
        List<string> imageBase64List = new List<string>();

        // PdfExtractor implements IDisposable – use a using block for deterministic cleanup
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdf);

            // Prepare the extractor to pull images from the document
            extractor.ExtractImage();

            // Iterate over all images found in the PDF
            while (extractor.HasNextImage())
            {
                // MemoryStream will receive the image data
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // Retrieve the next image in its original format and write it into the stream
                    // The overload without ImageFormat avoids the Windows‑only System.Drawing dependency
                    extractor.GetNextImage(imageStream);

                    // Convert the stream contents to a Base64 string
                    string base64 = Convert.ToBase64String(imageStream.ToArray());

                    // Store the Base64 representation for later JSON transmission
                    imageBase64List.Add(base64);
                }
            }
        }

        // Serialize the list of Base64 strings to JSON (e.g., for sending via an API)
        string jsonPayload = JsonSerializer.Serialize(imageBase64List, new JsonSerializerOptions { WriteIndented = true });

        // Output the JSON payload (or send it to your destination)
        Console.WriteLine(jsonPayload);
    }

    /// <summary>
    /// Creates a very simple PDF containing a single page. This method is used only when the expected
    /// input file is missing, allowing the sample to run without throwing a FileNotFoundException.
    /// </summary>
    private static void CreatePlaceholderPdf(string path)
    {
        // Aspose.Pdf.Document can be used without any external resources.
        using (Document doc = new Document())
        {
            // Add an empty page – no images, so the extractor will simply return an empty list.
            doc.Pages.Add();
            doc.Save(path);
        }
    }
}