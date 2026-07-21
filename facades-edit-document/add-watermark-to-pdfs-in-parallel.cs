using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;          // Facade classes for PDF manipulation
using Aspose.Pdf.Text;            // FormattedText and EncodingType

class Program
{
    static void Main()
    {
        // Input directory containing PDFs to process
        const string inputDirectory = "InputPdfs";
        // Output directory for processed PDFs
        const string outputDirectory = "OutputPdfs";

        // Ensure output folder exists
        Directory.CreateDirectory(outputDirectory);

        // Verify that the input folder exists – if it does not, create it and exit gracefully
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input folder '{inputDirectory}' does not exist. Creating it now. Place PDFs there and re‑run the program.");
            Directory.CreateDirectory(inputDirectory);
            return;
        }

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputDirectory}'. Nothing to process.");
            return;
        }

        // Process each PDF in parallel to improve performance
        Parallel.ForEach(pdfFiles, inputPath =>
        {
            try
            {
                // Build output file path
                string fileName = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputDirectory, fileName + "_watermarked.pdf");

                // Create a formatted text object for the watermark
                // Note: FormattedText requires System.Drawing.Color for the text color
                FormattedText formattedText = new FormattedText(
                    "CONFIDENTIAL",                     // Text to display
                    System.Drawing.Color.Red,           // Text color (System.Drawing.Color is required here)
                    "Helvetica",                        // Font name
                    EncodingType.Winansi,               // Encoding
                    false,                              // Is embedded font
                    48);                                // Font size

                // Initialize the Stamp facade (represents the visual stamp)
                Stamp stamp = new Stamp();
                stamp.BindLogo(formattedText);
                stamp.SetOrigin(100, 400);
                stamp.IsBackground = true;
                stamp.Opacity = 0.5f;

                // Use PdfFileStamp to apply the Stamp to the source PDF
                using (PdfFileStamp pdfStamp = new PdfFileStamp())
                {
                    pdfStamp.BindPdf(inputPath);   // Bind the source PDF
                    pdfStamp.AddStamp(stamp);      // Add the visual stamp
                    pdfStamp.Save(outputPath);     // Save the watermarked PDF
                }
            }
            catch (Exception ex)
            {
                // Log the error but allow other files to continue processing
                Console.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        });

        Console.WriteLine("All PDFs have been processed.");
    }
}
