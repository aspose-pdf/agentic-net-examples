using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ImageToPdfCombiner
{
    static void Main()
    {
        // Folder containing the image files (adjust as needed)
        const string imagesFolder = "Images";
        // Output PDF file path
        const string outputPdf = "CombinedImages.pdf";

        // Resolve the folder to an absolute path based on the current working directory
        string imagesPath = Path.GetFullPath(imagesFolder);

        // Verify that the folder exists before trying to enumerate files
        if (!Directory.Exists(imagesPath))
        {
            Console.Error.WriteLine($"Images folder not found: {imagesPath}");
            return;
        }

        // Get all image files in the folder, sorted alphabetically to preserve order
        // (you can restrict the pattern to known image extensions if desired)
        string[] imageFiles = Directory.GetFiles(imagesPath, "*.*", SearchOption.TopDirectoryOnly);
        Array.Sort(imageFiles, StringComparer.OrdinalIgnoreCase);

        if (imageFiles.Length == 0)
        {
            Console.Error.WriteLine("No image files found in the specified folder.");
            return;
        }

        // Create an empty PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a blank page for each image (pages are 1‑based)
            foreach (string _ in imageFiles)
            {
                pdfDoc.Pages.Add();
            }

            // Initialize the PdfFileMend facade with the created document
            PdfFileMend pdfMend = new PdfFileMend(pdfDoc);

            // Add each image to its corresponding page, scaling it to fill the page.
            // A4 page size in points: 595 (width) x 842 (height)
            const float pageWidth = 595f;
            const float pageHeight = 842f;

            for (int i = 0; i < imageFiles.Length; i++)
            {
                int pageNumber = i + 1; // pages are 1‑based
                string imagePath = imageFiles[i];

                // Add the image to the page, scaling it to fill the page.
                // lowerLeftX, lowerLeftY = 0,0 (bottom‑left corner)
                // upperRightX, upperRightY = page dimensions (top‑right corner)
                pdfMend.AddImage(imagePath, pageNumber, 0f, 0f, pageWidth, pageHeight);
            }

            // Save the combined PDF
            pdfMend.Save(outputPdf);
            // Close the facade (optional, as it does not implement IDisposable)
            pdfMend.Close();

            Console.WriteLine($"Successfully created PDF: {outputPdf}");
        }
    }
}
