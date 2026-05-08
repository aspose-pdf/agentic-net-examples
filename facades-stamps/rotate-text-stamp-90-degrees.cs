using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Required for FormattedText and EncodingType

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create formatted text that will be used as the stamp content
        // Note: FormattedText constructor expects System.Drawing.Color for the text color
        FormattedText formattedText = new FormattedText(
            "ROTATED TEXT",                 // Text to display
            System.Drawing.Color.Black,     // Text color
            "Helvetica",                    // Font name
            EncodingType.Winansi,           // Encoding
            false,                          // IsEmbedded (false = use system font)
            36);                            // Font size

        // Initialize a Aspose.Pdf.Facades.Stamp object and bind the formatted text
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        stamp.BindLogo(formattedText);

        // Set the rotation to 90 degrees
        stamp.Rotation = 90;

        // Output the rotation value to verify it was set correctly
        Console.WriteLine($"Aspose.Pdf.Facades.Stamp rotation set to: {stamp.Rotation} degrees");

        // Use the PdfFileStamp facade to apply the stamp to the PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);   // Load the source PDF
        fileStamp.AddStamp(stamp);      // Add the rotated text stamp
        fileStamp.Save(outputPath);     // Save the resulting PDF
        fileStamp.Close();              // Release resources

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}