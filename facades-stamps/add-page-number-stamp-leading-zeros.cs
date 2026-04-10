using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists – create a simple one if it does not.
        if (!File.Exists(inputPath))
        {
            CreateSamplePdf(inputPath);
        }

        // Initialize the PdfFileStamp facade inside a using block so resources are released automatically.
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the source PDF file
            fileStamp.BindPdf(inputPath);

            // Configure page‑number stamp: Arabic numerals with leading zeros.
            // The format string "000#" means: pad with zeros to a width of three digits.
            fileStamp.NumberingStyle = NumberingStyle.NumeralsArabic;
            fileStamp.StartingNumber = 1; // optional, default is 1
            fileStamp.AddPageNumber("000#");

            // Save the stamped PDF
            fileStamp.Save(outputPath);
        }
    }

    /// <summary>
    /// Creates a very small PDF containing three blank pages. This helper is used only when the
    /// expected input file is missing, allowing the example to run without external resources.
    /// </summary>
    private static void CreateSamplePdf(string path)
    {
        using (Document doc = new Document())
        {
            // Add a few pages so the page‑number stamp can be demonstrated.
            for (int i = 0; i < 3; i++)
            {
                doc.Pages.Add();
            }
            doc.Save(path);
        }
    }
}
