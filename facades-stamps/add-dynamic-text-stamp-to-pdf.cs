using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Dynamic values for the stamp
        string author = "John Doe";
        string stampText = $"Generated on {DateTime.Now:yyyy-MM-dd} by {author}";

        // Ensure the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Load the PDF document (core API) – required for page dimensions if needed
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the facade for stamping the whole document
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(inputPdf); // Bind the source PDF

            // Create a Facade Aspose.Pdf.Facades.Stamp object
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();

            // Position the stamp (example: centered at (200, 800))
            stamp.SetOrigin(200, 800);
            // Optional: make the stamp appear as background
            stamp.IsBackground = false;
            // Optional: set opacity (0.0 to 1.0)
            stamp.Opacity = 0.8f;

            // Create FormattedText with the interpolated string.
            // Note: FormattedText uses System.Drawing.Color for the text color.
            FormattedText formatted = new FormattedText(
                stampText,                     // Text with interpolation
                System.Drawing.Color.Black,    // Text color
                "Helvetica",                   // Font name
                EncodingType.Winansi,          // Encoding
                false,                         // IsEmbedded
                12);                           // Font size

            // Bind the formatted text to the stamp
            stamp.BindLogo(formatted);

            // Add the stamp to the document via the facade
            fileStamp.AddStamp(stamp);

            // Save the stamped PDF
            fileStamp.Save(outputPdf);
            fileStamp.Close();
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}