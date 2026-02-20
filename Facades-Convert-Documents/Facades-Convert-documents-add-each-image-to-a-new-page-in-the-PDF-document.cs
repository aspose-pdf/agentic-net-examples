using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the images to be added
        const string imagesFolder = "Images";
        // Output PDF file path
        const string outputPdf = "output.pdf";

        // Verify that the images folder exists
        if (!Directory.Exists(imagesFolder))
        {
            Console.Error.WriteLine($"Images folder not found: {imagesFolder}");
            return;
        }

        try
        {
            // Create a new empty PDF document
            Document pdfDoc = new Document();
            // The default document contains one blank page; remove it so that we add pages only for images
            if (pdfDoc.Pages.Count > 0)
                pdfDoc.Pages.Delete(1);

            // Initialize the PdfFileMend facade with the document
            using (PdfFileMend mend = new PdfFileMend(pdfDoc))
            {
                // Get all image files (common extensions) from the folder
                string[] imageFiles = Directory.GetFiles(imagesFolder, "*.*", SearchOption.TopDirectoryOnly);
                int pageNumber = 1;

                foreach (string imgPath in imageFiles)
                {
                    // Add a new blank page for the current image
                    pdfDoc.Pages.Add();

                    // Retrieve the newly added page to obtain its dimensions
                    Page page = pdfDoc.Pages[pageNumber];
                    float llx = 0;
                    float lly = 0;
                    // Width and Height are returned as double; cast to float for the AddImage overload
                    float urx = (float)page.PageInfo.Width;
                    float ury = (float)page.PageInfo.Height;

                    // Add the image to the page, scaling it to fill the page area
                    // AddImage(string imagePath, int pageNumber, float llx, float lly, float urx, float ury)
                    mend.AddImage(imgPath, pageNumber, llx, lly, urx, ury);

                    pageNumber++;
                }

                // Save the resulting PDF
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"PDF created successfully: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
