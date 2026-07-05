using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // TextFragment lives here

class Program
{
    // Valid rotation angles for page and stamp operations
    private static bool IsValidRotation(int angle)
    {
        return angle == 0 || angle == 90 || angle == 180 || angle == 270;
    }

    // Creates a minimal PDF if the expected input file does not exist.
    private static void EnsureSamplePdf(string path)
    {
        if (File.Exists(path))
            return;

        // Create a one‑page PDF with a simple paragraph.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample PDF – generated because the input file was missing."));
            doc.Save(path);
        }
        Console.WriteLine($"Sample PDF created at '{path}'.");
    }

    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "rotated_output.pdf";
        const string stampOutputPdf = "stamp_rotated_output.pdf";
        int rotationDegrees = 90; // example rotation; replace with desired value

        // Validate rotation before applying
        if (!IsValidRotation(rotationDegrees))
        {
            Console.Error.WriteLine($"Invalid rotation value: {rotationDegrees}. Allowed values are 0, 90, 180, 270.");
            return;
        }

        // Ensure we have a PDF to work with (creates a simple one if missing)
        EnsureSamplePdf(inputPdf);

        // ---------- Rotate all pages using PdfPageEditor ----------
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            // Bind the source PDF
            pageEditor.BindPdf(inputPdf);

            // Set the rotation for all pages
            pageEditor.Rotation = rotationDegrees;

            // Save the rotated document
            pageEditor.Save(outputPdf);
        }

        Console.WriteLine($"Pages rotated by {rotationDegrees}° and saved to '{outputPdf}'.");

        // ---------- Add a rotated stamp using PdfFileStamp ----------
        // Fully qualify the ambiguous Stamp type
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
        // Build the formatted text for the stamp (fully qualified as well)
        Aspose.Pdf.Facades.FormattedText ft = new Aspose.Pdf.Facades.FormattedText(
            "CONFIDENTIAL",
            System.Drawing.Color.Red,
            "Helvetica",
            Aspose.Pdf.Facades.EncodingType.Winansi,
            false,
            36);
        stamp.BindLogo(ft);
        // Rotation property of Stamp expects a float; cast from int
        stamp.Rotation = (float)rotationDegrees;

        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Load the original PDF (the same source we rotated earlier)
            fileStamp.BindPdf(inputPdf);

            // Add the rotated stamp (applies to all pages by default)
            fileStamp.AddStamp(stamp);

            // Save the stamped document
            fileStamp.Save(stampOutputPdf);
        }

        Console.WriteLine($"Stamp added with rotation {rotationDegrees}° and saved to '{stampOutputPdf}'.");
    }
}