using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string pdfPath = "input.pdf";
        string jsonPath = "images.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        var images = new List<Dictionary<string, object>>();
        int imageIndex = 0;

        // Load the document to know the total page count
        Document doc = new Document(pdfPath);
        int totalPages = doc.Pages.Count;

        // Use a single extractor instance; we will set the page range for each iteration
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);

            for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
            {
                // Restrict extraction to the current page only
                extractor.StartPage = pageNumber;
                extractor.EndPage   = pageNumber;
                extractor.ExtractImage();

                while (extractor.HasNextImage())
                {
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        // Retrieve the next image into the stream
                        extractor.GetNextImage(imgStream);
                        imgStream.Position = 0;
                        byte[] imgBytes = imgStream.ToArray();
                        string base64 = Convert.ToBase64String(imgBytes);

                        // Since PdfExtractor no longer provides GetImageInfo or GetImagePageNumber,
                        // we use the loop variables for page number and leave format/size as null.
                        var imgObj = new Dictionary<string, object>
                        {
                            ["index"]   = imageIndex,
                            ["page"]    = pageNumber,
                            ["base64"]  = base64,
                            ["format"]  = null,   // format information not directly available
                            ["width"]   = null,   // width information not directly available
                            ["height"]  = null    // height information not directly available
                        };

                        images.Add(imgObj);
                        imageIndex++;
                    }
                }
            }
        }

        // Serialize the list to JSON
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(images, options);
        File.WriteAllText(jsonPath, json);
        Console.WriteLine($"Extracted {images.Count} images to {jsonPath}");
    }
}
