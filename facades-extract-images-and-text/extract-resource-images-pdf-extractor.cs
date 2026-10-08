using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Reflection;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputImg = "extracted_page1.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF document
                extractor.BindPdf(inputPdf);

                // -----------------------------------------------------------------
                // Set the image extraction mode to retrieve images that are defined
                // in the PDF resources (if the property exists in the used version).
                // The property was introduced in newer Aspose.Pdf.Facades releases.
                // To keep the code compatible with older versions we set it via
                // reflection – if the property is not present the line is simply
                // ignored and the default extraction behaviour is used.
                // -----------------------------------------------------------------
                PropertyInfo modeProp = typeof(PdfExtractor).GetProperty("ImageExtractionMode");
                if (modeProp != null && modeProp.CanWrite)
                {
                    // ImageExtractionMode enum lives in the same namespace.
                    // Cast the enum value to the property type using reflection.
                    object enumValue = Enum.Parse(modeProp.PropertyType, "DefinedInResources");
                    modeProp.SetValue(extractor, enumValue);
                }

                // Limit extraction to page 1 (1‑based indexing)
                extractor.StartPage = 1;
                extractor.EndPage   = 1;

                // Perform the extraction
                extractor.ExtractImage();

                // Retrieve the first image, if any
                if (extractor.HasNextImage())
                {
                    using (FileStream imgStream = new FileStream(outputImg, FileMode.Create, FileAccess.Write))
                    {
                        extractor.GetNextImage(imgStream);
                    }
                    Console.WriteLine($"Image extracted to '{outputImg}'.");
                }
                else
                {
                    Console.WriteLine("No images found on the specified page.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
