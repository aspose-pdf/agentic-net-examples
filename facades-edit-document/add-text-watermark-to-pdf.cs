using System;
using System.IO;
using Aspose.Pdf.Facades;

/// <summary>
/// Demonstrates adding a text watermark to a PDF using Aspose.Pdf.Facades.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        ProcessPdf(inputPath, outputPath);
    }

    /// <summary>
    /// Binds the source PDF, creates a text stamp, adds it to the document,
    /// saves the result and releases resources.
    /// </summary>
    /// <param name="inputPdf">Path to the source PDF file.</param>
    /// <param name="outputPdf">Path where the watermarked PDF will be saved.</param>
    private static void ProcessPdf(string inputPdf, string outputPdf)
    {
        // Bind the PDF to the facade
        PdfFileStamp fileStamp = BindPdf(inputPdf);

        // Create and configure the stamp
        Stamp stamp = CreateConfiguredTextStamp();

        // Add the stamp to all pages
        AddStampToDocument(fileStamp, stamp);

        // Save the modified PDF and close the facade
        SaveAndClose(fileStamp, outputPdf);
    }

    /// <summary>
    /// Creates a new <see cref="PdfFileStamp"/> instance and binds it to the specified PDF file.
    /// </summary>
    /// <param name="pdfPath">Path to the PDF file to be edited.</param>
    /// <returns>Bound <see cref="PdfFileStamp"/> instance.</returns>
    private static PdfFileStamp BindPdf(string pdfPath)
    {
        PdfFileStamp stampFacade = new PdfFileStamp();
        stampFacade.BindPdf(pdfPath);
        return stampFacade;
    }

    /// <summary>
    /// Creates a <see cref="Stamp"/> containing formatted text and configures its visual properties.
    /// </summary>
    /// <returns>Configured <see cref="Stamp"/> ready to be added to a document.</returns>
    private static Stamp CreateConfiguredTextStamp()
    {
        // FormattedText constructor:
        // (string text, System.Drawing.Color color, string fontName, EncodingType encoding, bool isBold, int fontSize)
        FormattedText formatted = new FormattedText(
            "CONFIDENTIAL\nDo Not Distribute",
            System.Drawing.Color.Red,
            "Helvetica",
            EncodingType.Winansi,
            false,
            36);

        Stamp stamp = new Stamp();

        // Bind the formatted text to the stamp
        stamp.BindLogo(formatted);

        // Set the position (origin) in points (1 inch = 72 points)
        stamp.SetOrigin(100, 400);

        // Render the stamp behind page content
        stamp.IsBackground = true;

        // Set semi‑transparent opacity (0.0 to 1.0)
        stamp.Opacity = 0.5f;

        return stamp;
    }

    /// <summary>
    /// Adds the specified <see cref="Stamp"/> to the provided <see cref="PdfFileStamp"/> facade.
    /// </summary>
    /// <param name="fileStamp">Facade bound to a PDF document.</param>
    /// <param name="stamp">Stamp to be added.</param>
    private static void AddStampToDocument(PdfFileStamp fileStamp, Stamp stamp)
    {
        fileStamp.AddStamp(stamp);
    }

    /// <summary>
    /// Saves the edited PDF to the given path and closes the facade.
    /// </summary>
    /// <param name="fileStamp">Facade containing the edited document.</param>
    /// <param name="outputPath">Destination file path.</param>
    private static void SaveAndClose(PdfFileStamp fileStamp, string outputPath)
    {
        fileStamp.Save(outputPath);
        fileStamp.Close();
    }
}