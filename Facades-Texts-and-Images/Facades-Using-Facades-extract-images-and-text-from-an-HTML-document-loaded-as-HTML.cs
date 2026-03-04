using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string htmlPath   = "input.html";          // HTML source
        const string pdfPath    = "temp.pdf";            // Intermediate PDF
        const string textOut    = "extracted_text.txt"; // Extracted text file
        const string imagesDir  = "ExtractedImages";    // Folder for images

        // Ensure the images output folder exists
        Directory.CreateDirectory(imagesDir);

        // -----------------------------------------------------------------
        // 1. Convert HTML to PDF (required because PdfExtractor works on PDF)
        // -----------------------------------------------------------------
        try
        {
            // Load HTML and save as PDF using HtmlLoadOptions
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document htmlDoc = new Document(htmlPath, loadOptions))
            {
                htmlDoc.Save(pdfPath); // Save intermediate PDF
            }
        }
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation aborted.");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error converting HTML to PDF: {ex.Message}");
            return;
        }

        // ---------------------------------------------------------------
        // 2. Use PdfExtractor facade to get text and images from the PDF
        // ---------------------------------------------------------------
        try
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the intermediate PDF file
                extractor.BindPdf(pdfPath);

                // ----------- Extract Text -----------
                extractor.ExtractText();                 // Perform text extraction
                extractor.GetText(textOut);              // Save all text to a file

                // ----------- Extract Images ----------
                extractor.ExtractImage();                // Perform image extraction

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Save each image as a separate file (auto-detect format)
                    string imagePath = Path.Combine(imagesDir, $"image_{imageIndex}.png");
                    extractor.GetNextImage(imagePath);
                    imageIndex++;
                }
            }

            Console.WriteLine("Extraction completed successfully.");
            Console.WriteLine($"Text saved to: {textOut}");
            Console.WriteLine($"Images saved to folder: {imagesDir}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Extraction error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary PDF file
            if (File.Exists(pdfPath))
            {
                try { File.Delete(pdfPath); } catch { /* ignore */ }
            }
        }
    }
}