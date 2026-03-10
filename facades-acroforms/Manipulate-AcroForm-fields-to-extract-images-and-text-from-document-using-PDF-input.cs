using System;
using System.IO;
using System.Drawing.Imaging; // For ImageFormat (Windows-only, required for image extraction)
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputTextPath = "extracted_text.txt";
        const string imagesFolder   = "ExtractedImages";
        const string xfdfPath       = "form_fields.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the images output directory exists
        Directory.CreateDirectory(imagesFolder);

        // -----------------------------------------------------------------
        // 1. Load the PDF document
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 2. Work with AcroForm fields (read values, export to XFDF)
            // -----------------------------------------------------------------
            Form form = new Form(pdfDoc);

            Console.WriteLine("AcroForm field values:");
            foreach (string fieldName in form.FieldNames)
            {
                string fieldValue = form.GetField(fieldName);
                Console.WriteLine($"  {fieldName} = {fieldValue}");
            }

            // Export all form fields to an XFDF file (optional)
            using (FileStream xfdfStream = new FileStream(xfdfPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportXfdf(xfdfStream);
            }

            // -----------------------------------------------------------------
            // 3. Extract all text from the document using PdfExtractor
            // -----------------------------------------------------------------
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(pdfDoc);                     // Bind the loaded document
            extractor.StartPage = 1;
            extractor.EndPage   = pdfDoc.Pages.Count;     // Process all pages
            extractor.ExtractText();                      // Prepare text extraction
            extractor.GetText(outputTextPath);            // Save extracted text to file

            // -----------------------------------------------------------------
            // 4. Extract all images from the document using PdfExtractor
            // -----------------------------------------------------------------
            // Set extraction mode – DefinedInResources extracts images that are
            // defined in the PDF resources (most common case).
            extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;
            extractor.ExtractImage();                     // Prepare image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(imagesFolder, $"image_{imageIndex}.png");
                // Save each image as PNG
                extractor.GetNextImage(imagePath, ImageFormat.Png);
                Console.WriteLine($"Saved image: {imagePath}");
                imageIndex++;
            }

            // -----------------------------------------------------------------
            // 5. Clean up
            // -----------------------------------------------------------------
            extractor.Close(); // Releases resources held by the extractor
        }

        Console.WriteLine("Extraction completed.");
    }
}