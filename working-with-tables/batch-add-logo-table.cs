using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        // Folder that contains the source PDF files (relative to the executable)
        string inputFolder = "input-pdfs";
        // Folder where processed PDFs will be written
        string outputFolder = "output-pdfs";
        // Path to the company logo image (PNG, JPEG, etc.)
        string logoPath = "logo.png";

        // Resolve paths to absolute file‑system paths – this prevents Aspose from treating them as URIs.
        string absoluteInputFolder = Path.GetFullPath(inputFolder);
        string absoluteOutputFolder = Path.GetFullPath(outputFolder);
        string absoluteLogoPath = Path.GetFullPath(logoPath);

        // Ensure the input folder exists – if it does not, give a clear message and exit gracefully.
        if (!Directory.Exists(absoluteInputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {absoluteInputFolder}");
            return;
        }

        // Ensure the output folder exists (create it if necessary).
        Directory.CreateDirectory(absoluteOutputFolder);

        // Verify that the logo file exists – otherwise the image insertion will throw.
        if (!File.Exists(absoluteLogoPath))
        {
            Console.Error.WriteLine($"Logo file not found: {absoluteLogoPath}");
            return;
        }

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(absoluteInputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            // Open the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Create a table with two columns
                Table table = new Table();

                // Add a single row
                Row row = table.Rows.Add();

                // First cell – company logo
                Cell logoCell = row.Cells.Add();
                // Use ImageStream (or a fully‑qualified file path) to avoid UriFormatException
                using (FileStream logoStream = File.OpenRead(absoluteLogoPath))
                {
                    Aspose.Pdf.Image logo = new Aspose.Pdf.Image { ImageStream = logoStream };
                    logoCell.Paragraphs.Add(logo);
                }

                // Second cell – company name text
                Cell textCell = row.Cells.Add();
                TextFragment tf = new TextFragment("Company Name");
                textCell.Paragraphs.Add(tf);

                // Insert the table at the top of the first page (index 0 in the Paragraphs collection)
                doc.Pages[1].Paragraphs.Insert(0, table);

                // Build the output file name and save the modified PDF
                string outputFileName = Path.Combine(absoluteOutputFolder, "processed_" + Path.GetFileName(pdfPath));
                doc.Save(outputFileName);
                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)} → {outputFileName}");
            }
        }
    }
}
