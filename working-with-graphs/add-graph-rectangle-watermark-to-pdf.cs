using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Folder containing source PDF files
        string inputFolder = @"C:\PdfInput";
        // Folder where modified PDFs will be saved
        string outputFolder = @"C:\PdfOutput";
        // Path to the graph image that will be inserted into each page
        string graphImagePath = @"C:\Resources\graph.png";

        // Ensure output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Iterate over all PDF files in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Open the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Process each page in the document
                foreach (Page page in doc.Pages)
                {
                    // ------------------------------------------------------------
                    // Insert the graph image as a stamp
                    // ------------------------------------------------------------
                    ImageStamp imgStamp = new ImageStamp(graphImagePath);
                    // Set desired size and alignment (adjust as needed)
                    imgStamp.Width = 200;
                    imgStamp.Height = 150;
                    imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
                    imgStamp.VerticalAlignment = VerticalAlignment.Center;
                    // Add the image stamp to the current page
                    page.AddStamp(imgStamp);

                    // ------------------------------------------------------------
                    // Insert a semi‑transparent text watermark (acts as a rectangle)
                    // ------------------------------------------------------------
                    TextStamp txtStamp = new TextStamp("CONFIDENTIAL");
                    txtStamp.Background = true;                     // draw background rectangle
                    txtStamp.Opacity = 0.3;                         // make it semi‑transparent
                    txtStamp.HorizontalAlignment = HorizontalAlignment.Center;
                    txtStamp.VerticalAlignment = VerticalAlignment.Center;
                    txtStamp.TextState.FontSize = 72;
                    txtStamp.TextState.FontStyle = FontStyles.Bold;
                    txtStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                    // Add the text stamp to the current page
                    page.AddStamp(txtStamp);
                }

                // Build the output file path (preserve original file name)
                string outputPath = System.IO.Path.Combine(outputFolder, System.IO.Path.GetFileName(pdfPath));
                // Save the modified document
                doc.Save(outputPath);
                Console.WriteLine($"Processed and saved: {outputPath}");
            }
        }

        Console.WriteLine("All PDFs have been processed.");
    }
}